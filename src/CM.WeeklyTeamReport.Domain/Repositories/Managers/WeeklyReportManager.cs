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
        public IWeeklyReport createWeeklyReport(IWeeklyReport newWeeklyReport)
        {
            throw new NotImplementedException();
        }

        public void deleteWeeklyReport(IWeeklyReport entity)
        {
            throw new NotImplementedException();
        }

        public void deleteWeeklyReport(int entityIdy)
        {
            throw new NotImplementedException();
        }

        public ICollection<IWeeklyReport> readAlWeeklyReports()
        {
            throw new NotImplementedException();
        }

        public IWeeklyReport readWeeklyReport(int companyId, int WeeklyReportId)
        {
            throw new NotImplementedException();
        }

        public IWeeklyReport readWeeklyReport(int WeeklyReportId)
        {
            throw new NotImplementedException();
        }

        public void updateWeeklyReport(IWeeklyReport entity)
        {
            throw new NotImplementedException();
        }
    }
}
