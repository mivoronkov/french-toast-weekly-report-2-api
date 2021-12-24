using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class AverageOldReportDtoTests
    {
        [Fact]
        public void ShouldCreateAverageOldReportDto()
        {
            var averageOldReportDto = new AverageOldReportDto { };
            Assert.Null(averageOldReportDto.StatusLevel);
            Assert.Null(averageOldReportDto.FilterName);
        }
        [Fact]
        public void ShouldCreateDtoWithLevels()
        {
            int[] arr = new int[] { 1};
            var averageOldReportDto = new AverageOldReportDto 
            {
                StatusLevel= arr,
                FilterName = "Ole"
            };
            Assert.Equal(arr,averageOldReportDto.StatusLevel);
            Assert.Equal("Ole", averageOldReportDto.FilterName);
        }
    }
}