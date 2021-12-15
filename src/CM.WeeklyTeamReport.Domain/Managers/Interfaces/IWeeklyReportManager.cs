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
        public IWeeklyReport create(ReportsDto newWeeklyReport);
        public ReportsDto read(int companyId, int teamMemberId, int WeeklyReportId);
        public void update(ReportsDto oldEntity, ReportsDto newEntity);
        public void delete(int companyId, int teamMemberId, int WeeklyReportId);
        public ICollection<ReportsDto> readAll(int companyId, int teamMemberId);
    }
}
