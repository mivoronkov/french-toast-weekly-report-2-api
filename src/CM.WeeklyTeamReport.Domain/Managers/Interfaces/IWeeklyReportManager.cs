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
        public IWeeklyReport createWeeklyReport(IWeeklyReport newWeeklyReport);
        public IWeeklyReport readWeeklyReport(int companyId,int WeeklyReportId);
        public IWeeklyReport readWeeklyReport(int WeeklyReportId);

        public ICollection<IWeeklyReport> readAlWeeklyReports();
        public void updateWeeklyReport(IWeeklyReport entity);
        public void deleteWeeklyReport(IWeeklyReport entity);
        public void deleteWeeklyReport(int entityIdy);
    }
}
