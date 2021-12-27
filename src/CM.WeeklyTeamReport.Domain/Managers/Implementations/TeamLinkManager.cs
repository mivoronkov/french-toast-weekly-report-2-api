using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public class TeamLinkManager: ITeamLinkManager
    {
        ITeamLinkRepository _repository { get; }
        public TeamLinkManager(ITeamLinkRepository repository)
        {
            _repository = repository;
        }

        public async Task<ITeamLink> Create(int reportingTMId, int leaderTMId) 
        {
            var newLink = await _repository.Create(reportingTMId, leaderTMId);
            return newLink;
        }
        public async Task Delete(int reportingTMId, int leaderTMId)
        {
            await _repository.Delete(reportingTMId, leaderTMId);
        }
        public async Task<ICollection<ITeamLink>> ReadLeaders(int reportingTMId)
        {
            var result = await _repository.ReadLeaders(reportingTMId);
            return result.Count > 0 ? result : null;
        }
        public async Task<ICollection<ITeamLink>> ReadReportingTMs(int leaderTMId)
        {
            var result = await _repository.ReadReportingTMs(leaderTMId);
            return result.Count > 0 ? result : null;
        }
        public async Task<ITeamLink> ReadLink(int reportingTMId, int leaderTMId)
        {
            var result = await _repository.ReadLink(reportingTMId, leaderTMId);
            return result;
        }

        public async Task UpdateLeaders(int memberId, ICollection<int> oldLeaders, ICollection<int> newLeaders)
        {
            var removingLeaders = oldLeaders.Except(newLeaders);
            var addingLeaders = newLeaders.Except(oldLeaders);
            if (removingLeaders.Count() > 0)
            {
                await _repository.DeleteLiders(memberId, removingLeaders);
            }
            if (addingLeaders.Count() > 0)
            {
                await _repository.AddLeaders(memberId, addingLeaders);
            }
        }

        public async Task UpdateFollowers(int memberId, ICollection<int> oldFollowers, ICollection<int> newFollowers)
        {
            var removingFollowers = oldFollowers.Except(newFollowers);
            var addingFollowers = newFollowers.Except(oldFollowers);
            if (removingFollowers.Count() > 0)
            {
                await _repository.DeleteFollowers(memberId, removingFollowers);
            }
            if (addingFollowers.Count() > 0)
            {
                await _repository.AddFollowers(memberId, addingFollowers);
            }
        }
    }
}
