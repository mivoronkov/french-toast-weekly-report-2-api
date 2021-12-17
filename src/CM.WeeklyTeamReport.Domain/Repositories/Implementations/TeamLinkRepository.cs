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
                "select * from [ReportingTeamMemberToTeamMember] where LinkId = scope_identity()",
                conn
                );
            command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = reportingTMId });
            command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = leaderTMId });

            var reader = command.ExecuteReader();
            return reader.Read() ? MapCompany(reader) : null;
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
                result.Add(MapCompany(reader));
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
                result.Add(MapCompany(reader));
            }

            return result;
        }
        private static ITeamLink MapCompany(SqlDataReader reader)
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
    }
}
