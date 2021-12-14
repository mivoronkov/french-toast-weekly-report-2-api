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
    public class ReportsControllerTests
    {
        [Fact]
        public void ShouldReturnAllReports()
        {
            var fixture = new ReportsControllerFixture();
            fixture.WeeklyReportManager
                .Setup(x => x.readAlWeeklyReports())
                .Returns(new List<IWeeklyReport>() {
                    GetReport(1),
                    GetReport(2)
                });
            var controller = fixture.GetCompaniesController();
            var teamMembers = (ICollection<WeeklyReport>)((OkObjectResult)controller.Get()).Value;

            teamMembers.Should().NotBeNull();
            teamMembers.Should().HaveCount(2);

            fixture
                .WeeklyReportManager
                .Verify(x => x.readAlWeeklyReports(), Times.Once);
        }

        [Fact]
        public void ShouldReturnSingleReport()
        {
            var fixture = new ReportsControllerFixture();
            fixture.WeeklyReportManager
                .Setup(x => x.readWeeklyReport(56))
                .Returns(GetReport(56));
            var controller = fixture.GetCompaniesController();
            var teamMembers = (WeeklyReport)((OkObjectResult)controller.Get(56)).Value;

            teamMembers.Should().NotBeNull();

            fixture
                .WeeklyReportManager
                .Verify(x => x.readWeeklyReport(56), Times.Once);
        }

        [Fact]
        public void ShouldCreateReport()
        {
            var fixture = new ReportsControllerFixture();
            var report = GetReport();
            fixture.WeeklyReportManager
                .Setup(x => x.createWeeklyReport(report))
                .Returns(report);
            var controller = fixture.GetCompaniesController();
            var returnedTM = (WeeklyReport)((CreatedResult)controller.Post(report)).Value;

            returnedTM.Should().NotBeNull();
            returnedTM.ID.Should().NotBe(0);

            fixture
                .WeeklyReportManager
                .Verify(x => x.createWeeklyReport(report), Times.Once);
        }

        [Fact]
        public void ShouldUpdateReport()
        {
            var fixture = new ReportsControllerFixture();
            var report = GetReport();
            fixture.WeeklyReportManager
                .Setup(x => x.readWeeklyReport(report.ID))
                .Returns(report);
            fixture.WeeklyReportManager
                .Setup(x => x.updateWeeklyReport(report));
            report.HighThisWeek = "High 2";
            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Put(report.ID, report);
            actionResult.Should().BeOfType<OkObjectResult>();

            fixture
                .WeeklyReportManager
                .Verify(x => x.readWeeklyReport(report.ID), Times.Once);
            fixture
                .WeeklyReportManager
                .Verify(x => x.updateWeeklyReport(report), Times.Once);
        }

        [Fact]
        public void ShouldDeleteReport()
        {
            var fixture = new ReportsControllerFixture();
            var report = GetReport();
            fixture.WeeklyReportManager
                .Setup(x => x.deleteWeeklyReport(report.ID));
            fixture.WeeklyReportManager
                .Setup(x => x.readWeeklyReport(report.ID))
                .Returns(report);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Delete(report.ID);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .WeeklyReportManager
                .Verify(x => x.deleteWeeklyReport(report.ID), Times.Once);
        }

        private WeeklyReport GetReport(int id = 1)
        {
            return new WeeklyReport
            {
                ID = id,
                AuthorId = id,
                MoraleGradeId = id * 3,
                StressGradeId = id * 3 + 1,
                WorkloadGradeId = id * 3 + 2
            };
        }

        public class ReportsControllerFixture
        {
            public ReportsControllerFixture()
            {
                WeeklyReportManager = new Mock<IWeeklyReportManager>();
            }

            public Mock<IWeeklyReportManager> WeeklyReportManager { get; private set; }

            public WeeklyReportController GetCompaniesController()
            {
                return new WeeklyReportController(WeeklyReportManager.Object);
            }
        }
    }
}
