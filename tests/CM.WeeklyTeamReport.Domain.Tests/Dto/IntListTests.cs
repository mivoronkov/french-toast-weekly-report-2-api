using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class IntListDtoTests
    {
        [Fact]
        public void ShouldCreateIntListDtoDto()
        { 
            var intListDto = new IntListDto
            {
                Followers = new List<int>() { 1,2},
                Leaders = new List<int>() { 2, 3 },
            };
            Assert.Equal(1, intListDto.Followers[0]);
            Assert.Equal(2, intListDto.Followers[1]);
            Assert.Equal(2, intListDto.Leaders[0]);
            Assert.Equal(3, intListDto.Leaders[1]);
        }
    }
}