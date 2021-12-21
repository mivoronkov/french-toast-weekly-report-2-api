using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface ITeamLinkManager
    {
        public ITeamLink Create(int reportingTMId, int leaderTMId);
        public void Delete(int reportingTMId, int leaderTMId);
        public ICollection<ITeamLink> ReadLeaders(int reportingTMId);
        public ICollection<ITeamLink> ReadReportingTMs(int leaderTMId);
        public ITeamLink ReadLink(int reportingTMId, int leaderTMId);
        public void UpdateLeaders(int memberId, ICollection<int> oldLeaders, ICollection<int> newLeaders);
        public void UpdateFollowers(int memberId, ICollection<int> oldFollowers, ICollection<int> newFollowers);
    }
}
