using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
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
    public class LinksControllerTests
    {
        [Fact]
        public void ShouldReturnAllLeaders()
        {
            var fixture = new LinksControllerFixture();
            fixture.LinkManager
                .Setup(x => x.ReadLeaders(1))
                .Returns(new List<ITeamLink>() {
                    new TeamLink { ReportingTMId=1, LeaderTMId=1 },
                    new TeamLink { ReportingTMId=1, LeaderTMId=2 }
                });
            var controller = fixture.GetLinksController();
            var links = (ICollection<ITeamLink>)((OkObjectResult)controller.GetLeaders(1)).Value;

            links.Should().NotBeNull();
            links.Should().HaveCount(2);

            fixture
                .LinkManager
                .Verify(x => x.ReadLeaders(1), Times.Once);
        }
        [Fact]
        public void ShouldReturnNotFoundOnReadLeaders()
        {
            var fixture = new LinksControllerFixture();
            fixture.LinkManager
                .Setup(x => x.ReadLeaders(1))
                .Returns((List<ITeamLink>)null);
            var controller = fixture.GetLinksController();
            var actionResult = controller.GetLeaders(1);
            actionResult.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public void ShouldReturnAllReportingTm()
        {
            var fixture = new LinksControllerFixture();
            fixture.LinkManager
                .Setup(x => x.ReadReportingTMs(1))
                .Returns(new List<ITeamLink>() {
                    new TeamLink { ReportingTMId=1, LeaderTMId=1 },
                    new TeamLink { ReportingTMId=2, LeaderTMId=1 }
                });
            var controller = fixture.GetLinksController();
            var links = (ICollection<ITeamLink>)((OkObjectResult)controller.GetReportingTMs(1)).Value;

            links.Should().NotBeNull();
            links.Should().HaveCount(2);

            fixture
                .LinkManager
                .Verify(x => x.ReadReportingTMs(1), Times.Once);
        }
        [Fact]
        public void ShouldReturnNotFoundOnReadReportingTm()
        {
            var fixture = new LinksControllerFixture();
            fixture.LinkManager
                .Setup(x => x.ReadReportingTMs(1))
                .Returns((List<ITeamLink>)null);
            var controller = fixture.GetLinksController();
            var actionResult = controller.GetReportingTMs(1);
            actionResult.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public void ShouldCreateLink()
        {
            var fixture = new LinksControllerFixture();
            var link = new TeamLink()
            {
                ReportingTMId = 1,
                LeaderTMId = 2
            };
            fixture.LinkManager
                .Setup(x => x.Create(1,2))
                .Returns(link);
            var controller = fixture.GetLinksController();
            var result = (TeamLink)((CreatedResult)controller.AcceptInvite(1,2)).Value;

            result.Should().NotBeNull();
            result.LeaderTMId.Should().Be(link.LeaderTMId);

            fixture
                .LinkManager
                .Verify(x => x.Create(1,2), Times.Once);
        }

        [Fact]
        public void ShouldReturnNoContentIfCannotCreate()
        {
            var fixture = new LinksControllerFixture();
            fixture.LinkManager
                .Setup(x => x.Create(1,2))
                .Returns((TeamLink)null);

            var controller = fixture.GetLinksController();
            var actionResult = controller.AcceptInvite(1,2);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .LinkManager
                .Verify(x => x.Create(1,2), Times.Once);
        }

        [Fact]
        public void ShouldDeleteLink()
        {
            var fixture = new LinksControllerFixture();
            var link = new TeamLink()
            {
                ReportingTMId = 1,
                LeaderTMId = 2
            };
            fixture.LinkManager
                .Setup(x => x.Delete(1,1));
            fixture.LinkManager
                .Setup(x => x.ReadLink(1,1))
                .Returns(link);

            var controller = fixture.GetLinksController();
            var actionResult = controller.DeleteLink(1,1);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .LinkManager
                .Verify(x => x.Delete(1,1), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnDelete()
        {
            var fixture = new LinksControllerFixture();
            fixture.LinkManager
                .Setup(x => x.Delete(1, 1));
            fixture.LinkManager
                .Setup(x => x.ReadLink(1, 1))
                .Returns((TeamLink)null);

            var controller = fixture.GetLinksController();
            var actionResult = controller.DeleteLink(1, 1);
            actionResult.Should().BeOfType<NotFoundResult>();

            fixture
                .LinkManager
                .Verify(x => x.Delete(1,1), Times.Never);
        }
        public class LinksControllerFixture
        {
            public LinksControllerFixture()
            {
                LinkManager = new Mock<ITeamLinkManager>();
            }

            public Mock<ITeamLinkManager> LinkManager { get; private set; }

            public LinksController GetLinksController()
            {
                return new LinksController(LinkManager.Object);
            }
        }
    }
}
