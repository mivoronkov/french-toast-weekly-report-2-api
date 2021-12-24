using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class WeeklyNotationsTests
    {
        [Fact]
        public void ShouldCreateWeeklyNotations()
        {
            var enyity = new WeeklyNotations
            {
                Text="q",
                Title="w"
            };
            Assert.Equal("q", enyity.Text);
            Assert.Equal("w", enyity.Title);
        }
    }
}
