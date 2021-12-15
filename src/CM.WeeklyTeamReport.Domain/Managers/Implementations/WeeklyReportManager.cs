using CM.WeeklyTeamReport.Domain.Commands;
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

        public WeeklyReportManager(IWeeklyReportRepository weeklyReportRepository)
        {
            _repository = weeklyReportRepository;
        }
        public IWeeklyReport create(ReportsDto newWeeklyReport)
        {
            var newReport = ReportCommands.dtoToReport(newWeeklyReport);
            return _repository.Create(newReport);
        }

        public void delete(int companyId, int teamMemberId, int weeklyReportId)
        {
            _repository.Delete(weeklyReportId);
        }

        public ICollection<ReportsDto> readAll(int companyId, int teamMemberId)
        {
            var reports =_repository.ReadAll(companyId, teamMemberId);
            var reportsDto = reports.Select(el => ReportCommands.reportToDto(el)).ToList();

            return reportsDto;
        }

        public ReportsDto read(int companyId, int teamMemberId, int reportId)
        {
            var report = _repository.Read(companyId, teamMemberId, reportId);
            var reportDto = ReportCommands.reportToDto(report);

            return reportDto;
        }

        public void update(ReportsDto oldEntity, ReportsDto newEntity)
        {
            newEntity.ID = oldEntity.ID;
            var report = ReportCommands.dtoToReport(newEntity);
            _repository.Update(report);
        }   
    }
}
