using CM.WeeklyTeamReport.Domain.Dto.Interfaces;
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
        public IWeeklyReport create(IWeeklyReportDto newWeeklyReport);
        public IWeeklyReportDto read(int companyId, int teamMemberId, int WeeklyReportId);
        public void update(IWeeklyReportDto oldEntity, IWeeklyReportDto newEntity);
        public void delete(int companyId, int teamMemberId, int WeeklyReportId);
        public ICollection<IWeeklyReportDto> readAll(int companyId, int teamMemberId);
    }
}
