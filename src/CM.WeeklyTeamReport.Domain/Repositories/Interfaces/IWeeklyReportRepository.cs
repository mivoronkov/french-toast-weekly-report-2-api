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

        public void Update(IWeeklyReport entity);

        public void Delete(IWeeklyReport entity);

        public void Delete(int entityId);

        public ICollection<IWeeklyReport> ReadAll();
    }
}