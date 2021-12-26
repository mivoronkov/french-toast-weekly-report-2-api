using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Dto.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Managers.Interfaces;
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
                .Setup(x => x.delete(reportDto));
            fixture.WeeklyReportManager
                .Setup(x => x.read(1,1,report.ID))
                .Returns(reportDto);

            var controller = fixture.GetReportsController();
            var actionResult = controller.Delete(1,1, report.ID);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .WeeklyReportManager
                .Verify(x => x.delete(reportDto), Times.Once);
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

        [Theory]
        [InlineData("current", 0)]
        [InlineData("previous", -7)]
        [InlineData("", 0)]
        public async void ShouldGetTeamReports(string week, int dayShift)
        {
            var day = DateTime.Now;
            var fixture = new ReportsControllerFixture();
            var fullReport = GetFullWeeklyReport(1);
            var fullReportList = new List<IFullWeeklyReport>() { fullReport };
            var reportDto = new HistoryReportDto() { };
            var dtoReportList = new List<HistoryReportDto>() { reportDto };
            fixture.WeeklyReportManager
                .Setup(x => x.ReadReportHistory(1, 1, day, day, ""))
                .Returns(async ()=> { return dtoReportList; });
            fixture.DateTimeManager.Setup(x => x.TakeDateTime(dayShift)).Returns(day);
            fixture.DateTimeManager.Setup(x => x.TakeMonday(day)).Returns(day);
            fixture.DateTimeManager.Setup(x => x.TakeSunday(day)).Returns(day);

            var controller = fixture.GetReportsController();
            var report = await controller.GetTeamReports("", week, 1, 1);

            fixture
                .WeeklyReportManager.Verify(x => x.ReadReportHistory(1, 1, day, day, ""), Times.Once);
            fixture
                .DateTimeManager.Verify(x => x.TakeMonday(day), Times.Once);
            fixture
                .DateTimeManager.Verify(x => x.TakeSunday(day), Times.Once);
            fixture
                .DateTimeManager.Verify(x => x.TakeDateTime(dayShift), Times.Once);
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
        private IFullWeeklyReport GetFullWeeklyReport(int id =1)
        {
            const string HighThisWeek = "My high this week";
            const string LowThisWeek = "My low this week";
            var reportDate = new DateTime(2021, 11, 10);
            var anythingElse = "Anything else";
            var commentary = "nope";

            return new FullWeeklyReport
            {
                ID = id,
                AuthorId = 1,
                MoraleGradeId = 1,
                StressGradeId = 1,
                WorkloadGradeId = 1,
                MoraleLevel = 1,
                StressLevel = 1,
                WorkloadLevel = 1,
                MoraleCommentary = commentary,
                StressCommentary = commentary,
                WorkloadCommentary = commentary,
                HighThisWeek = HighThisWeek,
                LowThisWeek = LowThisWeek,
                AnythingElse = anythingElse,
                Date = reportDate
            };
        }
        public class ReportsControllerFixture
        {
            public ReportsControllerFixture()
            {
                WeeklyReportManager = new Mock<IWeeklyReportManager>();
                DateTimeManager= new Mock<IDateTimeManager>();
            }

            public Mock<IWeeklyReportManager> WeeklyReportManager { get; private set; }
            public Mock<IDateTimeManager> DateTimeManager { get; private set; }

            public WeeklyReportController GetReportsController()
            {
                return new WeeklyReportController(WeeklyReportManager.Object, DateTimeManager.Object);
            }
        }
    }
}
