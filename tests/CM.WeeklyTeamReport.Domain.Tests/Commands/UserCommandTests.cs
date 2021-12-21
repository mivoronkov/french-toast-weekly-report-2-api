using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class UserCommandTests
    {
        public UserCommands commands = new UserCommands();
        [Fact]
        public void ShouldFollowerDto()
        {
            var teamMember = new TeamMember()
            {
                ID = 1,
                CompanyId = 1,
                Email = "1",
                FirstName = "A",
                LastName = "b",
                Title = "Last",
                InviteLink = "asd",
                Sub = "a",            
            };

            var follower = commands.MemberToFollowerDto(teamMember);
            Assert.Equal(follower.ID, teamMember.ID);
            Assert.Equal(follower.FirstName, teamMember.FirstName);
            Assert.Equal(follower.LastName, teamMember.LastName);
        }
        [Fact]
        public void ShouldTeamMemberToUserDto()
        {
            var member = new TeamMember
            {
                ID = 1,
                CompanyId = 1,
                Email = "1",
                FirstName = "A",
                LastName = "b",
                Title = "Last",
                InviteLink = "asd",
                Sub = "a"
            };

            var leader = new FollowerDto() { ID = 2, FirstName = "Zorg", LastName = "Borg" };
            var leaderTeamMember = new TeamMember() { ID = 2, FirstName = "Zorg", LastName = "Borg" };
            var leadersTeam = new List<ITeamMember> { leaderTeamMember };

            var teammate = new FollowerDto() { ID = 3, FirstName = "Tzen", LastName = "Rock" };
            var teammateTeamMember = new TeamMember() { ID = 3, FirstName = "Tzen", LastName = "Rock" };
            var teammatesTeam = new List<ITeamMember> { teammateTeamMember };

            var userDto = commands.TeamMemberToUserDto(member, leadersTeam, teammatesTeam);

            Assert.Equal(userDto.ID, member.ID);
            Assert.Equal(userDto.InviteLink, member.InviteLink);
            Assert.Equal(userDto.LastName, member.LastName);
            Assert.Equal(userDto.Title, member.Title);
            Assert.Equal(userDto.Email, member.Email);
            Assert.Equal(userDto.CompanyId, member.CompanyId);
            Assert.Equal(userDto.FirstName, member.FirstName);
            Assert.Equal(1, userDto.Leaders.Count);
            Assert.Equal(1, userDto.Teammates.Count);
            var tm = userDto.Teammates;
            var en = tm.GetEnumerator();
            en.MoveNext();
            var res1 = en.Current;
            Assert.Equal(teammate.ID, res1.ID);
            Assert.Equal(teammate.FirstName, res1.FirstName);
            Assert.Equal(teammate.LastName, res1.LastName);
            var led = userDto.Leaders;
            var enLed = led.GetEnumerator();
            enLed.MoveNext();
            var resLed = enLed.Current;
            Assert.Equal(leader.ID, resLed.ID);
            Assert.Equal(leader.FirstName, resLed.FirstName);
            Assert.Equal(leader.LastName, resLed.LastName);
        }
    }
}
