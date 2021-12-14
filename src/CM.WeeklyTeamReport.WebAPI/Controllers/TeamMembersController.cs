using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.WebAPI.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/members")]
    public class TeamMembersController : ControllerBase
    {
        private readonly ITeamMemberManager _manager;

        public TeamMembersController(ITeamMemberManager teamMemberManager) {
            _manager = teamMemberManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{memberId}")]
        public IActionResult Get(int companyId, int memberId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post(int companyId, TeamMember entity)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{memberId}")]
        public IActionResult Put(int companyId, int memberId, TeamMember entity)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{memberId}")]
        public IActionResult Delete(int companyId, int memberId)
        {
            throw new NotImplementedException();
        }
    }
}
