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
        public void ShouldReadAllLinksToLeaders()
        {
            var fixture = new TeamLinkManagerFixture();
            var link1 = new TeamLink { LeaderTMId=1, ReportingTMId=2 };
            var link2 = new TeamLink { LeaderTMId = 3, ReportingTMId = 2 };
            var readedLeaders = new List<ITeamLink>() { link1, link2};

            fixture.LinkRepository.Setup(x => x.ReadLeaders(1)).Returns(readedLeaders);
            var manager = fixture.GetTeamLinkManager();

            var links = (List <ITeamLink>)manager.ReadLeaders(1);
            fixture.LinkRepository.Verify(x => x.ReadLeaders(1), Times.Once);

            links.Should().HaveCount(2);
            links[0].LeaderTMId.Should().Be(1);
            links[0].ReportingTMId.Should().Be(2);
            links[1].LeaderTMId.Should().Be(3);
            links[1].ReportingTMId.Should().Be(2);
        }
        [Fact]
        public void ShouldReadAllLinksToReportingTMs()
        {
            var fixture = new TeamLinkManagerFixture();
            var link1 = new TeamLink { LeaderTMId = 1, ReportingTMId = 1 };
            var link2 = new TeamLink { LeaderTMId = 1, ReportingTMId = 2 };
            var readedLeaders = new List<ITeamLink>() { link1, link2 };

            fixture.LinkRepository.Setup(x => x.ReadReportingTMs(1)).Returns(readedLeaders);
            var manager = fixture.GetTeamLinkManager();

            var links = (List<ITeamLink>)manager.ReadReportingTMs(1);
            fixture.LinkRepository.Verify(x => x.ReadReportingTMs(1), Times.Once);

            links.Should().HaveCount(2);
            links[0].LeaderTMId.Should().Be(1);
            links[0].ReportingTMId.Should().Be(1);
            links[1].LeaderTMId.Should().Be(1);
            links[1].ReportingTMId.Should().Be(2);
        }
        [Fact]
        public void ShouldInvokeDelete()
        {
            var fixture = new TeamLinkManagerFixture();
            fixture.LinkRepository.Setup(x => x.Delete(1,1));
            var manager = fixture.GetTeamLinkManager();

            manager.Delete(1,1);
            fixture.LinkRepository.Verify(x => x.Delete(1,1), Times.Once);
        }
        [Fact]
        public void ShouldCreateLink()
        {
            var fixture = new TeamLinkManagerFixture();
            var link = new TeamLink { LeaderTMId = 1, ReportingTMId = 1 };
            fixture.LinkRepository.Setup(x => x.Create(1, 1)).Returns(link);
            var manager = fixture.GetTeamLinkManager();

            var result = manager.Create(1, 1);
            fixture.LinkRepository.Verify(x => x.Create(1, 1), Times.Once);
            result.LeaderTMId.Should().Be(link.LeaderTMId);
            result.ReportingTMId.Should().Be(link.ReportingTMId);
        }


        public class TeamLinkManagerFixture
        {
            public TeamLinkManagerFixture()
            {
                LinkRepository = new Mock<ITeamLinkRepository>();
            }

            public Mock<ITeamLinkRepository> LinkRepository { get; private set; }

            public TeamLinkManager GetTeamLinkManager()
            {
                return new TeamLinkManager(LinkRepository.Object);
            }
        }
    }
}
