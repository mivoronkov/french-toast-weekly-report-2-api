using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Dto.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Commands
{
    public class ReportCommands: IReportCommands
    {
        public ReportsDto reportToDto(IWeeklyReport report)
        {
            var reportsDto = new ReportsDto();
            reportsDto.HighThisWeek = report.HighThisWeek;
            reportsDto.Date = report.Date;
            reportsDto.AnythingElse = report.AnythingElse;
            reportsDto.AuthorId = report.AuthorId;
            reportsDto.LowThisWeek = report.LowThisWeek;
            reportsDto.MoraleGrade = (Grade)report.MoraleGrade;
            reportsDto.WorkloadGradeId = report.WorkloadGradeId;
            reportsDto.WorkloadGrade = (Grade)report.WorkloadGrade;
            reportsDto.StressGradeId = report.StressGradeId;
            reportsDto.StressGrade = (Grade)report.StressGrade;
            reportsDto.MoraleGradeId = report.MoraleGradeId;
            reportsDto.ID = report.ID;

            return reportsDto;
        }
        public IWeeklyReport dtoToReport(ReportsDto reportsDto)
        {
            var report = new WeeklyReport();
            report.HighThisWeek = reportsDto.HighThisWeek;
            report.Date = reportsDto.Date;
            report.AnythingElse = reportsDto.AnythingElse;
            report.AuthorId = reportsDto.AuthorId;
            report.LowThisWeek = reportsDto.LowThisWeek;
            report.MoraleGrade = reportsDto.MoraleGrade;
            report.WorkloadGradeId = reportsDto.WorkloadGradeId;
            report.WorkloadGrade = reportsDto.WorkloadGrade;
            report.StressGradeId = reportsDto.StressGradeId;
            report.StressGrade = reportsDto.StressGrade;
            report.MoraleGradeId = reportsDto.MoraleGradeId;
            report.ID = reportsDto.ID;

            return report;
        }

        public ReportsDto fullReportToDto(IFullWeeklyReport fullReport)
        {
            var reportsDto = new ReportsDto();
            reportsDto.HighThisWeek = fullReport.HighThisWeek;
            reportsDto.Date = fullReport.Date;
            reportsDto.AnythingElse = fullReport.AnythingElse;
            reportsDto.AuthorId = fullReport.AuthorId;
            reportsDto.LowThisWeek = fullReport.LowThisWeek;
            reportsDto.MoraleGradeId = fullReport.MoraleGradeId;
            reportsDto.WorkloadGradeId = fullReport.WorkloadGradeId;
            reportsDto.StressGradeId = fullReport.StressGradeId;
            reportsDto.ID = fullReport.ID;
            reportsDto.MoraleGrade = new Grade{
                ID = fullReport.MoraleGradeId, 
                Level = (Level)fullReport.MoraleLevel, 
                Commentary = fullReport.MoraleCommentary
            };
            reportsDto.WorkloadGrade = new Grade
            {
                ID = fullReport.WorkloadGradeId,
                Level = (Level)fullReport.WorkloadLevel,
                Commentary = fullReport.WorkloadCommentary
            };
            reportsDto.StressGrade = new Grade
            {
                ID = fullReport.StressGradeId,
                Level = (Level)fullReport.StressLevel,
                Commentary = fullReport.StressCommentary
            };

            return reportsDto;
        }

        public HistoryReportDto fullToHistoryDto(IFullWeeklyReport fullReport)
        {
            var moraleInformation = new WeeklyInformation()
            {
                StateLevel = fullReport.MoraleLevel,
                Comments = fullReport.MoraleCommentary,
                StateName = "Morale"
            };
            var stressInformation = new WeeklyInformation()
            {
                StateLevel = fullReport.StressLevel,
                Comments = fullReport.StressCommentary,
                StateName = "Stress"
            };
            var workloadInformation = new WeeklyInformation()
            {
                StateLevel = fullReport.WorkloadLevel,
                Comments = fullReport.WorkloadCommentary,
                StateName = "Workload"
            };
            var informationList = new List<IWeeklyInformation>() { moraleInformation, stressInformation, workloadInformation };
            var highNotations = new WeeklyNotations() { Title = "Weekly High", Text = fullReport.HighThisWeek };
            var lowNotations = new WeeklyNotations() { Title = "Weekly Low", Text = fullReport.LowThisWeek };
            var anythingNotations = new WeeklyNotations() { Title = "Anything Else", Text = fullReport.AnythingElse };
            var notationsList = new List<IWeeklyNotations>() { highNotations, lowNotations, anythingNotations };

            var historyReportDto = new HistoryReportDto()
            {
                ID = fullReport.ID,
                FirstName = fullReport.FirstName,
                LastName = fullReport.LastName,
                WeeklyInformation = informationList,
                WeeklyNotations = notationsList
            };
            return historyReportDto;

        }
    }
}
