using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CM.WeeklyTeamReport.Domain
{
    public class TeamMemberRepository : IRepository<TeamMember>
    {
        public TeamMember Create(TeamMember newTeamMember)
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
            command.Parameters.Add(new SqlParameter("Email", System.Data.SqlDbType.NVarChar, 50) { Value = newTeamMember?.Email.Address });
            command.Parameters.Add(new SqlParameter("CompanyId", System.Data.SqlDbType.Int) { Value = newTeamMember?.CompanyId });
            var reader = command.ExecuteReader();
            return reader.Read() ? MapTeamMember(reader) : null;
        }

        public TeamMember Read(int teamMemberId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from TeamMember where TeamMemberId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = teamMemberId });
            var reader = command.ExecuteReader();
            return reader.Read() ? MapTeamMember(reader) : null;
        }

        public void Update(TeamMember teamMember)
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
            command.Parameters.Add(new SqlParameter("Email", System.Data.SqlDbType.NVarChar, 50) { Value = teamMember?.Email.Address });
            command.ExecuteNonQuery();
        }

        public void Delete(TeamMember teamMember)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "delete from TeamMember where TeamMemberId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = teamMember?.ID });
            command.ExecuteNonQuery();
        }

        public static TeamMember MapTeamMember(SqlDataReader reader)
        {
            return new TeamMember
            {
                ID = (int)reader["TeamMemberId"],
                FirstName = reader["FirstName"]?.ToString(),
                LastName = reader["LastName"]?.ToString(),
                Title = reader["Title"]?.ToString(),
                Email = new System.Net.Mail.MailAddress(reader["Email"]?.ToString()),
                CompanyId = (int)reader["CompanyId"]
            };
        }

        public void AddReportingMember(TeamMember reportingMember, TeamMember leaderToReport)
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

        public void RemoveReportingMember(TeamMember reportingMember, TeamMember leaderToReport)
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

        public ICollection<TeamMember> GetReportingMembers(TeamMember teamMember)
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
            var list = new List<TeamMember>();
            while (reader.Read())
                list.Add(MapTeamMember(reader));
            return list;
        }

        public ICollection<TeamMember> GetLeadersToReport(TeamMember teamMember)
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
            var list = new List<TeamMember>();
            while (reader.Read())
                list.Add(MapTeamMember(reader));
            return list;
        }

        private static SqlConnection CreateConnection()
        {
            var connection = new SqlConnection("Data Source=DESKTOP-OQH3EOQ;Initial Catalog=WeeklyReport;Integrated Security=True");
            connection.Open();
            return connection;
        }
    }
}
