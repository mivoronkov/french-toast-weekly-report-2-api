using CM.WeeklyTeamReport.Domain.Dto.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Managers
{
    public class WeeklyReportManager : IWeeklyReportManager
    {
        private readonly IWeeklyReportRepository _repository;

        public WeeklyReportManager(IWeeklyReportRepository weeklyReportRepository)
        {
            _repository = weeklyReportRepository;
        }
        public IWeeklyReport create(ReportsDto newWeeklyReport)
        {
            var newReport = dtoToReport(newWeeklyReport);
            return _repository.Create(newReport);
        }

        public void delete(int companyId, int teamMemberId, int weeklyReportId)
        {
            _repository.Delete(weeklyReportId);
        }

        public ICollection<ReportsDto> readAll(int companyId, int teamMemberId)
        {
            var reports =_repository.ReadAll(companyId, teamMemberId);
            var reportsDto = reports.Select(el => reportToDto(el)).ToList();

            return reportsDto;
        }

        public ReportsDto read(int companyId, int teamMemberId, int reportId)
        {
            var report = _repository.Read(companyId, teamMemberId, reportId);
            var reportDto = reportToDto(report);

            return reportDto;
        }

        public void update(ReportsDto oldEntity, ReportsDto newEntity)
        {
            newEntity.ID = oldEntity.ID;
            var report = dtoToReport(newEntity);
            _repository.Update(report);
        }

        private ReportsDto reportToDto(IWeeklyReport report)
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
        private IWeeklyReport dtoToReport(ReportsDto reportsDto)
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
