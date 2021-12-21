using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Commands
{
    public interface IUserCommands
    {
        public FollowerDto MemberToFollowerDto(ITeamMember teamMember);
        public UserDto TeamMemberToUserDto(ITeamMember teamMember, ICollection<ITeamMember> leaders, ICollection<ITeamMember> teammates);
    }
}
