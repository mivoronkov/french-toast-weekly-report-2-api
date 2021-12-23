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

        public ICollection<IFullWeeklyReport> ReadReportHistory(int companyId, int teamMemberId, string team, string week)
        {
            var searchingDate = week switch
            {
                "current" => DateTime.Now,
                "previus" => DateTime.Now.AddDays(-7),
                _ => DateTime.Now,
            };
            var searchingMonday = searchingDate.FirstDateInWeek(IWeeklyReport.StartOfWeek);
            var fullReports = _repository.ReadReportsInInterval(companyId, teamMemberId, searchingMonday, searchingMonday);

            return fullReports;
        }
        public AverageOldReportDto ReadAverageOldReports(int companyId, int teamMemberId, string team, string filter)
        {
            var currentMonday = DateTime.Now.FirstDateInWeek(IWeeklyReport.StartOfWeek);
            var startOfSearch = currentMonday.AddDays(-70);
            var averageOldReports = _repository.ReadAverageOldReports(companyId, teamMemberId, startOfSearch, currentMonday, team, filter);
            if (averageOldReports.Count == 0)
            {
                return null;
            };
            var averageDtoReport = new AverageOldReportDto() { };
            switch (filter)
            {
                case "morale":
                    averageDtoReport.MoraleLevel = new int[10];
                    break;
                case "stress":
                    averageDtoReport.StressLevel = new int[10];
                    break;
                case "workload":
                    averageDtoReport.WorkloadLevel = new int[10];
                    break;
                case "overall":
                    averageDtoReport.Overall = new int[10];
                    break;
                default:
                    averageDtoReport.Overall = new int[10];
                    break;
            };
            var reports = averageOldReports.Select(report => {
                int weekIndex = (int)((currentMonday - report.Date).TotalDays / 7);
                switch (filter)
                {
                    case "morale":
                        averageDtoReport.MoraleLevel[weekIndex] = (int)report.MoraleLevel;
                        break;
                    case "stress":
                        averageDtoReport.StressLevel[weekIndex] = (int)report.StressLevel;
                        break;
                    case "workload":
                        averageDtoReport.WorkloadLevel[weekIndex] = (int)report.WorkloadLevel;
                        break;
                    case "overall":
                        averageDtoReport.Overall[weekIndex] = (int)report.Overall;
                        break;
                    default:
                        averageDtoReport.Overall[weekIndex] = (int)report.Overall;
                        break;
                };
                return (WeekReportsDto)null;
            }).ToList();

            return averageDtoReport;
        }
        public ICollection<OverviewReportDto> ReadIndividualOldReports(int companyId, int memberId, string team = "", string filter = "")
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
                    switch (filter)
                    {
                        case "morale":
                            dict[el.AuthorId].MoraleLevel =  new int[10];
                            break;
                        case "stress":
                            dict[el.AuthorId].StressLevel = new int[10];
                            break;
                        case "workload":
                            dict[el.AuthorId].WorkloadLevel = new int[10];
                            break;
                        case "overall":
                            dict[el.AuthorId].Overall = new int[10];
                            break;
                        default:
                            dict[el.AuthorId].Overall = new int[10];
                            break;
                    };
                }
                int weekIndex = (int)((currentMonday - el.Date).TotalDays / 7);
                switch (filter)
                {
                    case "morale":
                        dict[el.AuthorId].MoraleLevel[weekIndex] = el.MoraleLevel;
                        break;
                    case "stress":
                        dict[el.AuthorId].StressLevel[weekIndex] = el.StressLevel;
                        break;
                    case "workload":
                        dict[el.AuthorId].WorkloadLevel[weekIndex] = el.WorkloadLevel;
                        break;
                    case "overall":
                        dict[el.AuthorId].Overall[weekIndex] = el.Overall;
                        break;
                    default:
                        dict[el.AuthorId].Overall[weekIndex] = el.Overall;
                        break;
                };
                return (WeekReportsDto)null;
            }).ToList();

            var result = new List<OverviewReportDto>(dict.Values) { };

            return result;
        }

    }
}
