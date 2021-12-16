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
    }
}
