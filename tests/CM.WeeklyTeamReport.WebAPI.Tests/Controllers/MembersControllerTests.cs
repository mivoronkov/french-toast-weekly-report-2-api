using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CM.WeeklyTeamReport.WebAPI.Controllers.Tests
{
    public class MembersControllerTests
    {
        [Fact]
        public void ShouldReturnAllMembers()
        {
            var fixture = new MembersControllerFixture();
            fixture.TeamMemberManager
                .Setup(x => x.readAllmembers(1))
                .Returns(new List<ITeamMemberDto>() {
                    GetTeamMemberDto(1),
                    GetTeamMemberDto(2)
                });
            var controller = fixture.GetCompaniesController();
            var teamMembers = (ICollection<ITeamMemberDto>)((OkObjectResult)controller.Get(1)).Value;

            teamMembers.Should().NotBeNull();
            teamMembers.Should().HaveCount(2);

            fixture
                .TeamMemberManager
                .Verify(x => x.readAllmembers(1), Times.Once);
        }

        [Fact]
        public void ShouldReturnSingleMember()
        {
            var fixture = new MembersControllerFixture();
            fixture.TeamMemberManager
                .Setup(x => x.readTeamMember(1,56))
                .Returns(GetTeamMemberDto(56));
            var controller = fixture.GetCompaniesController();
            var teamMembers = (ITeamMemberDto)((OkObjectResult)controller.Get(1, 56)).Value;

            teamMembers.Should().NotBeNull();

            fixture
                .TeamMemberManager
                .Verify(x => x.readTeamMember(1,56), Times.Once);
        }

        [Fact]
        public void ShouldCreateMember()
        {
            var fixture = new MembersControllerFixture();
            var teamMember = GetTeamMember();
            var teamMemberDto = GetTeamMemberDto();
            fixture.TeamMemberManager
                .Setup(x => x.createTeamMember(teamMemberDto))
                .Returns(teamMember);
            var controller = fixture.GetCompaniesController();
            var returnedTM = (TeamMember)((CreatedResult)controller.Post(1, teamMemberDto)).Value;

            returnedTM.Should().NotBeNull();
            returnedTM.ID.Should().NotBe(0);

            fixture
                .TeamMemberManager
                .Verify(x => x.createTeamMember(teamMemberDto), Times.Once);
        }

        [Fact]
        public void ShouldUpdateMember()
        {
            var fixture = new MembersControllerFixture();
            var teamMember = GetTeamMember();
            var teamMemberDto = GetTeamMemberDto();
            var teamMemberDto2 = GetTeamMemberDto();
            fixture.TeamMemberManager
                .Setup(x => x.readTeamMember(1, teamMember.ID))
                .Returns(teamMemberDto);
            fixture.TeamMemberManager
                .Setup(x => x.updateTeamMember(teamMemberDto, teamMemberDto2));
            teamMember.FirstName = "Name 2";
            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Put(1, teamMember.ID, teamMemberDto);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .TeamMemberManager
                .Verify(x => x.readTeamMember(1, teamMember.ID), Times.Once);
            fixture
                .TeamMemberManager
                .Verify(x => x.updateTeamMember(teamMemberDto, teamMemberDto), Times.Once);
        }

        [Fact]
        public void ShouldDeleteMember()
        {
            var fixture = new MembersControllerFixture();
            var teamMember = GetTeamMember();
            var teamMemberDto = GetTeamMemberDto();
            fixture.TeamMemberManager
                .Setup(x => x.deleteTeamMember(1, teamMember.ID));
            fixture.TeamMemberManager
                .Setup(x => x.readTeamMember(1, teamMember.ID))
                .Returns(teamMemberDto);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Delete(1, teamMember.ID);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .TeamMemberManager
                .Verify(x => x.deleteTeamMember(1, teamMember.ID), Times.Once);
        }

        private TeamMember GetTeamMember(int id = 1)
        {
            return new TeamMember
            {
                ID = id,
                FirstName = $"Agent{id}",
                LastName = $"Smith{id}",
                Title = $"Agent{id}",
                Email = $"smith{id}@matrix.org"
            };
        }
        private ITeamMemberDto GetTeamMemberDto(int id = 1)
        {
            return new TeamMemberDto
            {
                ID = id,
                FirstName = $"Agent{id}",
                LastName = $"Smith{id}",
                Title = $"Agent{id}",
                Email = $"smith{id}@matrix.org"
            };
        }

        public class MembersControllerFixture
        {
            public MembersControllerFixture()
            {
                TeamMemberManager = new Mock<ITeamMemberManager>();
            }

            public Mock<ITeamMemberManager> TeamMemberManager { get; private set; }

            public TeamMembersController GetCompaniesController()
            {
                return new TeamMembersController(TeamMemberManager.Object);
            }
        }
    }
}
