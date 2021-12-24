using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class OverviewReportDtoTests
    {
        [Fact]
        public void ShouldCreateDto()
        {
            var arr = new int[] { 1 };
            var dto = new OverviewReportDto
            { 
                AuthorId=1,
                FirstName="q",
                LastName="w",
                StatusLevel = arr,
            };
            Assert.Equal(1, dto.AuthorId);
            Assert.Equal(arr, dto.StatusLevel);
            Assert.Equal("w", dto.LastName);
            Assert.Equal("q", dto.FirstName);
        }
    }
}