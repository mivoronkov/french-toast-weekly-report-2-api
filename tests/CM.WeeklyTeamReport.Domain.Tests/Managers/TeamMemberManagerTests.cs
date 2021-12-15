using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
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
    public class TeamMemberManagerTests
    {
        [Theory]
        [InlineData(1, "Trevor Philips Industries")]
        [InlineData(5, "Sony")]
        public void ShouldReadAllMembers(int id, string setCompanyName)
        {
            var fixture = new MemberManagerFixture();
            var member1 = new TeamMember { CompanyId = id, ID = id + 1, 
                FirstName = "loto", LastName = "feto", Email = "df", Title = "TTT" };
            var member2 = new TeamMember { CompanyId = id, ID = id + 2, 
                FirstName = "Polo", LastName = "Darko", Email = "jjj", Title = "TTT" };
            var memberDto1 = new TeamMemberDto
            {
                CompanyId = id,
                ID = id + 1,
                FirstName = "loto",
                LastName = "feto",
                Email = "df",
                Title = "TTT"
            };
            var memberDto2 = new TeamMemberDto
            {
                CompanyId = id,
                ID = id + 2,
                FirstName = "Polo",
                LastName = "Darko",
                Email = "jjj",
                Title = "TTT"
            };
            var membersList = new List<ITeamMember> { member1, member2 };

            fixture.CompanyRepository.Setup(el => el.GetCompanyName(id)).Returns(setCompanyName);
            fixture.MemberRepository.Setup(el => el.ReadAll(id)).Returns(membersList);
            fixture.MemberCommands.Setup(el => el.teamMemberToDto(member1, setCompanyName)).Returns(memberDto1);
            fixture.MemberCommands.Setup(el => el.teamMemberToDto(member2, setCompanyName)).Returns(memberDto2);

            var manager = fixture.GetMemberManager();
            var members = (List <TeamMemberDto>)manager.readAll(id);
            members.Should().HaveCount(2);
            fixture.CompanyRepository.Verify(x => x.GetCompanyName(id), Times.Once);
            fixture.MemberCommands.Verify(x => x.teamMemberToDto(member1, setCompanyName), Times.Once);
            fixture.MemberCommands.Verify(x => x.teamMemberToDto(member1, setCompanyName), Times.Once);
            fixture.MemberRepository.Verify(el => el.ReadAll(id), Times.Once);
        }

        [Theory]
        [InlineData(1,1, "Trevor Philips Industries")]
        [InlineData(5,5, "Sony")]
        public void ShouldReadMemberByID(int companyId, int memberId, string setCompanyName)
        {
            var fixture = new MemberManagerFixture();
            var member = new TeamMember
            {
                CompanyId = companyId,
                ID = memberId,
                FirstName = "loto",
                LastName = "feto",
                Email = "df",
                Title = "TTT"
            };
            var memberDto = new TeamMemberDto
            {
                CompanyId = companyId,
                ID = memberId,
                FirstName = "loto",
                LastName = "feto",
                Email = "df",
                Title = "TTT"
            };

            fixture.CompanyRepository.Setup(el => el.GetCompanyName(companyId)).Returns(setCompanyName);
            fixture.MemberRepository.Setup(el => el.Read(companyId, memberId)).Returns(member);
            fixture.MemberCommands.Setup(el => el.teamMemberToDto(member, setCompanyName)).Returns(memberDto);

            var manager = fixture.GetMemberManager();
            var radedMember = manager.read(companyId, memberId);
            radedMember.Should().BeOfType<TeamMemberDto>();
            fixture.MemberCommands.Verify(el => el.teamMemberToDto(member, setCompanyName), Times.Once);
            fixture.MemberRepository.Verify(el => el.Read(companyId, memberId), Times.Once);
            fixture.CompanyRepository.Verify(el => el.GetCompanyName(companyId), Times.Once);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 5)]
        public void ShouldDeleteMemberByID(int companyId, int memberId)
        {
            var fixture = new MemberManagerFixture();
            var member = new TeamMember
            {
                CompanyId = companyId,
                ID = memberId,
                FirstName = "loto",
                LastName = "feto",
                Email = "df",
                Title = "TTT"
            };
            fixture.MemberRepository.Setup(x => x.Delete(memberId));
            var manager = fixture.GetMemberManager();

            manager.delete(companyId, memberId);
            fixture.MemberRepository.Verify(el => el.Delete(memberId), Times.Once);
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(5,5)]
        public void ShouldCreateMember(int companyId, int memberId)
        {
            var fixture = new MemberManagerFixture();
            var member = new TeamMember
            {
                CompanyId = companyId,
                ID = memberId,
                FirstName = "loto",
                LastName = "feto",
                Email = "df",
                Title = "TTT"
            };
            var memberDto = new TeamMemberDto
            {
                CompanyId = companyId,
                ID = memberId,
                FirstName = "loto",
                LastName = "feto",
                Email = "df",
                Title = "TTT"
            };

            fixture.MemberRepository.Setup(el => el.Create(member)).Returns(member);
            fixture.MemberCommands.Setup(el => el.dtoToTeamMember(memberDto)).Returns(member);

            var manager = fixture.GetMemberManager();
            var newMember = manager.create(memberDto);
            newMember.Should().BeOfType<TeamMember>();
            fixture.MemberRepository.Verify(el => el.Create(member), Times.Once);
            fixture.MemberCommands.Verify(el => el.dtoToTeamMember(memberDto), Times.Once);
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(5,5)]
        public void ShouldUpdateMember(int companyId, int memberId)
        {
            var fixture = new MemberManagerFixture();
            var oldMemberDto = new TeamMemberDto
            {
                CompanyId = companyId,
                ID = memberId,
                FirstName = "loto",
                LastName = "feto",
                Email = "df",
                Title = "TTT"
            };
            var newMemberDto = new TeamMemberDto
            {
                CompanyId = 0,
                ID = 0,
                FirstName = "loto",
                LastName = "feto",
                Email = "df",
                Title = "TTT"
            };
            var newMember = new TeamMember
            {
                CompanyId = companyId,
                ID = memberId,
                FirstName = "loto",
                LastName = "feto",
                Email = "df",
                Title = "TTT"
            };

            fixture.MemberCommands.Setup(el => el.dtoToTeamMember(newMemberDto)).Returns(newMember);
            fixture.MemberRepository.Setup(el => el.Update(newMember));

            var manager = fixture.GetMemberManager();
            manager.update(oldMemberDto, newMemberDto);
            fixture.MemberCommands.Verify(el => el.dtoToTeamMember(newMemberDto), Times.Once);
            fixture.MemberRepository.Verify(el => el.Update(newMember), Times.Once);

            newMember.ID.Should().Be(newMemberDto.ID);
            newMember.CompanyId.Should().Be(newMemberDto.CompanyId);
        }
        public class MemberManagerFixture
        {
            public MemberManagerFixture()
            {
                MemberRepository = new Mock<ITeamMemberRepository>();
                CompanyRepository = new Mock<ICompanyRepository>();
                MemberCommands = new Mock<IMemberCommands>();
            }

            public Mock<ITeamMemberRepository> MemberRepository { get; private set; }
            public Mock<ICompanyRepository> CompanyRepository { get; private set; }
            public Mock<IMemberCommands> MemberCommands { get; private set; }

            public TeamMemberManager GetMemberManager()
            {
                return new TeamMemberManager(MemberRepository.Object, CompanyRepository.Object, MemberCommands.Object);
            }
        }
    }
}
