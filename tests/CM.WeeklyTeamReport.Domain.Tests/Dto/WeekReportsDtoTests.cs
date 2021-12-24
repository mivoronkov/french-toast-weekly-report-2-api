using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class WeekReportsDtoTests
    {
        [Fact]
        public void ShouldCreateDto()
        {
            var today = new DateTime();
            var dto = new WeekReportsDto { 
                AuthorId=1,
                FirstName="q",
                LastName="w",
                ID=1,
                MoraleLevel=3,
                StressLevel=3,
                WorkloadLevel=4,
                Date= today
            };
            Assert.Equal(1, dto.AuthorId);
            Assert.Equal(3, dto.MoraleLevel);
            Assert.Equal(3, dto.StressLevel);
            Assert.Equal(4, dto.WorkloadLevel);
            Assert.Equal("w", dto.LastName);
            Assert.Equal("q", dto.FirstName);
            Assert.Equal(today, dto.Date);
        }
    }
}