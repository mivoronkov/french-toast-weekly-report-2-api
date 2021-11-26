using CM.WeeklyTeamReport.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.WebAPI.Controllers
{
    [ApiController]
    [Route("api/Companies/{companyId}/Members")]
    public class MembersController
    {
        private readonly MembersRESTController rest;

        public MembersController(IRepository<TeamMember> repo) {
            rest = new MembersRESTController(repo);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return rest.Get();
        }

        [HttpGet]
        [Route("{memberId}")]
        public IActionResult GetSingle(int companyId, int memberId)
        {
            return rest.GetSingle(memberId);
        }

        [HttpPost]
        public IActionResult Create(int companyId, TeamMember entity)
        {
            entity.CompanyId = companyId;
            return rest.Create(entity);
        }

        [HttpPut]
        [Route("{memberId}")]
        public IActionResult Put(int companyId, int memberId, TeamMember entity)
        {
            entity.CompanyId = companyId;
            return rest.Put(memberId, entity);
        }

        [HttpDelete]
        [Route("{memberId}")]
        public IActionResult Delete(int companyId, int memberId)
        {
            return rest.Delete(memberId);
        }

        private class MembersRESTController : RESTController<TeamMember>
        {
            public override string EntitiesEndpoint => "api/Companies/{companyId}/Members";

            public MembersRESTController(IRepository<TeamMember> repo) : base(repo) { }
        }
    }
}
