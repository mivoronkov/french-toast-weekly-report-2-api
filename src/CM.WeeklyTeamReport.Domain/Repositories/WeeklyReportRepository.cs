using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CM.WeeklyTeamReport.Domain
{
    public class WeeklyReportRepository : IRepository<WeeklyReport>
    {
        private readonly IConfiguration _configuration;

        public WeeklyReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public WeeklyReport Create(WeeklyReport report)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "insert into ReportGrade (Level, Commentary) values (@Level, @Commentary); " +
                "select * from ReportGrade where ReportGradeId = SCOPE_IDENTITY();",
                conn
                );
            command.Parameters.Add(new SqlParameter("Level", System.Data.SqlDbType.Int) { Value = (int)report.MoraleGrade.Level });
            command.Parameters.Add(new SqlParameter("Commentary", System.Data.SqlDbType.NVarChar, 600) {
                Value = (object)report.MoraleGrade.Commentary ?? DBNull.Value
            });
            var reader = command.ExecuteReader();
            var moraleGrade = reader.Read() ? MapReportGrade(reader) : null;
            report.MoraleGradeId = moraleGrade.ID;

            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Level", System.Data.SqlDbType.Int) { Value = (int)report.StressGrade.Level });
            command.Parameters.Add(new SqlParameter("Commentary", System.Data.SqlDbType.NVarChar, 600)
            {
                Value = (object)report.StressGrade.Commentary ?? DBNull.Value
            });
            reader.Close();
            reader = command.ExecuteReader();
            var stressGrade = reader.Read() ? MapReportGrade(reader) : null;
            report.StressGradeId = stressGrade.ID;

            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Level", System.Data.SqlDbType.Int) { Value = (int)report.WorkloadGrade.Level });
            command.Parameters.Add(new SqlParameter("Commentary", System.Data.SqlDbType.NVarChar, 600)
            {
                Value = (object)report.WorkloadGrade.Commentary ?? DBNull.Value
            });
            reader.Close();
            reader = command.ExecuteReader();
            var workloadGrade = reader.Read() ? MapReportGrade(reader) : null;
            report.WorkloadGradeId = workloadGrade.ID;

            command = new SqlCommand(
                "insert into WeeklyReport (AuthorId, MoraleGradeId, StressGradeId, WorkloadGradeId, HighThisWeek, LowThisWeek, AnythingElse, [Date]) " +
                "values (@AuthorId, @MoraleGradeId, @StressGradeId, @WorkloadGradeId, @HighThisWeek, @LowThisWeek, @AnythingElse, @Date); " +
                "select * from WeeklyReport where ReportId = scope_identity()",
                conn
                );
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("AuthorId", System.Data.SqlDbType.Int) { Value = report.AuthorId });
            command.Parameters.Add(new SqlParameter("MoraleGradeId", System.Data.SqlDbType.Int) { Value = report.MoraleGradeId });
            command.Parameters.Add(new SqlParameter("StressGradeId", System.Data.SqlDbType.Int) { Value = report.StressGradeId });
            command.Parameters.Add(new SqlParameter("WorkloadGradeId", System.Data.SqlDbType.Int) { Value = report.WorkloadGradeId });
            command.Parameters.Add(new SqlParameter("HighThisWeek", System.Data.SqlDbType.NVarChar, 600) { Value = report.HighThisWeek });
            command.Parameters.Add(new SqlParameter("LowThisWeek", System.Data.SqlDbType.NVarChar, 600) { Value = report.LowThisWeek });
            command.Parameters.Add(new SqlParameter("AnythingElse", System.Data.SqlDbType.NVarChar, 600)
            {
                Value = (object)report.AnythingElse ?? DBNull.Value
            });
            command.Parameters.Add(new SqlParameter("Date", System.Data.SqlDbType.Date) { Value = report.Date });
            reader.Close();
            reader = command.ExecuteReader();
            reader.Read();
            return Read((int)reader["ReportId"]);
        }

        public WeeklyReport Read(int reportId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from WeeklyReport where ReportId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = reportId });
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return BuildReport(conn, reader);
            }
            return null;
        }

        public ICollection<WeeklyReport> ReadAll()
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from WeeklyReport",
                conn
                );
            var reader = command.ExecuteReader();
            var result = new List<WeeklyReport>();
            while (reader.Read())
            {
                result.Add(BuildReport(conn, reader));
            }
            return result;
        }

        private WeeklyReport BuildReport(SqlConnection conn, SqlDataReader reader)
        {
            var report = new WeeklyReport
            {
                ID = (int)reader["ReportId"],
                AuthorId = (int)reader["AuthorId"],
                MoraleGradeId = (int)reader["MoraleGradeId"],
                StressGradeId = (int)reader["StressGradeId"],
                WorkloadGradeId = (int)reader["WorkloadGradeId"],
                HighThisWeek = reader["HighThisWeek"].ToString(),
                LowThisWeek = reader["LowThisWeek"].ToString(),
                AnythingElse = reader["AnythingElse"]?.ToString(),
                Date = (DateTime)reader["Date"]
            };
            var command = new SqlCommand(
                "select * from ReportGrade where ReportGradeId = @Id",
                conn
            );
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = report.MoraleGradeId });
            reader.Close();
            reader = command.ExecuteReader();
            reader.Read();
            report.MoraleGrade = MapReportGrade(reader);

            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = report.StressGradeId });
            reader.Close();
            reader = command.ExecuteReader();
            reader.Read();
            report.StressGrade = MapReportGrade(reader);

            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = report.WorkloadGradeId });
            reader.Close();
            reader = command.ExecuteReader();
            reader.Read();
            report.WorkloadGrade = MapReportGrade(reader);

            return report;
        }

        public void Update(WeeklyReport report)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "update WeeklyReport " +
                "set AuthorId = @AuthorId," +
                "HighThisWeek = @HighThisWeek, " +
                "LowThisWeek = @LowThisWeek, " +
                "AnythingElse = @AnythingElse " +
                "where ReportId = @Id",
                conn
                );
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = report.ID });
            command.Parameters.Add(new SqlParameter("AuthorId", System.Data.SqlDbType.Int) { Value = report.AuthorId });
            command.Parameters.Add(new SqlParameter("HighThisWeek", System.Data.SqlDbType.NVarChar, 600) { Value = report.HighThisWeek });
            command.Parameters.Add(new SqlParameter("LowThisWeek", System.Data.SqlDbType.NVarChar, 600) { Value = report.LowThisWeek });
            command.Parameters.Add(new SqlParameter("AnythingElse", System.Data.SqlDbType.NVarChar, 600)
            {
                Value = (object)report.AnythingElse ?? DBNull.Value
            });
            command.ExecuteNonQuery();

            command = new SqlCommand(
                "update ReportGrade " +
                "set Level = @Level," +
                "Commentary = @Commentary " +
                "where ReportGradeId = @Id",
                conn
                );
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = report.MoraleGradeId });
            command.Parameters.Add(new SqlParameter("Level", System.Data.SqlDbType.Int) { Value = report.MoraleGrade.Level });
            command.Parameters.Add(new SqlParameter("Commentary", System.Data.SqlDbType.NVarChar, 600) { Value = report.MoraleGrade?.Commentary });
            command.ExecuteNonQuery();

            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = report.StressGradeId });
            command.Parameters.Add(new SqlParameter("Level", System.Data.SqlDbType.Int) { Value = report.StressGrade.Level });
            command.Parameters.Add(new SqlParameter("Commentary", System.Data.SqlDbType.NVarChar, 600) { Value = report.StressGrade?.Commentary });
            command.ExecuteNonQuery();

            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = report.WorkloadGradeId });
            command.Parameters.Add(new SqlParameter("Level", System.Data.SqlDbType.Int) { Value = report.WorkloadGrade.Level });
            command.Parameters.Add(new SqlParameter("Commentary", System.Data.SqlDbType.NVarChar, 600) { Value = report.WorkloadGrade?.Commentary });
            command.ExecuteNonQuery();
        }

        public void Delete(int reportId)
        {
            Delete(Read(reportId));
        }

        public void Delete(WeeklyReport report)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "delete from WeeklyReport where ReportId = @Id",
                conn
            );
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = report.ID });
            command.ExecuteNonQuery();

            command = new SqlCommand(
                "delete from ReportGrade where ReportGradeId = @Id",
                conn
            );
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = report.MoraleGradeId });
            command.ExecuteNonQuery();

            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = report.StressGradeId });
            command.ExecuteNonQuery();

            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = report.WorkloadGradeId });
            command.ExecuteNonQuery();
        }

        public Grade MapReportGrade(SqlDataReader reader)
        {
            return new Grade
            {
                ID = (int)reader["ReportGradeId"],
                Level = (Level)reader["Level"],
                Commentary = reader["Commentary"].ToString()
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
