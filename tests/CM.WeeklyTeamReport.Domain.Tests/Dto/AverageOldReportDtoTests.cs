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
            Assert.Null(averageOldReportDto.MoraleLevel);
            Assert.Null(averageOldReportDto.FilterName);
            Assert.Null(averageOldReportDto.Overall);
            Assert.Null(averageOldReportDto.StressLevel);
            Assert.Null(averageOldReportDto.WorkloadLevel);
        }
        [Fact]
        public void ShouldCreateDtoWithLevels()
        {
            int[] arr = new int[] { 1};
            var averageOldReportDto = new AverageOldReportDto 
            {
                MoraleLevel= arr,
                Overall= arr,
                StressLevel= arr,
                WorkloadLevel= arr,
                FilterName = "Ole"
            };
            Assert.Equal(arr,averageOldReportDto.MoraleLevel);
            Assert.Equal(arr, averageOldReportDto.Overall);
            Assert.Equal(arr, averageOldReportDto.StressLevel);
            Assert.Equal(arr, averageOldReportDto.WorkloadLevel);
            Assert.Equal("Ole", averageOldReportDto.FilterName);
        }
    }
}