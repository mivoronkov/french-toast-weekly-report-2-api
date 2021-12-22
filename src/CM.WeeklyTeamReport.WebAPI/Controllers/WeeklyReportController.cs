using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Dto.Implementations;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.WebAPI.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/members/{memberId}/reports")]
    public class WeeklyReportController : ControllerBase
    {
        private readonly IWeeklyReportManager _manager;

        public WeeklyReportController(IWeeklyReportManager weeklyReportManager)
        {
            _manager = weeklyReportManager;
        }

        [HttpGet]
        public IActionResult Get(int companyId, int memberId)
        {
            var result = _manager.readAll(companyId, memberId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{reportId}")]
        public IActionResult Get(int companyId, int memberId, int reportId)
        {
            var result = _manager.read(companyId, memberId, reportId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("extended/{start}/{end}")]
        public IActionResult GetExtendedInInterval(int companyId, int memberId, long start, long end)
        {
            DateTime startDate = new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(start);
            DateTime endDate = new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds(end);
            var result = _manager.ReadReportsInInterval(companyId, memberId, startDate, endDate);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReportsDto entity, int companyId, int memberId)
        {
            var result = _manager.create(entity);
            if (result == null)
            {
                return NoContent();
            }
            var uriCreatedReport = $"api/companies/{companyId}/members/{memberId}/reports/{result.ID}";
            return Created(uriCreatedReport, result);
        }

        [HttpPut]
        [Route("{reportId}")]
        public IActionResult Put([FromBody] ReportsDto entity, int companyId, int memberId, int reportId)
        {
            var updatedReport = _manager.read(companyId, memberId, reportId);
            if (updatedReport == null)
            {
                return NotFound();
            }
            _manager.update(updatedReport, entity);
            return NoContent();
        }

        [HttpDelete]
        [Route("{reportId}")]
        public IActionResult Delete(int companyId, int memberId, int reportId)
        {
            var result = _manager.read(companyId, memberId, reportId);
            if (result == null)
            {
                return NotFound();
            }
            _manager.delete(result);
            return NoContent();
        }
    }
}
