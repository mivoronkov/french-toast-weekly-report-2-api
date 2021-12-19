using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CM.WeeklyTeamReport.WebAPI.Controllers
{
    [Route("api/links/{memberId}")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ITeamLinkManager _manager;
        public LinksController(ITeamLinkManager inviteLinksManager)
        {
            _manager = inviteLinksManager;
        }
        // GET: api/<InvitationController>
        [HttpGet]
        [Route("/leaders")]
        public IActionResult GetLeaders(int memberId)
        {
            var result = _manager.ReadLeaders(memberId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("/subscribers")]
        public IActionResult GetReportingTMs(int memberId)
        {
            var result = _manager.ReadReportingTMs(memberId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/<InvitationController>
        [HttpPost]
        public IActionResult AcceptInvite(int memberId, [FromBody] int leaderId)
        {
            var result = _manager.Create(memberId, leaderId);
            if (result == null)
            {
                return NoContent();
            }
            var uriCreatedCompany = $"api/invite/{memberId}";
            return Created(uriCreatedCompany, result);
        }

        // DELETE api/<InvitationController>/5
        [HttpDelete]
        [Route("{linkedMemberId}")]
        public IActionResult DeleteLink(int linkedMemberId, int memberId)
        {
            var result = _manager.ReadLink(linkedMemberId, memberId);
            if (result == null)
            {
                return NotFound();
            }
            _manager.Delete(linkedMemberId, memberId);
            return NoContent();
        }
    }
}
