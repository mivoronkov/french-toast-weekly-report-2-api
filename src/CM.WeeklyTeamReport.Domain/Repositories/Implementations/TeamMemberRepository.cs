using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CM.WeeklyTeamReport.Domain
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly IConfiguration _configuration;

        public TeamMemberRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ITeamMember Create(ITeamMember newTeamMember)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "insert into TeamMember (FirstName, LastName, Title, Email, CompanyId) " +
                "values (@FirstName, @LastName, @Title, @Email, @CompanyId); " +
                "select * from TeamMember where TeamMemberId = scope_identity()",
                conn
                );
            command.Parameters.Add(new SqlParameter("FirstName", System.Data.SqlDbType.NVarChar, 20) { Value = newTeamMember?.FirstName });
            command.Parameters.Add(new SqlParameter("LastName", System.Data.SqlDbType.NVarChar, 20) { Value = newTeamMember?.LastName });
            command.Parameters.Add(new SqlParameter("Title", System.Data.SqlDbType.NVarChar, 20) { Value = newTeamMember?.Title });
            command.Parameters.Add(new SqlParameter("Email", System.Data.SqlDbType.NVarChar, 50) { Value = newTeamMember?.Email });
            command.Parameters.Add(new SqlParameter("CompanyId", System.Data.SqlDbType.Int) { Value = newTeamMember?.CompanyId });
            var reader = command.ExecuteReader();
            return reader.Read() ? MapTeamMember(reader) : null;
        }

        public ITeamMember Read(int teamMemberID)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from TeamMember where TeamMemberId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = teamMemberID });
            var reader = command.ExecuteReader();
            return reader.Read() ? MapTeamMember(reader) : null;
        }
        public ITeamMember Read(int companyID, int teamMemberID)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from TeamMember where CompanyId=@CompanyId and TeamMemberId = @TeamMemberId",
                conn
                );
            command.Parameters.Add(new SqlParameter("TeamMemberId", System.Data.SqlDbType.Int) { Value = teamMemberID });
            command.Parameters.Add(new SqlParameter("CompanyId", System.Data.SqlDbType.Int) { Value = companyID });

            var reader = command.ExecuteReader();
            return reader.Read() ? MapTeamMember(reader) : null;
        }

        public ICollection<ITeamMember> ReadAll()
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from TeamMember",
                conn
                );
            var reader = command.ExecuteReader();
            var result = new List<ITeamMember>();
            while (reader.Read())
                result.Add(MapTeamMember(reader));
            return result;
        }
        public ICollection<ITeamMember> ReadAll(int companyID)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from TeamMember where CompanyId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = companyID });
            var reader = command.ExecuteReader();
            var result = new List<ITeamMember>();
            while (reader.Read())
                result.Add(MapTeamMember(reader));
            return result;
        }

        public void Update(ITeamMember teamMember)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "update TeamMember " +
                "set FirstName = @FirstName," +
                "LastName = @LastName," +
                "Title = @Title," +
                "Email = @Email " +
                "where TeamMemberId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = teamMember?.ID });
            command.Parameters.Add(new SqlParameter("FirstName", System.Data.SqlDbType.NVarChar, 20) { Value = teamMember?.FirstName });
            command.Parameters.Add(new SqlParameter("LastName", System.Data.SqlDbType.NVarChar, 20) { Value = teamMember?.LastName });
            command.Parameters.Add(new SqlParameter("Title", System.Data.SqlDbType.NVarChar, 20) { Value = teamMember?.Title });
            command.Parameters.Add(new SqlParameter("Email", System.Data.SqlDbType.NVarChar, 50) { Value = teamMember?.Email });
            command.ExecuteNonQuery();
        }

        public void Delete(int teamMemberId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "delete from TeamMember where TeamMemberId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = teamMemberId });
            command.ExecuteNonQuery();
        }

        public void Delete(ITeamMember teamMember)
        {
            Delete(teamMember.ID);
        }

        public void AddReportingMember(ITeamMember reportingMember, ITeamMember leaderToReport)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "insert into ReportingTeamMemberToTeamMember (ReportingTMId, LeaderTMId) " +
                "values (@ReportingTMId, @LeaderTMId);",
                conn
                );
            command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = reportingMember.ID });
            command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = leaderToReport.ID });
            command.ExecuteNonQuery();
        }

        public void RemoveReportingMember(ITeamMember reportingMember, ITeamMember leaderToReport)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "delete from ReportingTeamMemberToTeamMember " +
                "where ReportingTMId = @ReportingTMId and LeaderTMId = @LeaderTMId;",
                conn
                );
            command.Parameters.Add(new SqlParameter("ReportingTMId", System.Data.SqlDbType.Int) { Value = reportingMember.ID });
            command.Parameters.Add(new SqlParameter("LeaderTMId", System.Data.SqlDbType.Int) { Value = leaderToReport.ID });
            command.ExecuteNonQuery();
        }

        public ICollection<ITeamMember> GetReportingMembers(ITeamMember teamMember)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from TeamMember, ReportingTeamMemberToTeamMember " +
                "where ReportingTeamMemberToTeamMember.LeaderTMId = @Id " +
                "and TeamMember.TeamMemberId = ReportingTeamMemberToTeamMember.ReportingTMId",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = teamMember.ID });
            using var reader = command.ExecuteReader();
            var list = new List<ITeamMember>();
            while (reader.Read())
                list.Add(MapTeamMember(reader));
            return list;
        }

        public ICollection<ITeamMember> GetLeadersToReport(ITeamMember teamMember)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from TeamMember, ReportingTeamMemberToTeamMember " +
                "where ReportingTeamMemberToTeamMember.ReportingTMId = @Id " +
                "and TeamMember.TeamMemberId = ReportingTeamMemberToTeamMember.LeaderTMId",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = teamMember.ID });
            using var reader = command.ExecuteReader();
            var list = new List<ITeamMember>();
            while (reader.Read())
                list.Add(MapTeamMember(reader));
            return list;
        }

        private SqlConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("Sql");
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        public static ITeamMember MapTeamMember(SqlDataReader reader)
        {
            return new TeamMember
            {
                ID = (int)reader["TeamMemberId"],
                FirstName = reader["FirstName"]?.ToString(),
                LastName = reader["LastName"]?.ToString(),
                Title = reader["Title"]?.ToString(),
                Email = reader["Email"]?.ToString(),
                CompanyId = (int)reader["CompanyId"]
            };
        }
    }
}
