using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
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
                .Setup(x => x.readAllmembers())
                .Returns(new List<ITeamMember>() {
                    GetTeamMember(1),
                    GetTeamMember(2)
                });
            var controller = fixture.GetCompaniesController();
            var teamMembers = (ICollection<TeamMember>)((OkObjectResult)controller.Get()).Value;

            teamMembers.Should().NotBeNull();
            teamMembers.Should().HaveCount(2);

            fixture
                .TeamMemberManager
                .Verify(x => x.readAllmembers(), Times.Once);
        }

        [Fact]
        public void ShouldReturnSingleMember()
        {
            var fixture = new MembersControllerFixture();
            fixture.TeamMemberManager
                .Setup(x => x.readTeamMember(56))
                .Returns(GetTeamMember(56));
            var controller = fixture.GetCompaniesController();
            var teamMembers = (TeamMember)((OkObjectResult)controller.Get(1, 56)).Value;

            teamMembers.Should().NotBeNull();

            fixture
                .TeamMemberManager
                .Verify(x => x.readTeamMember(56), Times.Once);
        }

        [Fact]
        public void ShouldCreateMember()
        {
            var fixture = new MembersControllerFixture();
            var tm = GetTeamMember();
            fixture.TeamMemberManager
                .Setup(x => x.createTeamMember(tm))
                .Returns(tm);
            var controller = fixture.GetCompaniesController();
            var returnedTM = (TeamMember)((CreatedResult)controller.Post(1, tm)).Value;

            returnedTM.Should().NotBeNull();
            returnedTM.ID.Should().NotBe(0);

            fixture
                .TeamMemberManager
                .Verify(x => x.createTeamMember(tm), Times.Once);
        }

        [Fact]
        public void ShouldUpdateMember()
        {
            var fixture = new MembersControllerFixture();
            var tm = GetTeamMember();
            fixture.TeamMemberManager
                .Setup(x => x.readTeamMember(tm.ID))
                .Returns(tm);
            fixture.TeamMemberManager
                .Setup(x => x.updateTeamMember(tm));
            tm.FirstName = "Name 2";
            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Put(1, tm.ID, tm);
            actionResult.Should().BeOfType<OkObjectResult>();

            fixture
                .TeamMemberManager
                .Verify(x => x.readTeamMember(tm.ID), Times.Once);
            fixture
                .TeamMemberManager
                .Verify(x => x.updateTeamMember(tm), Times.Once);
        }

        [Fact]
        public void ShouldDeleteMember()
        {
            var fixture = new MembersControllerFixture();
            var tm = GetTeamMember();
            fixture.TeamMemberManager
                .Setup(x => x.deleteTeamMember(tm.ID));
            fixture.TeamMemberManager
                .Setup(x => x.readTeamMember(tm.ID))
                .Returns(tm);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Delete(1, tm.ID);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .TeamMemberManager
                .Verify(x => x.deleteTeamMember(tm.ID), Times.Once);
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
