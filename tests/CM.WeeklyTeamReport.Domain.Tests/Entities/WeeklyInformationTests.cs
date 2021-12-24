using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class WeeklyInformationTests
    {
        [Fact]
        public void ShouldCreateWeeklyInformation()
        {
            var enyity = new WeeklyInformation
            {
                Comments="q",
                StateName="w",
                StateLevel=1
            };
            Assert.Equal("q", enyity.Comments);
            Assert.Equal("w", enyity.StateName);
            Assert.Equal(1, enyity.StateLevel);
        }
    }
}
