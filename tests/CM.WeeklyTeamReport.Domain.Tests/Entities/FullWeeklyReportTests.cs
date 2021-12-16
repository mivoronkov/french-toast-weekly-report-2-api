using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class FullWeeklyReportTests
    {
        [Fact]
        public void ShouldCreateFullReport()
        {
            const string HighThisWeek = "My high this week";
            const string LowThisWeek = "My low this week";
            var reportDate = new DateTime(2021, 11, 10);
            var anythingElse = "Anything else";
            var commentary = "nope";

            var report = new FullWeeklyReport
            {
                ID = 1,
                AuthorId = 1,
                MoraleGradeId = 1,
                StressGradeId = 1,
                WorkloadGradeId = 1,
                MoraleLevel = 1,
                StressLevel = 1,
                WorkloadLevel = 1,
                MoraleCommentary = commentary,
                StressCommentary = commentary,
                WorkloadCommentary = commentary,
                HighThisWeek = HighThisWeek,
                LowThisWeek = LowThisWeek,
                AnythingElse = anythingElse,
                Date = reportDate
            };
            Assert.Equal(1, report.ID);
            Assert.Equal(1, report.AuthorId);
            Assert.Equal(1, report.MoraleGradeId);
            Assert.Equal(1, report.StressGradeId);
            Assert.Equal(1, report.WorkloadGradeId);
            Assert.Equal(commentary, report.MoraleCommentary);
            Assert.Equal(commentary, report.StressCommentary);
            Assert.Equal(commentary, report.WorkloadCommentary);
            Assert.Equal(HighThisWeek, report.HighThisWeek);
            Assert.Equal(LowThisWeek, report.LowThisWeek);
            Assert.Equal(reportDate, report.Date);
            Assert.Equal(1, report.MoraleLevel);
            Assert.Equal(1, report.StressLevel);
            Assert.Equal(1, report.WorkloadLevel);
            Assert.Equal(anythingElse, report.AnythingElse);
        }
    }
}
