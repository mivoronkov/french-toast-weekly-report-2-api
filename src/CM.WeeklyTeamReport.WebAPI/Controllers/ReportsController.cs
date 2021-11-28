using CM.WeeklyTeamReport.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.WebAPI.Controllers
{
    [ApiController]
    [Route("api/Companies/{companyId}/Members/{memberId}/Reports")]
    public class ReportsController
    {
        private readonly ReportsRESTController rest;

        public ReportsController(IRepository<WeeklyReport> repo){
            rest = new ReportsRESTController(repo);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return rest.Get();
        }

        [HttpGet]
        [Route("{reportId}")]
        public IActionResult GetSingle(int memberId)
        {
            return rest.GetSingle(memberId);
        }

        [HttpPost]
        public IActionResult Create(WeeklyReport entity)
        {
            return rest.Create(entity);
        }

        [HttpPut]
        [Route("{reportId}")]
        public IActionResult Put(int reportId, WeeklyReport entity)
        {
            return rest.Put(reportId, entity);
        }

        [HttpDelete]
        [Route("{reportId}")]
        public IActionResult Delete(int reportId)
        {
            return rest.Delete(reportId);
        }

        private class ReportsRESTController : RESTController<WeeklyReport>
        {
            public override string EntitiesEndpoint => "api/Companies/{companyId}/Members/{memberId}/Reports";

            public ReportsRESTController(IRepository<WeeklyReport> repo) : base(repo) { }
        }
    }
}
