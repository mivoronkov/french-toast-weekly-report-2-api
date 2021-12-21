using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Managers.Interfaces;
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
        private readonly IUserManager _manager;

        public UsersController(IUserManager userManager) {
            _manager = userManager;
        }

        // Get Team Member by user sub
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var result = _manager.readUserBySub(User.Identity.Name);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
