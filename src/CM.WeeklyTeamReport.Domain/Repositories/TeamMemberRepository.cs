using System;
using System.Data.SqlClient;

namespace CM.WeeklyTeamReport.Domain
{
    public class TeamMemberRepository : IRepository<TeamMember?>
    {
        public TeamMember? Create(TeamMember? newTeamMember)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "insert into TeamMember (FirstName, LastName, Title, Email) " +
                "values (@FirstName, @LastName, @Title, @Email);" +
                "select * from TeamMember where TeamMemberId = scope_identity()",
                conn
                );
            command.Parameters.Add(new SqlParameter("FirstName", System.Data.SqlDbType.NVarChar, 20) { Value = newTeamMember?.FirstName });
            command.Parameters.Add(new SqlParameter("LastName", System.Data.SqlDbType.NVarChar, 20) { Value = newTeamMember?.LastName });
            command.Parameters.Add(new SqlParameter("Title", System.Data.SqlDbType.NVarChar, 20) { Value = newTeamMember?.Title });
            command.Parameters.Add(new SqlParameter("Email", System.Data.SqlDbType.NVarChar, 50) { Value = newTeamMember?.Email.Address });
            var reader = command.ExecuteReader();
            return MapTeamMember(reader);
        }

        

        public TeamMember? Read(int teamMemberId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from TeamMember where TeamMemberId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = teamMemberId });
            var reader = command.ExecuteReader();
            return MapTeamMember(reader);
        }

        public void Update(TeamMember? teamMember)
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

        public void Delete(TeamMember? teamMember)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "delete from TeamMember where TeamMemberId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = teamMember?.ID });
            command.ExecuteNonQuery();
        }

        private static TeamMember? MapTeamMember(SqlDataReader reader)
        {
            TeamMember? result = null;

            if (reader.Read())
            {
                result = new TeamMember(
                    (int)reader["TeamMemberId"],
                    reader["FirstName"]?.ToString(),
                    reader["LastName"]?.ToString(),
                    reader["Title"]?.ToString(),
                    reader["Email"]?.ToString()
                    );
            }
            return result;
        }

        private static SqlConnection CreateConnection()
        {
            var connection = new SqlConnection("Data Source=DESKTOP-OQH3EOQ;Initial Catalog=WeeklyReport;Integrated Security=True");
            connection.Open();
            return connection;
        }
    }
}
