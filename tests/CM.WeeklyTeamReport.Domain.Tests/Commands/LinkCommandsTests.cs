using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class LinkCommandsTests
    {
        public LinkCommands commands = new LinkCommands();
        [Fact]
        public void ShouldBeDifference()
        {
            int[] expected = new[] { 4, 5 };
            int[] substr = new[] { 1, 2 };
            int[] source = new[] { 1, 2, 4, 5 };


            var result = commands.LinksDifference(substr, source);
            var en = result.GetEnumerator();
            en.MoveNext();
            Assert.Equal(expected[0], en.Current);
            en.MoveNext();
            Assert.Equal(expected[1], en.Current);
        }
    }
}
