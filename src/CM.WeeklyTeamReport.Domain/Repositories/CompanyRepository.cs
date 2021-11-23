using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CM.WeeklyTeamReport.Domain
{
    public class CompanyRepository : IRepository<Company>
    {
        public Company Create(Company newCompany)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "insert into Company (Name, CreationDate) " +
                "values (@Name, @CreationDate);" +
                "select * from Company where CompanyId = scope_identity()",
                conn
                );
            command.Parameters.Add(new SqlParameter("Name", System.Data.SqlDbType.NVarChar, 50) { Value = newCompany?.Name });
            command.Parameters.Add(new SqlParameter("CreationDate", System.Data.SqlDbType.DateTime)
            {
                Value = (object)newCompany.CreationDate ?? DBNull.Value
            });
            return MapCompany(command.ExecuteReader());
        }

        

        public Company Read(int companyId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from Company where CompanyId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = companyId });
            return MapCompany(command.ExecuteReader());
        }

        public void Update(Company company)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "update Company " +
                "set Name = @Name, " +
                "CreationDate = @CreationDate " +
                "where CompanyId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = company?.ID });
            command.Parameters.Add(new SqlParameter("Name", System.Data.SqlDbType.NVarChar, 20) { Value = company?.Name });
            command.Parameters.Add(new SqlParameter("CreationDate", System.Data.SqlDbType.DateTime)
            {
                Value = (object)company.CreationDate ?? DBNull.Value
            });
            command.ExecuteNonQuery();
        }

        public ICollection<TeamMember> GetTeamMembers(Company company)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from TeamMember, Company " +
                "where TeamMember.CompanyId = @CompanyId",
                conn
                );
            command.Parameters.Add(new SqlParameter("CompanyId", System.Data.SqlDbType.Int) { Value = company.ID });
            using var reader = command.ExecuteReader();
            var list = new List<TeamMember>();
            while (reader.Read())
                list.Add(TeamMemberRepository.MapTeamMember(reader));
            return list;
        }

        public void Delete(Company company)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "delete from Company where CompanyId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = company?.ID });
            command.ExecuteNonQuery();
        }

        private static Company MapCompany(SqlDataReader reader)
        {
            Company result = null;

            if (reader.Read())
            {
                result = new Company {
                    ID = (int)reader["CompanyId"],
                    Name = reader["Name"]?.ToString(),
                    CreationDate = (reader["CreationDate"] as DateTime?) ?? null
                    };
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
