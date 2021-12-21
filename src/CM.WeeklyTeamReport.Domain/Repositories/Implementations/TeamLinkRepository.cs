using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Implementations
{
    public class TeamLinkRepository: ITeamLinkRepository
    {
        private readonly IConfiguration _configuration;

        public TeamLinkRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ITeamLink Create(int reportingTMId, int leaderTMId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "insert into ReportingTeamMemberToTeamMember (ReportingTMId, LeaderTMId) " +
                "values (@ReportingTMId, @LeaderTMId);" +
                "select * from [ReportingTeamMemberToTeamMember] where ReportingTMId=@ReportingTMId and LeaderTMId=@LeaderTMId",
                conn
                );
            command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = reportingTMId });
            command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = leaderTMId });

            var reader = command.ExecuteReader();
            return reader.Read() ? MapLink(reader) : null;
        }
        public ITeamLink ReadLink(int reportingTMId, int leaderTMId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from [ReportingTeamMemberToTeamMember] where ReportingTMId=@ReportingTMId and LeaderTMId=@LeaderTMId",
                conn
                );
            command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = reportingTMId });
            command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = leaderTMId });

            var reader = command.ExecuteReader();
            return reader.Read() ? MapLink(reader) : null;
        }
        public void Delete(int reportingTMId, int leaderTMId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "delete from ReportingTeamMemberToTeamMember where ReportingTMId=@ReportingTMId and LeaderTMId=@LeaderTMId",
                conn
                );
            command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = reportingTMId });
            command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = leaderTMId });
            var reader = command.ExecuteReader();
        }
        public ICollection<ITeamLink> ReadLeaders(int reportingTMId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from ReportingTeamMemberToTeamMember where ReportingTMId=@ReportingTMId",
                conn
                );
            command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = reportingTMId });
            var reader = command.ExecuteReader();
            var result = new List<ITeamLink>();
            while (reader.Read())
            {
                result.Add(MapLink(reader));
            }

            return result;
        }
        public ICollection<ITeamLink> ReadReportingTMs(int leaderTMId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from ReportingTeamMemberToTeamMember where LeaderTMId=@LeaderTMId",
                conn
                );
            command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = leaderTMId });
            var reader = command.ExecuteReader();
            var result = new List<ITeamLink>();
            while (reader.Read())
            {
                result.Add(MapLink(reader));
            }

            return result;
        }
        private static ITeamLink MapLink(SqlDataReader reader)
        {
            return new TeamLink
            {
                ReportingTMId = (int)reader["ReportingTMId"],
                LeaderTMId = (int)reader["LeaderTMId"]
            };
        }
        private SqlConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("Sql");
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public void DeleteLiders(int memberId, IEnumerable<int> removingLeaders)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "delete from ReportingTeamMemberToTeamMember where ReportingTMId=@ReportingTMId and LeaderTMId=@Leaders;",
                conn
                );
            var leaders = removingLeaders.ToArray();
            for (int i = 0; i < leaders.Length; i++)
            {
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = memberId });
                command.Parameters.Add(new SqlParameter("Leaders", System.Data.SqlDbType.NVarChar) { Value = leaders[i] });
                command.ExecuteNonQuery();
            }
        }

        public void AddLeaders(int memberId, IEnumerable<int> addingLeaders)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "insert into ReportingTeamMemberToTeamMember (ReportingTMId, LeaderTMId) " +
                "values (@ReportingTMId, @LeaderTMId);",
                conn
                );
            var leaders = addingLeaders.ToArray();
            for (int i =0; i < leaders.Length; i++)
            {
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = memberId });
                command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = leaders[i] });
                command.ExecuteNonQuery();
            }

        }

        public void DeleteFollowers(int memberId, IEnumerable<int> removingFollowers)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "delete from ReportingTeamMemberToTeamMember where LeaderTMId=@LeaderId and ReportingTMId = @Followers",
                conn
                );
            var followers = removingFollowers.ToArray();
            for (int i = 0; i < followers.Length; i++)
            {
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("Followers", System.Data.SqlDbType.Int) { Value = followers[i] });
                command.Parameters.Add(new SqlParameter("LeaderId", System.Data.SqlDbType.Int) { Value = memberId });
                command.ExecuteNonQuery();
            }
        }

        public void AddFollowers(int memberId, IEnumerable<int> addingFollowers)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "insert into ReportingTeamMemberToTeamMember (ReportingTMId, LeaderTMId) " +
                "values (@ReportingTMId, @LeaderTMId);",
                conn
                );
            var followers = addingFollowers.ToArray();
            for (int i = 0; i < followers.Length; i++)
            {
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = followers[i] });
                command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = memberId });
                command.ExecuteNonQuery();
            }
        }
    }
}
