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
    public class TeamLinkManagerTests
    {
        [Fact]
        public async void ShouldReadAllLinksToLeaders()
        {
            var fixture = new TeamLinkManagerFixture();
            var link1 = new TeamLink { LeaderTMId=1, ReportingTMId=2 };
            var link2 = new TeamLink { LeaderTMId = 3, ReportingTMId = 2 };
            var readedLeaders = new List<ITeamLink>() { link1, link2};

            fixture.LinkRepository.Setup(x => x.ReadLeaders(1)).Returns(async ()=> { return readedLeaders; });
            var manager = fixture.GetTeamLinkManager();

            var links = (List <ITeamLink>)await manager.ReadLeaders(1);
            fixture.LinkRepository.Verify(x => x.ReadLeaders(1), Times.Once);

            links.Should().HaveCount(2);
            links[0].LeaderTMId.Should().Be(1);
            links[0].ReportingTMId.Should().Be(2);
            links[1].LeaderTMId.Should().Be(3);
            links[1].ReportingTMId.Should().Be(2);
        }
        [Fact]
        public async void ShouldReadAllLinksToReportingTMs()
        {
            var fixture = new TeamLinkManagerFixture();
            var link1 = new TeamLink { LeaderTMId = 1, ReportingTMId = 1 };
            var link2 = new TeamLink { LeaderTMId = 1, ReportingTMId = 2 };
            var readedLeaders = new List<ITeamLink>() { link1, link2 };

            fixture.LinkRepository.Setup(x => x.ReadReportingTMs(1)).Returns(async () => { return readedLeaders; });
            var manager = fixture.GetTeamLinkManager();

            var links = (List<ITeamLink>)await manager.ReadReportingTMs(1);
            fixture.LinkRepository.Verify(x => x.ReadReportingTMs(1), Times.Once);

            links.Should().HaveCount(2);
            links[0].LeaderTMId.Should().Be(1);
            links[0].ReportingTMId.Should().Be(1);
            links[1].LeaderTMId.Should().Be(1);
            links[1].ReportingTMId.Should().Be(2);
        }
        [Fact]
        public async void ShouldReadLink()
        {
            var fixture = new TeamLinkManagerFixture();
            var link = new TeamLink { LeaderTMId = 1, ReportingTMId = 1 };

            fixture.LinkRepository.Setup(x => x.ReadLink(1,1)).Returns(async () => { return link; });
            var manager = fixture.GetTeamLinkManager();

            var result = (ITeamLink)await manager.ReadLink(1, 1);
            fixture.LinkRepository.Verify(x => x.ReadLink(1, 1), Times.Once);

            result.Should().NotBeNull();
        }
        [Fact]
        public async void ShouldInvokeDelete()
        {
            var fixture = new TeamLinkManagerFixture();
            fixture.LinkRepository.Setup(x => x.Delete(1,1));
            var manager = fixture.GetTeamLinkManager();

            await manager.Delete(1,1);
            fixture.LinkRepository.Verify(x => x.Delete(1,1), Times.Once);
        }
        [Fact]
        public async void ShouldCreateLink()
        {
            var fixture = new TeamLinkManagerFixture();
            var link = new TeamLink { LeaderTMId = 1, ReportingTMId = 1 };
            fixture.LinkRepository.Setup(x => x.Create(1, 1)).Returns(async () => { return link; });
            var manager = fixture.GetTeamLinkManager();

            var result =await manager.Create(1, 1);
            fixture.LinkRepository.Verify(x => x.Create(1, 1), Times.Once);
            result.LeaderTMId.Should().Be(link.LeaderTMId);
            result.ReportingTMId.Should().Be(link.ReportingTMId);
        }
        [Fact]
        public async void ShouldUpdateLeaders()
        {
            var fixture = new TeamLinkManagerFixture();
            var manager = fixture.GetTeamLinkManager();
            int[] oldLeaders = new int[] { 1, 2, 3 };
            int[] newLeaders = new int[] { 3, 4 };
            int[] LeadersToDelete = new int[] { 1, 2 };
            int[] LeadersToAdd = new int[] {  4 };
            fixture.LinkCommand.Setup(x => x.LinksDifference(oldLeaders, newLeaders)).Returns(LeadersToAdd);
            fixture.LinkCommand.Setup(x => x.LinksDifference(newLeaders, oldLeaders)).Returns(LeadersToDelete);
            fixture.LinkRepository.Setup(x => x.DeleteLeaders(0, LeadersToDelete));
            fixture.LinkRepository.Setup(x => x.AddLeaders(0, LeadersToAdd));

            await manager.UpdateLeaders(0, oldLeaders, newLeaders);
            fixture.LinkCommand.Verify(x => x.LinksDifference(oldLeaders, newLeaders), Times.Once);
            fixture.LinkCommand.Verify(x => x.LinksDifference(newLeaders, oldLeaders), Times.Once);
            fixture.LinkRepository.Verify(x => x.DeleteLeaders(0, LeadersToDelete), Times.Once);
            fixture.LinkRepository.Verify(x => x.AddLeaders(0, LeadersToAdd), Times.Once);
        }
        [Fact]
        public async void ShouldUpdateFollowers()
        {
            var fixture = new TeamLinkManagerFixture();
            var manager = fixture.GetTeamLinkManager();
            int[] oldFollowers = new int[] { 1, 2, 3 };
            int[] newFollowers = new int[] { 3, 4 };
            int[] followersToDelete = new int[] { 1, 2 };
            int[] followersToAdd = new int[] { 4 };
            fixture.LinkCommand.Setup(x => x.LinksDifference(oldFollowers, newFollowers)).Returns(followersToAdd);
            fixture.LinkCommand.Setup(x => x.LinksDifference(newFollowers, oldFollowers)).Returns(followersToDelete);
            fixture.LinkRepository.Setup(x => x.DeleteFollowers(0, followersToDelete));
            fixture.LinkRepository.Setup(x => x.AddFollowers(0, followersToAdd));

            await manager.UpdateFollowers(0, oldFollowers, newFollowers);
            fixture.LinkCommand.Verify(x => x.LinksDifference(oldFollowers, newFollowers), Times.Once);
            fixture.LinkCommand.Verify(x => x.LinksDifference(newFollowers, oldFollowers), Times.Once);
            fixture.LinkRepository.Verify(x => x.DeleteFollowers(0, followersToDelete), Times.Once);
            fixture.LinkRepository.Verify(x => x.AddFollowers(0, followersToAdd), Times.Once);
        }
        public class TeamLinkManagerFixture
        {
            public TeamLinkManagerFixture()
            {
                LinkRepository = new Mock<ITeamLinkRepository>();
                LinkCommand = new Mock<ILinkCommands>();
            }

            public Mock<ITeamLinkRepository> LinkRepository { get; private set; }
            public Mock<ILinkCommands> LinkCommand { get; private set; }

            public TeamLinkManager GetTeamLinkManager()
            {
                return new TeamLinkManager(LinkRepository.Object, LinkCommand.Object);
            }
        }
    }
}
