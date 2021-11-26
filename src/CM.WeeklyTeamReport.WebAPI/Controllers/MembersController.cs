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
    public class MembersController : RESTController<TeamMember>
    {
        protected override string EntityEndpoint => "Members";

        public MembersController(IRepository<TeamMember> repo) : base(repo) { }
    }
}
