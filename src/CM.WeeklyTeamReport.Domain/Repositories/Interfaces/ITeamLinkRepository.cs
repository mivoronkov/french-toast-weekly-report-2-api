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
        public ITeamLink ReadLink(int reportingTMId, int leaderTMId);
        public ICollection<ITeamLink> ReadLeaders(int reportingTMId);
        public ICollection<ITeamLink> ReadReportingTMs(int leaderTMId);
        public void DeleteLiders(int memberId, IEnumerable<int> removingLeaders);
        public void AddLeaders(int memberId, IEnumerable<int> addingLeaders);
        public void DeleteFollowers(int memberId, IEnumerable<int> removingFollowers);
        public void AddFollowers(int memberId, IEnumerable<int> addingFollowers);
    }
}
