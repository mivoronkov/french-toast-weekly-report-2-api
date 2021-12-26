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
        public Task<IWeeklyReport> Create(IWeeklyReport entity);

        public Task<IWeeklyReport> Read(int entityId);
        public Task<IFullWeeklyReport> Read(int companyId, int authorId, int entityId);

        public Task Update(IWeeklyReport entity);

        public Task Delete(IWeeklyReport entity);

        public Task Delete(int entityId);

        public Task<ICollection<IWeeklyReport>> ReadAll();
        public Task<ICollection<IFullWeeklyReport>> ReadAll(int companyId, int authorId);
        public Task<ICollection<IFullWeeklyReport>> ReadReportsInInterval(int companyId, int memberId,DateTime start, DateTime end, string team="");
        public Task<ICollection<IOldReport>> ReadAverageOldReports(int companyId, int memberId, DateTime firstDate, DateTime lastDate, string team = "", string filter = "");
        public Task<ICollection<IIndividualOldReport>> ReadMemberOldReports(int companyId, int memberId, DateTime firstDate, DateTime lastDate, string team = "", string filter = "");

    }
}