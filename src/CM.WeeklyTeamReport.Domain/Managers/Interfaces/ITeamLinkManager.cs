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
        public Task<ITeamLink> Create(int reportingTMId, int leaderTMId);
        public Task Delete(int reportingTMId, int leaderTMId);
        public Task<ICollection<ITeamLink>> ReadLeaders(int reportingTMId);
        public Task<ICollection<ITeamLink>> ReadReportingTMs(int leaderTMId);
        public Task<ITeamLink> ReadLink(int reportingTMId, int leaderTMId);
        public Task UpdateLeaders(int memberId, ICollection<int> oldLeaders, ICollection<int> newLeaders);
        public Task UpdateFollowers(int memberId, ICollection<int> oldFollowers, ICollection<int> newFollowers);
    }
}
