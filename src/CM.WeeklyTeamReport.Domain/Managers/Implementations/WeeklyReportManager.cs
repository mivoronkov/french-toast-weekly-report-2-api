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
        public async Task<IWeeklyReport> create(ReportsDto newWeeklyReport)
        {
            var newReport = _reportCommands.dtoToReport(newWeeklyReport);
            return await _repository.Create(newReport);
        }

        public async Task delete(ReportsDto reportDto)
        {
            var report = _reportCommands.dtoToReport(reportDto);
            await _repository.Delete(report);
        }

        public async Task<ICollection<ReportsDto>> readAll(int companyId, int teamMemberId)
        {
            var reports = await _repository.ReadAll(companyId, teamMemberId);
            if (reports.Count==0 )
            {
                return null;
            }
            var reportsDto = reports.Select(el => _reportCommands.fullReportToDto(el)).ToList();

            return reportsDto;
        }

        public async Task<ReportsDto> read(int companyId, int teamMemberId, int reportId)
        {
            var report = await _repository.Read(companyId, teamMemberId, reportId);
            if (report == null)
            {
                return null;
            }
            var reportDto = _reportCommands.fullReportToDto(report);

            return reportDto;
        }

        public async Task update(ReportsDto oldEntity, ReportsDto newEntity)
        {
            newEntity.ID = oldEntity.ID;
            var report = _reportCommands.dtoToReport(newEntity);
            await _repository.Update(report);
        }

        public async Task<ICollection<ReportsDto>> ReadReportsInInterval(int companyId, int teamMemberId, DateTime start, DateTime end)
        {
            var firstDate = start;
            var lastDate = end;
            if (start> end)
            {
                firstDate = end;
                lastDate = start;
            }
            var reports = await _repository.ReadReportsInInterval(companyId, teamMemberId, firstDate, lastDate);
            if (reports.Count == 0)
            {
                return null;
            }
            var reportsDto = reports.Select(el => _reportCommands.fullReportToDto(el)).ToList();

            return reportsDto;
        }

        public async Task<ICollection<HistoryReportDto>> ReadReportHistory(int companyId, int teamMemberId, DateTime start, 
            DateTime finish, string team)
        {
            
            var fullReports = await _repository.ReadReportsInInterval(companyId, teamMemberId, start, finish, team);
            var historyReports = fullReports.Select(el => _reportCommands.fullToHistoryDto(el)).ToList();

            return historyReports;
        }
        public async Task<AverageOldReportDto> ReadAverageOldReports(int companyId, int teamMemberId, DateTime start,
            DateTime finish, string team, string filter)
        {
            var averageOldReports = await _repository.ReadAverageOldReports(companyId, teamMemberId, start, finish, team, filter);
            if (averageOldReports.Count == 0)
            {
                return null;
            };
            var averageDtoReport = new AverageOldReportDto() { };
            averageDtoReport.StatusLevel = new int[10];
            averageDtoReport.FilterName = filter switch
            {
                "morale" => "Morale",
                "stress" => "Stress",
                "workload" => "Workload",
                "overall" => "Overall",
                _ => "Overall",
            };
            ;
            var reports = averageOldReports.Select(report => {
                int weekIndex = averageDtoReport.StatusLevel.Length -1 - (int)((finish - report.Date).TotalDays / 7);
                averageDtoReport.StatusLevel[weekIndex] = report.StatusLevel;
                return (WeekReportsDto)null;
            }).ToList();

            return averageDtoReport;
        }
        public async Task<ICollection<OverviewReportDto>> ReadIndividualOldReports(int companyId, int memberId, DateTime start,
            DateTime finish, string team = "", string filter = "")
        {
            var reports = await _repository.ReadMemberOldReports(companyId, memberId, start, finish, team ,filter);
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
                int weekIndex = dict[el.AuthorId].StatusLevel.Length - 1 - (int)((finish - el.Date).TotalDays / 7);
                dict[el.AuthorId].StatusLevel[weekIndex] = el.StatusLevel;
                return (WeekReportsDto)null;
            }).ToList();

            var result = new List<OverviewReportDto>(dict.Values) { };

            return result;
        }

    }
}
