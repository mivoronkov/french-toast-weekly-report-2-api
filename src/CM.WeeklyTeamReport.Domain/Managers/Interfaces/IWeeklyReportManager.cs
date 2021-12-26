using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Dto.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface IWeeklyReportManager
    {
        public Task<IWeeklyReport> create(ReportsDto newWeeklyReport);
        public Task<ReportsDto> read(int companyId, int teamMemberId, int WeeklyReportId);
        public Task update(ReportsDto oldEntity, ReportsDto newEntity);
        public Task delete(ReportsDto reportsDto);
        public Task<ICollection<ReportsDto>> readAll(int companyId, int teamMemberId);
        public Task<ICollection<ReportsDto>> ReadReportsInInterval(int companyId, int teamMemberId,DateTime start, DateTime end);
        public Task< AverageOldReportDto> ReadAverageOldReports(int companyId, int teamMemberId, DateTime start,
            DateTime finish, string team, string filter);
        public Task<ICollection<HistoryReportDto>> ReadReportHistory(int companyId, int teamMemberId, DateTime start, DateTime finish, string team);
        public Task<ICollection<OverviewReportDto>> ReadIndividualOldReports(int companyId, int memberId, DateTime start, 
            DateTime finish, string team = "",string filter = "");
    }
}
