using CM.WeeklyTeamReport.Domain.Dto.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Commands
{
    public class ReportCommands
    {
        public ReportsDto reportToDto(IWeeklyReport report)
        {
            var reportsDto = new ReportsDto();
            reportsDto.HighThisWeek = report.HighThisWeek;
            reportsDto.Date = report.Date;
            reportsDto.AnythingElse = report.AnythingElse;
            reportsDto.AuthorId = report.AuthorId;
            reportsDto.LowThisWeek = report.LowThisWeek;
            reportsDto.MoraleGrade = report.MoraleGrade;
            reportsDto.WorkloadGradeId = report.WorkloadGradeId;
            reportsDto.WorkloadGrade = report.WorkloadGrade;
            reportsDto.StressGradeId = report.StressGradeId;
            reportsDto.StressGrade = report.StressGrade;
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
    }
}
