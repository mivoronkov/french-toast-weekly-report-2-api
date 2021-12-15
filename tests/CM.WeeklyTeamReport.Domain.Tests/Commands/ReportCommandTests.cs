using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Dto.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class ReportCommandTests
    {
        public ReportCommands commands = new ReportCommands()
;        [Fact]
        public void ShouldCreatReportFromDto()
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

            var reportDto = new ReportsDto
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
            var report = commands.dtoToReport(reportDto);
            Assert.Equal(reportDto.ID, report.ID);
            Assert.Equal(reportDto.AuthorId, report.AuthorId);
            Assert.Equal(reportDto.MoraleGradeId, report.MoraleGradeId);
            Assert.Equal(reportDto.StressGradeId, report.StressGradeId);
            Assert.Equal(reportDto.WorkloadGradeId, report.WorkloadGradeId);
            Assert.Equal(reportDto.MoraleGrade, report.MoraleGrade);
            Assert.Equal(reportDto.StressGrade, report.StressGrade);
            Assert.Equal(reportDto.WorkloadGrade, report.WorkloadGrade);
            Assert.Equal(reportDto.HighThisWeek, report.HighThisWeek);
            Assert.Equal(reportDto.LowThisWeek, report.LowThisWeek);
            Assert.Equal(reportDto.Date, report.Date);
            Assert.Equal(reportDto.AnythingElse, report.AnythingElse);
        }
        [Fact]
        public void ShouldCreateDtoFromReport()
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
            var reportDto = commands.reportToDto(report);
            Assert.Equal(reportDto.ID, report.ID);
            Assert.Equal(reportDto.AuthorId, report.AuthorId);
            Assert.Equal(reportDto.MoraleGradeId, report.MoraleGradeId);
            Assert.Equal(reportDto.StressGradeId, report.StressGradeId);
            Assert.Equal(reportDto.WorkloadGradeId, report.WorkloadGradeId);
            Assert.Equal(reportDto.MoraleGrade, report.MoraleGrade);
            Assert.Equal(reportDto.StressGrade, report.StressGrade);
            Assert.Equal(reportDto.WorkloadGrade, report.WorkloadGrade);
            Assert.Equal(reportDto.HighThisWeek, report.HighThisWeek);
            Assert.Equal(reportDto.LowThisWeek, report.LowThisWeek);
            Assert.Equal(reportDto.Date, report.Date);
            Assert.Equal(reportDto.AnythingElse, report.AnythingElse);
        }
    }
}
