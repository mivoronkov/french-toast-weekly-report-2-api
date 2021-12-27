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

        public async Task<ITeamLink> Create(int reportingTMId, int leaderTMId)
        {
            using var conn = await CreateConnection();
            var command = new SqlCommand(
                "insert into ReportingTeamMemberToTeamMember (ReportingTMId, LeaderTMId) " +
                "values (@ReportingTMId, @LeaderTMId);" +
                "select * from [ReportingTeamMemberToTeamMember] where ReportingTMId=@ReportingTMId and LeaderTMId=@LeaderTMId",
                conn
                );
            command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = reportingTMId });
            command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = leaderTMId });

            var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapLink(reader) : null;
        }
        public async  Task<ITeamLink> ReadLink(int reportingTMId, int leaderTMId)
        {
            using var conn = await CreateConnection();
            var command = new SqlCommand(
                "select * from [ReportingTeamMemberToTeamMember] where ReportingTMId=@ReportingTMId and LeaderTMId=@LeaderTMId",
                conn
                );
            command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = reportingTMId });
            command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = leaderTMId });

            var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? MapLink(reader) : null;
        }
        public async Task Delete(int reportingTMId, int leaderTMId)
        {
            using var conn = await CreateConnection();
            var command = new SqlCommand(
                "delete from ReportingTeamMemberToTeamMember where ReportingTMId=@ReportingTMId and LeaderTMId=@LeaderTMId",
                conn
                );
            command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = reportingTMId });
            command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = leaderTMId });
            await command.ExecuteNonQueryAsync();
        }
        public async Task<ICollection<ITeamLink>> ReadLeaders(int reportingTMId)
        {
            using var conn = await CreateConnection();
            var command = new SqlCommand(
                "select * from ReportingTeamMemberToTeamMember where ReportingTMId=@ReportingTMId",
                conn
                );
            command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = reportingTMId });
            var reader = await command.ExecuteReaderAsync();
            var result = new List<ITeamLink>();
            while (await reader.ReadAsync())
            {
                result.Add(MapLink(reader));
            }

            return result;
        }
        public async Task<ICollection<ITeamLink>> ReadReportingTMs(int leaderTMId)
        {
            using var conn = await CreateConnection();
            var command = new SqlCommand(
                "select * from ReportingTeamMemberToTeamMember where LeaderTMId=@LeaderTMId",
                conn
                );
            command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = leaderTMId });
            var reader = await command.ExecuteReaderAsync();
            var result = new List<ITeamLink>();
            while (await reader.ReadAsync())
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
        private async Task<SqlConnection> CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("Sql");
            var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }

        public async Task DeleteLiders(int memberId, IEnumerable<int> removingLeaders)
        {
            using var conn = await CreateConnection();
            var leaders = removingLeaders.ToArray();
            var leadersList = new StringBuilder("");
            for (int i = 0; i < leaders.Length; i++)
            {
                leadersList.Append(leaders[i]);
                leadersList.Append(",");
            }
            leadersList.Remove(leadersList.Length - 1, 1);
            var command = new SqlCommand(
                $"delete from ReportingTeamMemberToTeamMember where ReportingTMId=@ReportingTMId and LeaderTMId in ({leadersList});",
                conn
                );
            command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = memberId });
            await command.ExecuteNonQueryAsync();

        }

        public async Task AddLeaders(int memberId, IEnumerable<int> addingLeaders)
        {
            using var conn = await CreateConnection();
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
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteFollowers(int memberId, IEnumerable<int> removingFollowers)
        {
            using var conn = await CreateConnection();
            var followers = removingFollowers.ToArray();
            var followersList = new StringBuilder("");
            for (int i = 0; i < followers.Length; i++)
            {
                followersList.Append(followers[i]);
                followersList.Append(",");
            }
            followersList.Remove(followersList.Length - 1, 1);
            var command = new SqlCommand(
                $"delete from ReportingTeamMemberToTeamMember where LeaderTMId=@LeaderId and ReportingTMId in ({followersList})",
                conn
                );
            command.Parameters.Add(new SqlParameter("LeaderId", System.Data.SqlDbType.Int) { Value = memberId });
            await command.ExecuteNonQueryAsync();

        }

        public async Task AddFollowers(int memberId, IEnumerable<int> addingFollowers)
        {
            using var conn = await CreateConnection();
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
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
