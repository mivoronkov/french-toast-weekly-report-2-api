using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private readonly ITeamMemberManager _manager;

        public UsersController(ITeamMemberManager teamMemberManager) {
            _manager = teamMemberManager;
        }

        // Get Team Member by user sub
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var result = _manager.readBySub(User.Identity.Name);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
