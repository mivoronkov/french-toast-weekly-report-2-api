using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Entities.Implementations;
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
        public LinksController(ITeamLinkManager linksManager)
        {
            _manager = linksManager;
        }
        // GET: api/<InvitationController>
        [HttpGet]
        [Route("leaders")]
        public async Task<IActionResult> GetLeaders(int memberId)
        {
            var result = await _manager.ReadLeaders(memberId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("followers")]
        public async Task<IActionResult> GetReportingTMs(int memberId)
        {
            var result = await _manager.ReadReportingTMs(memberId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/<InvitationController>
        [HttpPost]
        public async Task<IActionResult> AcceptInvite(int memberId, [FromBody] int leaderId)
        {
            var result = await _manager.Create(memberId, leaderId);
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
        public async Task<IActionResult> DeleteLink(int linkedMemberId, int memberId)
        {
            var result = await _manager.ReadLink(linkedMemberId, memberId);
            if (result == null)
            {
                return NotFound();
            }
            await _manager.Delete(linkedMemberId, memberId);
            return NoContent();
        }
        [HttpPut]
        [Route("leaders")]
        public async Task<IActionResult> PutLeaders([FromBody] IntListDto leadersDto, int memberId)
        {
            var oldLeaders = await _manager.ReadLeaders(memberId);
            var oldLeadersList = oldLeaders != null ? oldLeaders.Select(el => el.LeaderTMId).ToList() : new List<int>();
            var newLeaders = leadersDto != null ? leadersDto.Leaders.ToList() : new List<int>();
            await _manager.UpdateLeaders(memberId, oldLeadersList, newLeaders);
            return NoContent();
        }
        [HttpPut]
        [Route("followers")]
        public async Task<IActionResult> PutFollowers([FromBody] IntListDto followersDto, int memberId)
        {
            var oldFollowers = await _manager.ReadReportingTMs(memberId);
            var oldFollowerssList = oldFollowers != null ? oldFollowers.Select(el => el.ReportingTMId).ToList() : new List<int>();
            var newFollowers = followersDto != null ? followersDto.Followers.ToList() : new List<int>();
            await _manager.UpdateFollowers(memberId, oldFollowerssList, newFollowers);
            return NoContent();
        }
    }
}
