using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class SummaryOldReportTests
    {
        [Fact]
        public void ShouldCreateSummaryOldReport()
        {
            var averageOldReportDto = new AverageOldReportDto() { FilterName = "q", StatusLevel = new int[] { 1} };
            var overviewReportDto = new OverviewReportDto() { AuthorId = 1, FirstName = "z", LastName = "x", StatusLevel = new int[] { 5 } };
            var overviewReportsDtos = new List<OverviewReportDto>() { overviewReportDto };

            var summaryOldReport = new SummaryOldReport
            {
               AverageOldReportDto= averageOldReportDto,
               OverviewReportsDtos= overviewReportsDtos
            };
            Assert.Equal("q", summaryOldReport.AverageOldReportDto.FilterName);
            Assert.Equal(1, summaryOldReport.AverageOldReportDto.StatusLevel[0]);
            Assert.Single(summaryOldReport.AverageOldReportDto.StatusLevel);
            Assert.Equal(1, summaryOldReport.OverviewReportsDtos.Count);
            var enumerator = summaryOldReport.OverviewReportsDtos.GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(1, enumerator.Current.AuthorId);
            Assert.Equal("z", enumerator.Current.FirstName);
            Assert.Equal("x", enumerator.Current.LastName);
            Assert.Single(enumerator.Current.StatusLevel);
            Assert.Equal(5, enumerator.Current.StatusLevel[0]);
        }
    }
}