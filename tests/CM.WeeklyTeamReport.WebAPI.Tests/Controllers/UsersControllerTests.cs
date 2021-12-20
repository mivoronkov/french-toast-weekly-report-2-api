using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CM.WeeklyTeamReport.WebAPI.Controllers.Tests
{
    public class UsersControllerTests
    {
        [Fact]
        public void ShouldReturnSingleMemberBySub()
        {
            var fixture = new MembersControllerFixture();
            int id = 1;
            string sub = $"auth0|{id}";
            fixture.TeamMemberManager
                .Setup(x => x.readBySub(sub))
                .Returns(GetTeamMemberDto(id));
            var controller = fixture.GetUsersController();
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User.Identity.Name).Returns(sub);
            controller.ControllerContext.HttpContext = contextMock.Object;
            var teamMembers = (TeamMemberDto)((OkObjectResult)controller.Get()).Value;

            teamMembers.Should().NotBeNull();

            fixture
                .TeamMemberManager
                .Verify(x => x.readBySub(sub), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnReadBySub()
        {
            var fixture = new MembersControllerFixture();
            string sub = "auth0|1";
            fixture.TeamMemberManager
                .Setup(x => x.readBySub(sub))
                .Returns((TeamMemberDto)null);
            var controller = fixture.GetUsersController();
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User.Identity.Name).Returns(sub);
            controller.ControllerContext.HttpContext = contextMock.Object;
            var teamMembers = controller.Get();

            teamMembers.Should().BeOfType<NotFoundResult>();
        }

        private TeamMember GetTeamMember(int id = 1)
        {
            return new TeamMember
            {
                ID = id,
                FirstName = $"Agent{id}",
                LastName = $"Smith{id}",
                Title = $"Agent{id}",
                Email = $"smith{id}@matrix.org",
                Sub = $"auth0|{id}"
            };
        }

        private TeamMemberDto GetTeamMemberDto(int id = 1)
        {
            return new TeamMemberDto
            {
                ID = id,
                FirstName = $"Agent{id}",
                LastName = $"Smith{id}",
                Title = $"Agent{id}",
                Email = $"smith{id}@matrix.org",
                Sub = $"auth0|{id}"
            };
        }

        public class MembersControllerFixture
        {
            public MembersControllerFixture()
            {
                TeamMemberManager = new Mock<ITeamMemberManager>();
            }

            public Mock<ITeamMemberManager> TeamMemberManager { get; private set; }

            public UsersController GetUsersController()
            {
                return new UsersController(TeamMemberManager.Object);
            }
        }
    }
}
