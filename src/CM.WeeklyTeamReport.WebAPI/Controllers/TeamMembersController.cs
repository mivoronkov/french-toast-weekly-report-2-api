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
    [Route("api/companies/{companyId}/members")]
    public class TeamMembersController : ControllerBase
    {
        private readonly ITeamMemberManager _manager;

        public TeamMembersController(ITeamMemberManager teamMemberManager)
        {
            _manager = teamMemberManager;
        }

        [HttpGet]
        public IActionResult Get(int companyId)
        {
            var result = _manager.readAll(companyId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{memberId}")]
        public IActionResult Get(int companyId, int memberId)
        {
            var result = _manager.read(companyId, memberId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(int companyId, [FromBody] TeamMemberDto teamMemberDto)
        {
            var email = User.Claims.FirstOrDefault(claim => claim.Type.EndsWith("/email")).Value;
            if (email == null)
                return Unauthorized();
            Console.WriteLine(User.Claims.ToList());
            teamMemberDto.CompanyId = companyId;
            teamMemberDto.Email = email;
            teamMemberDto.Sub = User.Identity.Name;
            var result = _manager.create(teamMemberDto);
            if (result == null)
            {
                return NoContent();
            }
            var uriCreatedTeamMember = $"api/companies/{companyId}/{result.ID}";
            return Created(uriCreatedTeamMember, result);
        }

        [HttpPut]
        [Route("{memberId}")]
        public IActionResult Put([FromBody] TeamMemberDto entity, int companyId, int memberId)
        {
            var updatedTeamMember = _manager.read(companyId, memberId);
            if (updatedTeamMember == null)
            {
                return NotFound();
            }
            _manager.update(updatedTeamMember, entity);
            return NoContent();
        }

        [HttpDelete]
        [Route("{memberId}")]
        public IActionResult Delete(int companyId, int memberId)
        {
            var result = _manager.read(companyId, memberId);
            if (result == null)
            {
                return NotFound();
            }
            _manager.delete(companyId, memberId);
            return NoContent();
        }
    }
}
