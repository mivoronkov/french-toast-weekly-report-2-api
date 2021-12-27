using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Dto;
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
        public async void ShouldReturnAllLeaders()
        {
            var fixture = new LinksControllerFixture();
            fixture.LinkManager
                .Setup(x => x.ReadLeaders(1))
                .Returns(async () =>
                {
                    return new List<ITeamLink>() {
                    new TeamLink { ReportingTMId = 1, LeaderTMId = 1 },
                    new TeamLink { ReportingTMId = 1, LeaderTMId = 2 }
                };
                });
            var controller = fixture.GetLinksController();
            var links = (ICollection<ITeamLink>)((OkObjectResult)await controller.GetLeaders(1)).Value;

            links.Should().NotBeNull();
            links.Should().HaveCount(2);

            fixture
                .LinkManager
                .Verify(x => x.ReadLeaders(1), Times.Once);
        }
        [Fact]
        public async void ShouldReturnNotFoundOnReadLeaders()
        {
            var fixture = new LinksControllerFixture();
            fixture.LinkManager
                .Setup(x => x.ReadLeaders(1))
                .Returns(async () => { return null; });
            var controller = fixture.GetLinksController();
            var actionResult =await controller.GetLeaders(1);
            actionResult.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async void ShouldReturnAllReportingTm()
        {
            var fixture = new LinksControllerFixture();
            fixture.LinkManager
                .Setup(x => x.ReadReportingTMs(1))
                .Returns(async () =>
                {
                    return new List<ITeamLink>() {
                    new TeamLink { ReportingTMId = 1, LeaderTMId = 1 },
                    new TeamLink { ReportingTMId = 2, LeaderTMId = 1 }
                };
                });
            var controller = fixture.GetLinksController();
            var links = (ICollection<ITeamLink>)((OkObjectResult)await controller.GetReportingTMs(1)).Value;

            links.Should().NotBeNull();
            links.Should().HaveCount(2);

            fixture
                .LinkManager
                .Verify(x => x.ReadReportingTMs(1), Times.Once);
        }
        [Fact]
        public async void ShouldReturnNotFoundOnReadReportingTm()
        {
            var fixture = new LinksControllerFixture();
            fixture.LinkManager
                .Setup(x => x.ReadReportingTMs(1))
                .Returns(async () => { return null; });
            var controller = fixture.GetLinksController();
            var actionResult =await controller.GetReportingTMs(1);
            actionResult.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async void ShouldCreateLink()
        {
            var fixture = new LinksControllerFixture();
            var link = new TeamLink()
            {
                ReportingTMId = 1,
                LeaderTMId = 2
            };
            fixture.LinkManager
                .Setup(x => x.Create(1,2))
                .Returns(async () => { return link; });
            var controller = fixture.GetLinksController();
            var result = (TeamLink)((CreatedResult)await controller.AcceptInvite(1,2)).Value;

            result.Should().NotBeNull();
            result.LeaderTMId.Should().Be(link.LeaderTMId);

            fixture
                .LinkManager
                .Verify(x => x.Create(1,2), Times.Once);
        }

        [Fact]
        public async void ShouldReturnNoContentIfCannotCreate()
        {
            var fixture = new LinksControllerFixture();
            fixture.LinkManager
                .Setup(x => x.Create(1,2))
                .Returns(async () => { return null; });

            var controller = fixture.GetLinksController();
            var actionResult =await controller.AcceptInvite(1,2);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .LinkManager
                .Verify(x => x.Create(1,2), Times.Once);
        }

        [Fact]
        public async void ShouldDeleteLink()
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
                .Returns(async () => { return link; });

            var controller = fixture.GetLinksController();
            var actionResult =await controller.DeleteLink(1,1);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .LinkManager
                .Verify(x => x.Delete(1,1), Times.Once);
        }

        [Fact]
        public async void ShouldReturnNotFoundOnDelete()
        {
            var fixture = new LinksControllerFixture();
            fixture.LinkManager
                .Setup(x => x.Delete(1, 1));
            fixture.LinkManager
                .Setup(x => x.ReadLink(1, 1))
                .Returns(async () => { return null; });

            var controller = fixture.GetLinksController();
            var actionResult =await controller.DeleteLink(1, 1);
            actionResult.Should().BeOfType<NotFoundResult>();

            fixture
                .LinkManager
                .Verify(x => x.Delete(1,1), Times.Never);
        }
        [Fact]
        public async void ShouldPutLeaders()
        {
            var fixture = new LinksControllerFixture();
            var oldLeaders = new List<int>() { 1 };
            var newLeaders = new List<int>() { 2 };
            var link = new TeamLink()
            {
                ReportingTMId = 1,
                LeaderTMId = 2
            };
            var linkList = new List<ITeamLink>() { link };
            var dto = new IntListDto() {Leaders= oldLeaders, Followers= newLeaders };
            fixture.LinkManager
                .Setup(x => x.ReadLeaders(1)).Returns(async () => { return linkList; });
            fixture.LinkManager
                .Setup(x => x.UpdateLeaders(1, oldLeaders, newLeaders));

            var controller = fixture.GetLinksController();
            var actionResult =await controller.PutLeaders(dto, 1);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .LinkManager
                .Verify(x => x.ReadLeaders(1), Times.Once);
        }
        [Fact]
        public async void ShouldPutFollowers()
        {
            var fixture = new LinksControllerFixture();
            var oldLeaders = new List<int>() { 1 };
            var newLeaders = new List<int>() { 2 };
            var link = new TeamLink()
            {
                ReportingTMId = 1,
                LeaderTMId = 2
            };
            var linkList = new List<ITeamLink>() { link };
            var dto = new IntListDto() { Leaders = oldLeaders, Followers = newLeaders };
            fixture.LinkManager
                .Setup(x => x.ReadReportingTMs(1)).Returns(async () => { return linkList; });
            fixture.LinkManager
                .Setup(x => x.UpdateFollowers(1, oldLeaders, newLeaders));

            var controller = fixture.GetLinksController();
            var actionResult =await controller.PutFollowers(dto, 1);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .LinkManager
                .Verify(x => x.ReadReportingTMs(1), Times.Once);
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
