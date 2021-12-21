using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Managers.Implementations;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Managers;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class UserManagerTests
    {
        [Fact]
        public void ShouldReadBySub()
        {
            var fixture = new UserManagerFixture();
            var member = new TeamMember {
                ID = 1,
                CompanyId = 1,
                Email = "1",
                FirstName = "A",
                LastName = "b",
                Title = "Last",
                InviteLink = "asd",
                Sub ="a"
            };

            var leader = new FollowerDto() { ID = 2, FirstName = "Zorg", LastName = "Borg" };
            var leaderTeamMember = new TeamMember() { ID = 2, FirstName = "Zorg", LastName = "Borg" };
            var followerL = new List<FollowerDto> { leader };
            var leadersTeam = new List<ITeamMember> { leaderTeamMember };

            var teammate = new FollowerDto() { ID = 3, FirstName = "Tzen", LastName = "Rock" };
            var teammateTeamMember = new TeamMember() { ID = 3, FirstName = "Tzen", LastName = "Rock" };
            var followerTM = new List<FollowerDto> { teammate };
            var teammatesTeam = new List<ITeamMember> { teammateTeamMember };

            var userDto = new UserDto
            {
                ID = 1,
                CompanyId = 1,
                Email = "1",
                FirstName = "A",
                LastName = "b",
                Title = "Last",
                Leaders = followerL,
                Teammates = followerTM,
                InviteLink = "asd"
            };

            fixture.MembersRepository.Setup(x => x.ReadBySub("a")).Returns(member);
            fixture.MembersRepository.Setup(x => x.GetLeadersToReport(member)).Returns(leadersTeam);
            fixture.MembersRepository.Setup(x => x.GetReportingMembers(member)).Returns(teammatesTeam);

            fixture.UserCommand.Setup(x => x.TeamMemberToUserDto(member, leadersTeam, teammatesTeam)).Returns(userDto);
            var manager = fixture.GetUserManager();

            var result = manager.readUserBySub("a");
            fixture.MembersRepository.Verify(x => x.ReadBySub("a"), Times.Once);
            fixture.MembersRepository.Verify(x => x.GetLeadersToReport(member), Times.Once);
            fixture.MembersRepository.Verify(x => x.GetReportingMembers(member), Times.Once);
            fixture.UserCommand.Verify(x => x.TeamMemberToUserDto(member, leadersTeam, teammatesTeam), Times.Once);

        }
        [Fact]
        public void ShouldReturnNull()
        {
            var fixture = new UserManagerFixture();
            fixture.MembersRepository.Setup(x => x.ReadBySub("fd")).Returns((TeamMember)null);
            var manager = fixture.GetUserManager();

            var result = manager.readUserBySub("fd");
            result.Should().BeNull();
        }
        public class UserManagerFixture
        {
            public UserManagerFixture()
            {
                MembersRepository = new Mock<ITeamMemberRepository>();
                UserCommand = new Mock<IUserCommands>();
            }

            public Mock<ITeamMemberRepository> MembersRepository { get; private set; }
            public Mock<IUserCommands> UserCommand { get; private set; }

            public UserManager GetUserManager()
            {
                return new UserManager(MembersRepository.Object, UserCommand.Object);
            }
        }
    }
}
