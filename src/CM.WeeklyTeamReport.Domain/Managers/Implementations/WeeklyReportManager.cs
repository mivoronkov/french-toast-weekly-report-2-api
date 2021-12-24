using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Dto.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Managers
{
    public class WeeklyReportManager : IWeeklyReportManager
    {
        private readonly IWeeklyReportRepository _repository;
        private readonly IReportCommands _reportCommands;

        public WeeklyReportManager(IWeeklyReportRepository weeklyReportRepository, IReportCommands reportCommands)
        {
            _repository = weeklyReportRepository;
            _reportCommands = reportCommands;
        }
        public IWeeklyReport create(ReportsDto newWeeklyReport)
        {
            var newReport = _reportCommands.dtoToReport(newWeeklyReport);
            return _repository.Create(newReport);
        }

        public void delete(ReportsDto reportDto)
        {
            var report = _reportCommands.dtoToReport(reportDto);
            _repository.Delete(report);
        }

        public ICollection<ReportsDto> readAll(int companyId, int teamMemberId)
        {
            var reports =_repository.ReadAll(companyId, teamMemberId);
            if (reports.Count==0 )
            {
                return null;
            }
            var reportsDto = reports.Select(el => _reportCommands.fullReportToDto(el)).ToList();

            return reportsDto;
        }

        public ReportsDto read(int companyId, int teamMemberId, int reportId)
        {
            var report = _repository.Read(companyId, teamMemberId, reportId);
            if (report == null)
            {
                return null;
            }
            var reportDto = _reportCommands.fullReportToDto(report);

            return reportDto;
        }

        public void update(ReportsDto oldEntity, ReportsDto newEntity)
        {
            newEntity.ID = oldEntity.ID;
            var report = _reportCommands.dtoToReport(newEntity);
            _repository.Update(report);
        }

        public ICollection<ReportsDto> ReadReportsInInterval(int companyId, int teamMemberId, DateTime start, DateTime end)
        {
            var firstDate = start;
            var lastDate = end;
            if (start> end)
            {
                firstDate = end;
                lastDate = start;
            }
            var reports = _repository.ReadReportsInInterval(companyId, teamMemberId, firstDate, lastDate);
            if (reports.Count == 0)
            {
                return null;
            }
            var reportsDto = reports.Select(el => _reportCommands.fullReportToDto(el)).ToList();

            return reportsDto;
        }

        public ICollection<IFullWeeklyReport> ReadReportHistory(int companyId, int teamMemberId, DateTime start, 
            DateTime finish, string team)
        {
            
            var fullReports = _repository.ReadReportsInInterval(companyId, teamMemberId, start, finish, team);

            return fullReports;
        }
        public AverageOldReportDto ReadAverageOldReports(int companyId, int teamMemberId, DateTime start,
            DateTime finish, string team, string filter)
        {
            var averageOldReports = _repository.ReadAverageOldReports(companyId, teamMemberId, start, finish, team, filter);
            if (averageOldReports.Count == 0)
            {
                return null;
            };
            var averageDtoReport = new AverageOldReportDto() { };
            averageDtoReport.StatusLevel = new int[10];
            switch (filter)
            {
                case "morale":
                    averageDtoReport.FilterName = "Morale";
                    break;
                case "stress":
                    averageDtoReport.FilterName = "Stress";
                    break;
                case "workload":
                    averageDtoReport.FilterName = "Workload";
                    break;
                case "overall":
                    averageDtoReport.FilterName = "Overall";
                    break;
                default:
                    averageDtoReport.FilterName = "Overall";
                    break;
            };
            var reports = averageOldReports.Select(report => {
                int weekIndex = (int)((finish - report.Date).TotalDays / 7);
                averageDtoReport.StatusLevel[weekIndex] = report.StatusLevel;
                return (WeekReportsDto)null;
            }).ToList();

            return averageDtoReport;
        }
        public ICollection<OverviewReportDto> ReadIndividualOldReports(int companyId, int memberId, DateTime start,
            DateTime finish, string team = "", string filter = "")
        {
            var currentMonday = DateTime.Now.FirstDateInWeek(IWeeklyReport.StartOfWeek);
            var startOfSearch = currentMonday.AddDays(-70);
            var reports = _repository.ReadIndividualOldReports(companyId, memberId, startOfSearch, currentMonday, team ,filter);
            if (reports.Count == 0)
            {
                return null;
            }

            var dict = new Dictionary<int, OverviewReportDto>() { };
            var weekReports = reports.Select(el => {
                if (!dict.ContainsKey(el.AuthorId))
                {
                    dict.Add(el.AuthorId, new OverviewReportDto()
                    {
                        AuthorId = el.AuthorId,
                        FirstName = el.FirstName,
                        LastName = el.LastName,
                    });
                    dict[el.AuthorId].StatusLevel = new int[10];
                }
                int weekIndex = (int)((currentMonday - el.Date).TotalDays / 7);
                dict[el.AuthorId].StatusLevel[weekIndex] = el.StatusLevel;
                return (WeekReportsDto)null;
            }).ToList();

            var result = new List<OverviewReportDto>(dict.Values) { };

            return result;
        }

    }
}
