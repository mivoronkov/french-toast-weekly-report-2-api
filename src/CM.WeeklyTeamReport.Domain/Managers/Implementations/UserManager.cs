using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Managers.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Managers.Implementations
{
    public class UserManager : IUserManager
    {
        readonly ITeamMemberRepository _teamMemberRepository;
        readonly IUserCommands _userCommands;
        public UserManager(ITeamMemberRepository teamMemberRepository, IUserCommands userCommands)
        {
            _teamMemberRepository = teamMemberRepository;
            _userCommands = userCommands;
        }
        public UserDto readUserBySub(string sub)
        {
            var teamMember = _teamMemberRepository.ReadBySub(sub);
            if(teamMember == null)
            {
                return null;
            }
            var leaders = _teamMemberRepository.GetLeadersToReport(teamMember);
            var teammates = _teamMemberRepository.GetReportingMembers(teamMember);
            var user = _userCommands.TeamMemberToUserDto(teamMember, leaders, teammates);
            return user;
        }
    }
}
