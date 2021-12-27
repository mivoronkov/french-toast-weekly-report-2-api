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
        public Task<ITeamLink> Create(int reportingTMId, int leaderTMId);
        public Task Delete(int reportingTMId, int leaderTMId);
        public Task<ITeamLink> ReadLink(int reportingTMId, int leaderTMId);
        public Task<ICollection<ITeamLink>> ReadLeaders(int reportingTMId);
        public Task<ICollection<ITeamLink>> ReadReportingTMs(int leaderTMId);
        public Task DeleteLiders(int memberId, IEnumerable<int> removingLeaders);
        public Task AddLeaders(int memberId, IEnumerable<int> addingLeaders);
        public Task DeleteFollowers(int memberId, IEnumerable<int> removingFollowers);
        public Task AddFollowers(int memberId, IEnumerable<int> addingFollowers);
    }
}
