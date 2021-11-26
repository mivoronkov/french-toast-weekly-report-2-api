using CM.WeeklyTeamReport.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : RESTController<WeeklyReport>
    {
        protected override string EntityEndpoint => "Reports";

        public ReportsController(IRepository<WeeklyReport> repo) : base(repo) { }
    }
}
