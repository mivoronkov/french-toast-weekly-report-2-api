using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain
{
    public class WeeklyReportRepository : IWeeklyReportRepository
    {
        private readonly IConfiguration _configuration;

        public WeeklyReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IWeeklyReport Create(IWeeklyReport report)
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

        public IWeeklyReport Read(int reportId)
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
        public IFullWeeklyReport Read(int companyId, int teamMemberId, int reportId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                @"select w.ReportId, w.AuthorId, w.MoraleGradeId, rm.Level as MoraleLevel, rm.Commentary as MoraleCommentary, 
w.StressGradeId, rs.Level as StressLevel, rs.Commentary as StressCommentary, w.WorkloadGradeId, rw.Level as WorkloadLevel, 
rw.Commentary as WorkloadCommentary, w.HighThisWeek, w.LowThisWeek, w.AnythingElse, w.Date from WeeklyReport as w 
join ReportGrade as rm on rm.ReportGradeId = w.MoraleGradeId join ReportGrade as rs on rs.ReportGradeId = w.StressGradeId 
join ReportGrade as rw on rw.ReportGradeId = w.WorkloadGradeId join TeamMember as tm on tm.TeamMemberId = w.AuthorId 
where w.ReportId = @Id and w.AuthorId=@TeamMemberId and tm.CompanyId=@CompanyId",
                conn);
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = reportId });
            command.Parameters.Add(new SqlParameter("TeamMemberId", System.Data.SqlDbType.Int) { Value = teamMemberId });
            command.Parameters.Add(new SqlParameter("CompanyId", System.Data.SqlDbType.Int) { Value = companyId });
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new FullWeeklyReport
                {
                    ID = (int)reader["ReportId"],
                    AuthorId = (int)reader["AuthorId"],
                    MoraleGradeId = (int)reader["MoraleGradeId"],
                    MoraleLevel = (int)reader["MoraleLevel"],
                    MoraleCommentary = reader["MoraleCommentary"].ToString(),
                    StressGradeId = (int)reader["StressGradeId"],
                    StressLevel = (int)reader["StressLevel"],
                    StressCommentary = reader["StressCommentary"].ToString(),
                    WorkloadGradeId = (int)reader["WorkloadGradeId"],
                    WorkloadLevel = (int)reader["WorkloadLevel"],
                    WorkloadCommentary = reader["WorkloadCommentary"].ToString(),
                    HighThisWeek = reader["HighThisWeek"].ToString(),
                    LowThisWeek = reader["LowThisWeek"].ToString(),
                    AnythingElse = reader["AnythingElse"]?.ToString(),
                    Date = (DateTime)reader["Date"]
                };
            }
            return null;
        }

        public ICollection<IWeeklyReport> ReadAll()
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                "select * from WeeklyReport",
                conn
                );
            var reader = command.ExecuteReader();
            var result = new List<IWeeklyReport>();
            while (reader.Read())
            {
                result.Add(BuildReport(conn, reader));
            }
            return result;
        }
        public ICollection<IFullWeeklyReport> ReadAll(int companyId, int teamMemberId)
        {
            using var conn = CreateConnection();
            var command = new SqlCommand(
                @"select w.ReportId, w.AuthorId, w.MoraleGradeId, rm.Level as MoraleLevel, rm.Commentary as MoraleCommentary, 
w.StressGradeId, rs.Level as StressLevel, rs.Commentary as StressCommentary, w.WorkloadGradeId, rw.Level as WorkloadLevel, 
rw.Commentary as WorkloadCommentary, w.HighThisWeek, w.LowThisWeek, w.AnythingElse, w.Date from WeeklyReport as w 
join ReportGrade as rm on rm.ReportGradeId = w.MoraleGradeId join ReportGrade as rs on rs.ReportGradeId = w.StressGradeId 
join ReportGrade as rw on rw.ReportGradeId = w.WorkloadGradeId join TeamMember as tm on tm.TeamMemberId = w.AuthorId 
where w.AuthorId=@TeamMemberId and tm.CompanyId=@CompanyId",
                conn
                );
            command.Parameters.Add(new SqlParameter("TeamMemberId", System.Data.SqlDbType.Int) { Value = teamMemberId });
            command.Parameters.Add(new SqlParameter("CompanyId", System.Data.SqlDbType.Int) { Value = companyId });
            var reader = command.ExecuteReader();
            var result = new List<IFullWeeklyReport>();
            while (reader.Read())
            {
                result.Add(new FullWeeklyReport
                {
                    ID = (int)reader["ReportId"],
                    AuthorId = (int)reader["AuthorId"],
                    MoraleGradeId = (int)reader["MoraleGradeId"],
                    MoraleLevel = (int)reader["MoraleLevel"],
                    MoraleCommentary = reader["MoraleCommentary"].ToString(),
                    StressGradeId = (int)reader["StressGradeId"],
                    StressLevel = (int)reader["StressLevel"],
                    StressCommentary = reader["StressCommentary"].ToString(),
                    WorkloadGradeId = (int)reader["WorkloadGradeId"],
                    WorkloadLevel = (int)reader["WorkloadLevel"],
                    WorkloadCommentary = reader["WorkloadCommentary"].ToString(),
                    HighThisWeek = reader["HighThisWeek"].ToString(),
                    LowThisWeek = reader["LowThisWeek"].ToString(),
                    AnythingElse = reader["AnythingElse"]?.ToString(),
                    Date = (DateTime)reader["Date"]
                });
            }
            return result;
        }
        public async Task<ICollection<IFullWeeklyReport>> ReadReportsInInterval(int companyId, int memberId, DateTime firstDate, DateTime lastDate, string team ="")
        {
            var teamSearchConditon = new StringBuilder("");

            if(team == "immediate")
            {
                teamSearchConditon.Append("join ReportingTeamMemberToTeamMember as RTT on RTT.ReportingTMId = tm.TeamMemberId where RTT.LeaderTMId=@AuthorId");
            } else if(team == "extended")
            {
                teamSearchConditon.Append("where w.AuthorId!=@AuthorId");
            }
            var sqlString = @"select w.ReportId, w.AuthorId, tm.FirstName, tm.LastName, w.MoraleGradeId, rm.Level as MoraleLevel, rm.Commentary as MoraleCommentary, 
w.StressGradeId, rs.Level as StressLevel, rs.Commentary as StressCommentary, w.WorkloadGradeId, rw.Level as WorkloadLevel, 
rw.Commentary as WorkloadCommentary, w.HighThisWeek, w.LowThisWeek, w.AnythingElse, w.Date from WeeklyReport as w 
join ReportGrade as rm on rm.ReportGradeId = w.MoraleGradeId join ReportGrade as rs on rs.ReportGradeId = w.StressGradeId 
join ReportGrade as rw on rw.ReportGradeId = w.WorkloadGradeId join TeamMember as tm on tm.TeamMemberId = w.AuthorId "+teamSearchConditon +
" and tm.CompanyId=@CompanyId and Date between @FirstDate and @LastDate ORDER BY Date";
            using var conn = CreateConnection();
            var command = new SqlCommand(sqlString, conn);
            command.Parameters.Add(new SqlParameter("AuthorId", System.Data.SqlDbType.Int) { Value = memberId });
            command.Parameters.Add(new SqlParameter("CompanyId", System.Data.SqlDbType.Int) { Value = companyId });
            command.Parameters.Add(new SqlParameter("FirstDate", System.Data.SqlDbType.Date) { Value = firstDate });
            command.Parameters.Add(new SqlParameter("LastDate", System.Data.SqlDbType.Date) { Value = lastDate });
            var reader = await command.ExecuteReaderAsync();
            var result = new List<IFullWeeklyReport>();
            while (reader.Read())
            {
                result.Add(new FullWeeklyReport
                {
                    ID = (int)reader["ReportId"],
                    AuthorId = (int)reader["AuthorId"],
                    FirstName= reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    MoraleGradeId = (int)reader["MoraleGradeId"],
                    MoraleLevel = (int)reader["MoraleLevel"],
                    MoraleCommentary = reader["MoraleCommentary"].ToString(),
                    StressGradeId = (int)reader["StressGradeId"],
                    StressLevel = (int)reader["StressLevel"],
                    StressCommentary = reader["StressCommentary"].ToString(),
                    WorkloadGradeId = (int)reader["WorkloadGradeId"],
                    WorkloadLevel = (int)reader["WorkloadLevel"],
                    WorkloadCommentary = reader["WorkloadCommentary"].ToString(),
                    HighThisWeek = reader["HighThisWeek"].ToString(),
                    LowThisWeek = reader["LowThisWeek"].ToString(),
                    AnythingElse = reader["AnythingElse"]?.ToString(),
                    Date = (DateTime)reader["Date"]
                });
            }
            return result;
        }

        private IWeeklyReport BuildReport(SqlConnection conn, SqlDataReader reader)
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

        public void Update(IWeeklyReport report)
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
                @"update ReportGrade set Level = @MoraleLevel, Commentary = @MoraleCommentary where ReportGradeId = @Id;
update ReportGrade set Level = @StressLevel, Commentary = @StressCommentary where ReportGradeId = @Id;
update ReportGrade set Level = @WorkloadLevel, Commentary = @WorkloadCommentary where ReportGradeId = @Id;",
                conn
                );
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int) { Value = report.MoraleGradeId });
            command.Parameters.Add(new SqlParameter("MoraleLevel", System.Data.SqlDbType.Int) { Value = report.MoraleGrade.Level });
            command.Parameters.Add(new SqlParameter("MoraleCommentary", System.Data.SqlDbType.NVarChar, 600) { Value = report.MoraleGrade?.Commentary });
            command.Parameters.Add(new SqlParameter("StressLevel", System.Data.SqlDbType.Int) { Value = report.StressGrade.Level });
            command.Parameters.Add(new SqlParameter("StressCommentary", System.Data.SqlDbType.NVarChar, 600) { Value = report.StressGrade?.Commentary });
            command.Parameters.Add(new SqlParameter("WorkloadLevel", System.Data.SqlDbType.Int) { Value = report.WorkloadGrade.Level });
            command.Parameters.Add(new SqlParameter("WorkloadCommentary", System.Data.SqlDbType.NVarChar, 600) { Value = report.WorkloadGrade?.Commentary });
            command.ExecuteNonQuery();
        }

        public void Delete(int reportId)
        {
            Delete(Read(reportId));
        }

        public void Delete(IWeeklyReport report)
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
                "delete from ReportGrade where ReportGradeId in (@MoraleId, @StressId, @WorkloadId)",
                conn
            );
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("MoraleId", System.Data.SqlDbType.Int) { Value = report.MoraleGradeId });
            command.Parameters.Add(new SqlParameter("StressId", System.Data.SqlDbType.Int) { Value = report.StressGradeId });
            command.Parameters.Add(new SqlParameter("WorkloadId", System.Data.SqlDbType.Int) { Value = report.WorkloadGradeId });
            command.ExecuteNonQuery();
        }
        public async Task<ICollection<IOldReport>> ReadAverageOldReports(int companyId, int memberId, DateTime firstDate, DateTime lastDate, string team = "", string filter = "")
        {
            var teamSearchConditon = ChoiseTeamCommand(team);

            string baseSelect = "select ";
            string moraleColumn = " AVG(cast(NULLIF(rm.Level, 0) AS INT)) as MoraleLevel, ";
            string stressColumn = " AVG(cast(NULLIF(rs.Level, 0) AS INT)) as StressLevel, ";
            string workloadColumn = " AVG(cast(NULLIF(rw.Level, 0) AS INT)) as WorkloadLevel, ";
            string overallColumn = @" (Select AVG(MyAverage) From (Values (AVG(cast(NULLIF(rm.Level, 0) AS INT))), 
(AVG(cast(NULLIF(rs.Level, 0) AS INT))), (AVG(cast(NULLIF(rw.Level, 0) AS INT)))) as TableAvg(MyAverage)) as Overall, ";
            string dateColumn = " w.Date from WeeklyReport as w ";
            string moraleJoinTable = " join ReportGrade as rm on rm.ReportGradeId = w.MoraleGradeId ";
            string stressJoinTable = " join ReportGrade as rs on rs.ReportGradeId = w.StressGradeId ";
            string workloadJoinTable = " join ReportGrade as rw on rw.ReportGradeId = w.WorkloadGradeId ";
            string groupedStatment = @" join TeamMember as tm on tm.TeamMemberId=w.AuthorId "+teamSearchConditon+
                " and tm.CompanyId=@CompanyId and Date between @FirstDate and @LastDate group by Date ORDER BY Date";

            var sqlString = new StringBuilder("");
            switch (filter)
            {
                case "morale":
                    sqlString.Append(baseSelect).Append(moraleColumn).Append(dateColumn).Append(moraleJoinTable).Append(groupedStatment);
                    break;
                case "stress":
                    sqlString.Append(baseSelect).Append(stressColumn).Append(dateColumn).Append(stressJoinTable).Append(groupedStatment);
                    break;
                case "workload":
                    sqlString.Append(baseSelect).Append(workloadColumn).Append(dateColumn).Append(workloadJoinTable).Append(groupedStatment);
                    break;
                case "overall":
                    sqlString.Append(baseSelect)
                        .Append(overallColumn)
                        .Append(dateColumn)
                        .Append(moraleJoinTable)
                        .Append(stressJoinTable)
                        .Append(workloadJoinTable)
                        .Append(groupedStatment);
                    break;
                default:
                    sqlString.Append(baseSelect)
                        .Append(overallColumn)
                        .Append(dateColumn)
                        .Append(moraleJoinTable)
                        .Append(stressJoinTable)
                        .Append(workloadJoinTable)
                        .Append(groupedStatment);
                    break;
            };

            using var conn = CreateConnection();
            var command = new SqlCommand(sqlString.ToString(), conn);
            command.Parameters.Add(new SqlParameter("AuthorId", System.Data.SqlDbType.Int) { Value = memberId });
            command.Parameters.Add(new SqlParameter("CompanyId", System.Data.SqlDbType.Int) { Value = companyId });
            command.Parameters.Add(new SqlParameter("FirstDate", System.Data.SqlDbType.Date) { Value = firstDate });
            command.Parameters.Add(new SqlParameter("LastDate", System.Data.SqlDbType.Date) { Value = lastDate });
            var reader = await command.ExecuteReaderAsync();
            var result = new List<IOldReport>();
            while (reader.Read())
            {
                var element = new OldReport() { };
                switch (filter)
                {
                    case "morale":
                        element.StatusLevel = (int)reader["MoraleLevel"];
                        break;
                    case "stress":
                        element.StatusLevel = (int)reader["StressLevel"];
                        break;
                    case "workload":
                        element.StatusLevel = (int)reader["WorkloadLevel"];
                        break;
                    case "overall":
                        element.StatusLevel = (int)reader["Overall"];
                        break;
                    default:
                        element.StatusLevel = (int)reader["Overall"];
                        break;
                }
                element.Date = (DateTime)reader["Date"];
                result.Add(element);
            }
            return result;
        }

        public async Task<ICollection<IIndividualOldReport>> ReadMemberOldReports(int companyId, int memberId, DateTime firstDate, DateTime lastDate, string team = "", string filter="")
        {
            var teamSearchConditon = ChoiseTeamCommand(team);

            string baseSelect = "select w.AuthorId, tm.FirstName, tm.LastName, ";
            string moraleColumn = " rm.Level as MoraleLevel,  ";
            string stressColumn = " rs.Level as StressLevel, ";
            string workloadColumn = " rw.Level as WorkloadLevel, ";
            string overallColumn = " (Select AVG(MyAverage) From (Values (rm.Level), (rs.Level), (rw.Level)) as TblAverage(MyAverage)) as Overall, ";
            string dateColumn = " w.Date from WeeklyReport as w ";
            string moraleJoinTable = " join ReportGrade as rm on rm.ReportGradeId = w.MoraleGradeId ";
            string stressJoinTable = " join ReportGrade as rs on rs.ReportGradeId = w.StressGradeId ";
            string workloadJoinTable = " join ReportGrade as rw on rw.ReportGradeId = w.WorkloadGradeId ";
            string groupedStatment = @" join TeamMember as tm on tm.TeamMemberId=w.AuthorId " + teamSearchConditon +
                " and tm.CompanyId=@CompanyId and Date between @FirstDate and @LastDate ORDER BY Date";

            var sqlString = new StringBuilder("");
            switch (filter)
            {
                case "morale":
                    sqlString.Append(baseSelect).Append(moraleColumn).Append(dateColumn).Append(moraleJoinTable).Append(groupedStatment);
                    break;
                case "stress":
                    sqlString.Append(baseSelect).Append(stressColumn).Append(dateColumn).Append(stressJoinTable).Append(groupedStatment);
                    break;
                case "workload":
                    sqlString.Append(baseSelect).Append(workloadColumn).Append(dateColumn).Append(workloadJoinTable).Append(groupedStatment);
                    break;
                case "overall":
                    sqlString.Append(baseSelect)
                        .Append(overallColumn)
                        .Append(dateColumn)
                        .Append(moraleJoinTable)
                        .Append(stressJoinTable)
                        .Append(workloadJoinTable)
                        .Append(groupedStatment);
                    break;
                default:
                    sqlString.Append(baseSelect)
                        .Append(overallColumn)
                        .Append(dateColumn)
                        .Append(moraleJoinTable)
                        .Append(stressJoinTable)
                        .Append(workloadJoinTable)
                        .Append(groupedStatment);
                    break;
            };

            using var conn = CreateConnection();
            var command = new SqlCommand(sqlString.ToString(), conn);
            command.Parameters.Add(new SqlParameter("AuthorId", System.Data.SqlDbType.Int) { Value = memberId });
            command.Parameters.Add(new SqlParameter("CompanyId", System.Data.SqlDbType.Int) { Value = companyId });
            command.Parameters.Add(new SqlParameter("FirstDate", System.Data.SqlDbType.Date) { Value = firstDate });
            command.Parameters.Add(new SqlParameter("LastDate", System.Data.SqlDbType.Date) { Value = lastDate });
            var reader = await command.ExecuteReaderAsync();
            var result = new List<IIndividualOldReport>();
            while (reader.Read())
            {
                var element = new IndividualOldReport() { };
                switch (filter)
                {
                    case "morale":
                        element.StatusLevel = (int)reader["MoraleLevel"];
                        break;
                    case "stress":
                        element.StatusLevel = (int)reader["StressLevel"];
                        break;
                    case "workload":
                        element.StatusLevel = (int)reader["WorkloadLevel"];
                        break;
                    case "overall":
                        element.StatusLevel = (int)reader["Overall"];
                        break;
                    default:
                        element.StatusLevel = (int)reader["Overall"];
                        break;
                }
                element.AuthorId = (int)reader["AuthorId"];
                element.FirstName = reader["FirstName"].ToString();
                element.LastName = reader["LastName"].ToString();
                element.Date = (DateTime)reader["Date"];
                result.Add(element);
            }
            return result;
        }
        public IGrade MapReportGrade(SqlDataReader reader)
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
        private string ChoiseTeamCommand(string team)
        {
            var teamSearchConditon = new StringBuilder("");
            switch (team)
            {
                case "immediate":
                    teamSearchConditon.Append("join ReportingTeamMemberToTeamMember as RTT on RTT.ReportingTMId = tm.TeamMemberId where RTT.LeaderTMId=@AuthorId");
                    break;
                case "extended":
                    teamSearchConditon.Append("where w.AuthorId!=@AuthorId");
                    break;
                default:
                    teamSearchConditon.Append("where w.AuthorId!=@AuthorId");
                    break;
            }

            return teamSearchConditon.ToString();
        }
    }
}
