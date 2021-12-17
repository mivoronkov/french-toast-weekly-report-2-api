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

        public ITeamLink Create(int reportingTMId, int leaderTMId) 
        {
            var newLink = _repository.Create(reportingTMId, leaderTMId);
            return newLink;
        }
        public void Delete(int reportingTMId, int leaderTMId)
        {
            _repository.Delete(reportingTMId, leaderTMId);
        }
        public ICollection<ITeamLink> ReadLeaders(int reportingTMId)
        {
            var result = _repository.ReadLeaders(reportingTMId);
            return result.Count > 0 ? result : null;
        }
        public ICollection<ITeamLink> ReadReportingTMs(int leaderTMId)
        {
            var result = _repository.ReadReportingTMs(leaderTMId);
            return result.Count > 0 ? result : null;
        }
    }
}
