using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public class TeamLinkManager : ITeamLinkManager
    {
        ITeamLinkRepository _repository { get; }
        ILinkCommands _commands { get; }

        public TeamLinkManager(ITeamLinkRepository repository, ILinkCommands commands)
        {
            _repository = repository;
            _commands = commands;
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
            return result;
        }
        public async Task<ICollection<ITeamLink>> ReadReportingTMs(int leaderTMId)
        {
            var result = await _repository.ReadReportingTMs(leaderTMId);
            return result;
        }
        public async Task<ITeamLink> ReadLink(int reportingTMId, int leaderTMId)
        {
            var result = await _repository.ReadLink(reportingTMId, leaderTMId);
            return result;
        }

        public async Task UpdateLeaders(int memberId, ICollection<int> oldLeaders, ICollection<int> newLeaders)
        {
            var removingLeaders = _commands.LinksDifference(newLeaders, oldLeaders);
            var addingLeaders = _commands.LinksDifference(oldLeaders, newLeaders);
            if (removingLeaders.Any())
            {
                await _repository.DeleteLeaders(memberId, removingLeaders);
            }
            if (addingLeaders.Any())
            {
                await _repository.AddLeaders(memberId, addingLeaders);
            }
        }

        public async Task UpdateFollowers(int memberId, ICollection<int> oldFollowers, ICollection<int> newFollowers)
        {
            var removingFollowers = _commands.LinksDifference(newFollowers, oldFollowers);
            var addingFollowers = _commands.LinksDifference(oldFollowers, newFollowers);
            if (removingFollowers.Any())
            {
                await _repository.DeleteFollowers(memberId, removingFollowers);
            }
            if (addingFollowers.Any())
            {
                await _repository.AddFollowers(memberId, addingFollowers);
            }
        }
    }
}
