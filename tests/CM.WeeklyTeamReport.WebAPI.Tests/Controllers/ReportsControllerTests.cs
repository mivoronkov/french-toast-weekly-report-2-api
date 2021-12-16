using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Dto.Implementations;
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
                .Setup(x => x.readAll(1,1))
                .Returns(new List<ReportsDto>() {
                    GetReportDto(1),
                    GetReportDto(2)
                });
            var controller = fixture.GetReportsController();
            var teamMembers = (ICollection<ReportsDto>)((OkObjectResult)controller.Get(1,1)).Value;

            teamMembers.Should().NotBeNull();
            teamMembers.Should().HaveCount(2);

            fixture
                .WeeklyReportManager
                .Verify(x => x.readAll(1,1), Times.Once);
        }

        [Fact]
        public void ShouldReturnSingleReport()
        {
            var fixture = new ReportsControllerFixture();
            fixture.WeeklyReportManager
                .Setup(x => x.read(1,1,56))
                .Returns(GetReportDto(56));
            var controller = fixture.GetReportsController();
            var teamMembers = (ReportsDto)((OkObjectResult)controller.Get(1,1,56)).Value;

            teamMembers.Should().NotBeNull();

            fixture
                .WeeklyReportManager
                .Verify(x => x.read(1,1,56), Times.Once);
        }

        [Fact]
        public void ShouldCreateReport()
        {
            var fixture = new ReportsControllerFixture();
            var report = GetReport();
            var reportDto = GetReportDto();
            fixture.WeeklyReportManager
                .Setup(x => x.create(reportDto))
                .Returns(report);
            var controller = fixture.GetReportsController();
            var returnedTM = (WeeklyReport)((CreatedResult)controller.Post(reportDto, 1, 1)).Value;

            returnedTM.Should().NotBeNull();
            returnedTM.ID.Should().NotBe(0);

            fixture
                .WeeklyReportManager
                .Verify(x => x.create(reportDto), Times.Once);
        }

        [Fact]
        public void ShouldUpdateReport()
        {
            var fixture = new ReportsControllerFixture();
            var report = GetReport();
            var reportDto = GetReportDto();
            fixture.WeeklyReportManager
                .Setup(x => x.read(1,1,report.ID))
                .Returns(reportDto);
            fixture.WeeklyReportManager
                .Setup(x => x.update(reportDto, reportDto));
            report.HighThisWeek = "High 2";
            var controller = fixture.GetReportsController();
            var actionResult = controller.Put(reportDto, 1,1, report.ID);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .WeeklyReportManager
                .Verify(x => x.read(1,1,report.ID), Times.Once);
            fixture
                .WeeklyReportManager
                .Verify(x => x.update(reportDto, reportDto), Times.Once);
        }

        [Fact]
        public void ShouldDeleteReport()
        {
            var fixture = new ReportsControllerFixture();
            var report = GetReport();
            var reportDto = GetReportDto();
            fixture.WeeklyReportManager
                .Setup(x => x.delete(1,1, report.ID));
            fixture.WeeklyReportManager
                .Setup(x => x.read(1,1,report.ID))
                .Returns(reportDto);

            var controller = fixture.GetReportsController();
            var actionResult = controller.Delete(1,1, report.ID);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .WeeklyReportManager
                .Verify(x => x.delete(1,1,report.ID), Times.Once);
        }
        [Fact]
        public void ShouldReturnNotFoundOnReadAll()
        {
            var fixture = new ReportsControllerFixture();
            fixture.WeeklyReportManager
                .Setup(x => x.readAll(1,1))
                .Returns((List<ReportsDto>)null);
            var controller = fixture.GetReportsController();
            var reports = controller.Get(1,1);

            reports.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public void ShouldReturnNotFoundOnRead()
        {
            var fixture = new ReportsControllerFixture();
            fixture.WeeklyReportManager
                .Setup(x => x.read(1, 1,1))
                .Returns((ReportsDto)null);
            var controller = fixture.GetReportsController();
            var report = controller.Get(1,1,1);

            report.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public void ShouldReturnNoContentOnPost()
        {
            var fixture = new ReportsControllerFixture();
            var reportDto = GetReportDto();
            fixture.WeeklyReportManager
                .Setup(x => x.create(reportDto))
                .Returns((WeeklyReport)null);
            var controller = fixture.GetReportsController();
            var report = controller.Post(reportDto,1,1);

            report.Should().BeOfType<NoContentResult>();
        }
        [Fact]
        public void ShouldReturnNotFoundOnPut()
        {
            var fixture = new ReportsControllerFixture();
            var reportDto = GetReportDto();
            fixture.WeeklyReportManager
                .Setup(x => x.read(1, 1,1))
                .Returns((ReportsDto)null);
            var controller = fixture.GetReportsController();
            var report = controller.Put(reportDto, 1, 1,1);

            report.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public void ShouldReturnNotFoundOnDelete()
        {
            var fixture = new ReportsControllerFixture();
            var reportDto = GetReportDto();
            fixture.WeeklyReportManager
                .Setup(x => x.read(1, 1,1))
                .Returns((ReportsDto)null);
            var controller = fixture.GetReportsController();
            var report = controller.Delete(1, 1,1);

            report.Should().BeOfType<NotFoundResult>();
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
        private ReportsDto GetReportDto(int id = 1)
        {
            return new ReportsDto
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

            public WeeklyReportController GetReportsController()
            {
                return new WeeklyReportController(WeeklyReportManager.Object);
            }
        }
    }
}
