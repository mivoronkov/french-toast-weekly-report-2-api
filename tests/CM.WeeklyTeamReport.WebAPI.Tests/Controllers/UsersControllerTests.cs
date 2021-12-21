using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Managers.Interfaces;
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
            fixture.Manager
                .Setup(x => x.readUserBySub(sub))
                .Returns(GetUserDto(id));
            var controller = fixture.GetUsersController();
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User.Identity.Name).Returns(sub);
            controller.ControllerContext.HttpContext = contextMock.Object;
            var teamMembers = (UserDto)((OkObjectResult)controller.Get()).Value;

            teamMembers.Should().NotBeNull();

            fixture
                .Manager
                .Verify(x => x.readUserBySub(sub), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnReadBySub()
        {
            var fixture = new MembersControllerFixture();
            string sub = "auth0|1";
            fixture.Manager
                .Setup(x => x.readUserBySub(sub))
                .Returns((UserDto)null);
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

        private UserDto GetUserDto(int id = 1)
        {
            return new UserDto
            {
                ID = id,
                FirstName = $"Agent{id}",
                LastName = $"Smith{id}",
                Title = $"Agent{id}",
                Email = $"smith{id}@matrix.org",
            };
        }
        public class MembersControllerFixture
        {
            public MembersControllerFixture()
            {
                Manager = new Mock<IUserManager>();
            }

            public Mock<IUserManager> Manager { get; private set; }

            public UsersController GetUsersController()
            {
                return new UsersController(Manager.Object);
            }
        }
    }
}
