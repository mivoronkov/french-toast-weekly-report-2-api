using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Entities.Implementations
{
    public class TeamLink : ITeamLink
    {
        public int ReportingTMId { get ; set; }
        public int LeaderTMId { get; set; }
        public TeamLink() { }
    }
}
