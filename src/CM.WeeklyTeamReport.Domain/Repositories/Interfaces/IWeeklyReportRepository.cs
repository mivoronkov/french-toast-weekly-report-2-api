using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface IWeeklyReportRepository
    {
        public IWeeklyReport Create(IWeeklyReport entity);

        public IWeeklyReport Read(int entityId);
        public IFullWeeklyReport Read(int companyId, int authorId, int entityId);

        public void Update(IWeeklyReport entity);

        public void Delete(IWeeklyReport entity);

        public void Delete(int entityId);

        public ICollection<IWeeklyReport> ReadAll();
        public ICollection<IFullWeeklyReport> ReadAll(int companyId, int authorId);
        public ICollection<IFullWeeklyReport> ReadReportsInInterval(int companyId, int memberId,DateTime start, DateTime end, string team="");
        public ICollection<IOldReport> ReadAverageOldReports(int companyId, int memberId, DateTime firstDate, DateTime lastDate, string team = "", string filter = "");
        public ICollection<IIndividualOldReport> ReadMemberOldReports(int companyId, int memberId, DateTime firstDate, DateTime lastDate, string team = "", string filter = "");

    }
}