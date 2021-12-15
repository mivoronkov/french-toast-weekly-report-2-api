using CM.WeeklyTeamReport.Domain.Dto.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class ReportsDtoTests
    {
        [Fact]
        public void ShouldCreateReportsDto()
        {
            var moraleGrade = new Grade { Level = Level.VeryLow };
            var stressGrade = new Grade { Level = Level.Low };
            var workloadGrade = new Grade { Level = Level.Average, Commentary = "Usual amount of stress" };
            const string HighThisWeek = "My high this week";
            const string LowThisWeek = "My low this week";
            var reportDate = new DateTime(2021, 11, 10);
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
            Assert.Equal(1, reportDto.ID);
            Assert.Equal(1, reportDto.AuthorId);
            Assert.Equal(1, reportDto.MoraleGradeId);
            Assert.Equal(1, reportDto.StressGradeId);
            Assert.Equal(1, reportDto.WorkloadGradeId);
            Assert.Equal(moraleGrade, reportDto.MoraleGrade);
            Assert.Equal(stressGrade, reportDto.StressGrade);
            Assert.Equal(workloadGrade, reportDto.WorkloadGrade);
            Assert.Equal(HighThisWeek, reportDto.HighThisWeek);
            Assert.Equal(LowThisWeek, reportDto.LowThisWeek);
            Assert.Equal(reportDate, reportDto.Date);
            Assert.Equal(anythingElse, reportDto.AnythingElse);
        }        
    }
}
