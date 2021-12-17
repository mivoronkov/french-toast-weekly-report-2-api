using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface ITeamLinkRepository
    {
        public ITeamLink Create(int reportingTMId, int leaderTMId);
        public void Delete(int reportingTMId, int leaderTMId);
        public ICollection<ITeamLink> ReadLeaders(int reportingTMId);
        public ICollection<ITeamLink> ReadReportingTMs(int leaderTMId);
    }
}
