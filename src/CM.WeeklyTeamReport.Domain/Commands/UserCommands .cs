using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Commands
{
    public class UserCommands: IUserCommands
    {
        public FollowerDto MemberToFollowerDto(ITeamMember teamMember)
        {
            return new FollowerDto()
            {
                ID = teamMember.ID,
                FirstName = teamMember.FirstName,
                LastName = teamMember.LastName
            };
        }
        public UserDto TeamMemberToUserDto(ITeamMember teamMember, ICollection<ITeamMember> leaders, ICollection<ITeamMember> teammates)
        {
            var user = new UserDto()
            {
                ID = teamMember.ID,
                FirstName = teamMember.FirstName,
                LastName = teamMember.LastName,
                Email = teamMember.Email,
                InviteLink = teamMember.InviteLink,
                Title = teamMember.Title,
                Leaders = leaders.Select(el => MemberToFollowerDto(el)).ToList(),
                Teammates = teammates.Select(el => MemberToFollowerDto(el)).ToList(),
                CompanyId =teamMember.CompanyId,
            };
            return user;
        }
    }
}
