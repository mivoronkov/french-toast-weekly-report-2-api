using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Dto.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Managers.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.WebAPI.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/members/{memberId}/reports")]
    public class WeeklyReportController : ControllerBase
    {
        private readonly IWeeklyReportManager _manager;
        private readonly IDateTimeManager _dateTimeManager;

        public WeeklyReportController(IWeeklyReportManager weeklyReportManager, IDateTimeManager dateTimeManager)
        {
            _dateTimeManager = dateTimeManager;
            _manager = weeklyReportManager;
        }

        [HttpGet]
        [Route("current-reports")]
        public async Task<IActionResult> GetTeamReports([FromQuery(Name = "team")] string team, [FromQuery(Name = "week")] string week, 
            int companyId, int memberId)
        {
            var searchingDate = week switch
            {
                "current" => _dateTimeManager.TakeDateTime(0),
                "previous" => _dateTimeManager.TakeDateTime(-7),
                _ => _dateTimeManager.TakeDateTime(0),
            };
            var searchingMonday = _dateTimeManager.TakeMonday(searchingDate);
            var searchingSunday = _dateTimeManager.TakeSunday(searchingDate);
            var result = await _manager.ReadReportHistory(companyId, memberId, searchingMonday, searchingSunday, team);          
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("old-reports")]
        public async Task<IActionResult> GetOldReports([FromQuery(Name = "team")] string team, [FromQuery(Name = "filter")] string filter, 
            int companyId, int memberId)
        {
            var currentMonday = _dateTimeManager.TakeMonday();
            var startOfSearch = _dateTimeManager.TakeMonday(-70);

            var averageReport = await _manager.ReadAverageOldReports(companyId, memberId, startOfSearch, currentMonday, team, filter);            
            var individualReports = await _manager.ReadIndividualOldReports(companyId, memberId, startOfSearch, currentMonday, team, filter);

            if (averageReport == null || individualReports == null)
            {
                return NotFound();
            }
            var result = new SummaryOldReport()
            {
                AverageOldReportDto = averageReport,
                OverviewReportsDtos = individualReports
            };
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "shouldFormatReports")] bool shouldFormatReports, int companyId, int memberId)
        {
            var result = await _manager.readAll(companyId, memberId);
            if (result == null)
            {
                return NotFound();
            }
            if (shouldFormatReports)
            {
                var reportCommands = new ReportCommands();
                var formattedResult = result.Select(el => reportCommands.reportToPersonalReportDto(el)).ToList();
                return Ok(formattedResult);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{reportId}")]
        public async Task<IActionResult> Get(int companyId, int memberId, int reportId)
        {
            var result = await _manager.read(companyId, memberId, reportId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReportsDto entity, int companyId, int memberId)
        {
            var result = await _manager.create(entity);
            if (result == null)
            {
                return NoContent();
            }
            var uriCreatedReport = $"api/companies/{companyId}/members/{memberId}/reports/{result.ID}";
            return Created(uriCreatedReport, result);
        }

        [HttpPut]
        [Route("{reportId}")]
        public async Task<IActionResult> Put([FromBody] ReportsDto entity, int companyId, int memberId, int reportId)
        {
            var updatedReport = await _manager.read(companyId, memberId, reportId);
            if (updatedReport == null)
            {
                return NotFound();
            }
            await _manager.update(updatedReport, entity);
            return NoContent();
        }

        [HttpDelete]
        [Route("{reportId}")]
        public async Task<IActionResult> Delete(int companyId, int memberId, int reportId)
        {
            var result = await _manager.read(companyId, memberId, reportId);
            if (result == null)
            {
                return NotFound();
            }
            await _manager.delete(result);
            return NoContent();
        }
    }
}
