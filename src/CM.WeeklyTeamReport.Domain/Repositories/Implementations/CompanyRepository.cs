using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CM.WeeklyTeamReport.Domain
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IConfiguration _configuration;

        public CompanyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ICompany Create(ICompany newCompany)
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
            var reader = command.ExecuteReader();
            return reader.Read() ? MapCompany(reader) : null;
        }

        

        public ICompany Read(int companyId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from Company where CompanyId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = companyId });
            var reader = command.ExecuteReader();
            return reader.Read() ? MapCompany(reader) : null;
        }

        public ICollection<ICompany> ReadAll()
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from Company",
                conn
                );
            var result = new List<ICompany>();
            var reader = command.ExecuteReader();
            while (reader.Read())
                result.Add(MapCompany(reader));
            return result;
        }

        public void Update(ICompany company)
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


        public void Delete(int entityId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "delete from Company where CompanyId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = entityId });
            command.ExecuteNonQuery();
        }

        public void Delete(ICompany company)
        {
            Delete(company.ID);
        }

        private static ICompany MapCompany(SqlDataReader reader)
        {
            return new Company
            {
                ID = (int)reader["CompanyId"],
                Name = reader["Name"]?.ToString(),
                CreationDate = (reader["CreationDate"] as DateTime?) ?? null
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
