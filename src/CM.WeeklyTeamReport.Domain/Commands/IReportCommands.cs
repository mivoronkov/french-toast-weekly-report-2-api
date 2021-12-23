using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Dto.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Commands
{
    public interface IReportCommands
    {
        public ReportsDto reportToDto(IWeeklyReport report);
        public ReportsDto fullReportToDto(IFullWeeklyReport fullReport);
        public IWeeklyReport dtoToReport(ReportsDto reportsDto);
        public WeekReportsDto FullToWeekReportDto(IFullWeeklyReport fullReport);
        public ICollection<OverviewReportDto> FullToOverviewRepor(ICollection<IFullWeeklyReport> fullReports, DateTime monday);
    }
}
