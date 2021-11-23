using System;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class WeeklyReportTests
    {
        [Fact]
        public void ShouldCreateReport()
        {
            var moraleGrade = new Grade { Level = Level.VeryLow };
            var stressGrade = new Grade { Level = Level.Low };
            var workloadGrade = new Grade { Level = Level.Average, Commentary = "Usual amount of stress" };
            const string HighThisWeek = "My high this week";
            const string LowThisWeek = "My low this week";
            var reportDate = new DateTime(2021, 11, 10);
            var expectedWeekStart = new DateTime(2021, 11, 8);
            var expectedWeekEnd = new DateTime(2021, 11, 14);
            var anythingElse = "Anything else";

            var report = new WeeklyReport
            {
                ID = 1,
                AuthorId = 1,
                MoraleGradeId = 1,
                StressGradeId = 1,
                WorkloadGradeId = 1,
                MoraleGrade = moraleGrade,
                StressGrade = stressGrade,
                WorkloadGrade = workloadGrade,
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
            Assert.Equal(moraleGrade, report.MoraleGrade);
            Assert.Equal(stressGrade, report.StressGrade);
            Assert.Equal(workloadGrade, report.WorkloadGrade);
            Assert.Equal(HighThisWeek, report.HighThisWeek);
            Assert.Equal(LowThisWeek, report.LowThisWeek);
            Assert.Equal(reportDate, report.Date);
            Assert.Equal(expectedWeekStart, report.WeekStartDate);
            Assert.Equal(expectedWeekEnd, report.WeekEndDate);
            Assert.Equal(anythingElse, report.AnythingElse);
        }

        [Fact]
        public void ShouldCreateEstimation()
        {
            var grade = new Grade {
                ID = 1,
                Level = Level.VeryLow, 
                Commentary = "Very Low" 
            };

            Assert.Equal(1, grade.ID);
            Assert.Equal(Level.VeryLow, grade.Level);
            Assert.Equal("Very Low", grade.Commentary);
        }
    }
}
