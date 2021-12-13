using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.WebAPI.Controllers
{
    [ApiController]
    [Route("api/Companies/{companyId}/Members/{memberId}/Reports")]
    public class WeeklyReportController
    {
        private readonly IWeeklyReportManager _manager;

        public WeeklyReportController(IWeeklyReportManager weeklyReportManager)
        {
            _manager = weeklyReportManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{reportId}")]
        public IActionResult GetSingle(int memberId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Create(WeeklyReport entity)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{reportId}")]
        public IActionResult Put(int reportId, WeeklyReport entity)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{reportId}")]
        public IActionResult Delete(int reportId)
        {
            throw new NotImplementedException();
        }
    }
}
