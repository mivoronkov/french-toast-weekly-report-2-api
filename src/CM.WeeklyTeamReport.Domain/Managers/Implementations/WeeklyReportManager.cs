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

        public ICollection<OverviewReportDto> ReadOldExtendedReports(int companyId, int teamMemberId, DateTime end)
        {
            var firstDate = end.AddDays(-70);
            var reports = _repository.ReadReportsInInterval(companyId, teamMemberId, firstDate, end);
            if (reports.Count == 0)
            {
                return null;
            }
            var authors = new HashSet<int>() { };
            var weekReports = reports.Select(el => {
                authors.Add(el.AuthorId);
                return _reportCommands.FullToWeekReportDto(el);
            }).ToList();

            var result = new List<OverviewReportDto>() { };
            authors.Select(id =>
            {
                result.Add(new OverviewReportDto()
                {
                    AuthorId = id,
                    FirstName = weekReports.First(el => el.AuthorId == id).FirstName,
                    LastName = weekReports.First(el => el.AuthorId == id).LastName,
                }); 
                return id;
            }).Count();

            var weekSep = new DateTime(firstDate.Ticks);
            for (int i=0; i < 10; i++)
            {
                var monday = weekSep.AddDays(7 * i).FirstDateInWeek(IWeeklyReport.StartOfWeek);             
                result.ForEach(el => {
                    var weeklyReport = weekReports.Find(report => (report.AuthorId == el.AuthorId) && (report.Date.ToShortDateString() == monday.ToShortDateString()));
                    el.MoraleGrade.Add(weeklyReport!=null? weeklyReport.MoraleLevel:0);
                    el.StressGrade.Add(weeklyReport != null ? weeklyReport.StressLevel : 0);
                    el.WorkloadGrade.Add(weeklyReport != null ? weeklyReport.WorkloadLevel : 0);
                });                           
            }

            return result;
        }
    }
}
