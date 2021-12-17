using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class TeamLinkTests
    {
        [Fact]
        public void ShouldCreateTeamLink()
        {
            var teamLink = new TeamLink
            {
                ReportingTMId = 1,
                LeaderTMId = 2,
            };
            Assert.Equal(1, teamLink.ReportingTMId);
            Assert.Equal(2, teamLink.LeaderTMId);
        }
    }
}
