using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Dto.Implementations;
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
    public class WeeklyReportManagerTests
    {
        [Fact]
        public void ShouldReadAllCompanies()
        {
            var fixture = new WeeklyReportManagerFixture();

            var report1 = GetFullReport(1, 1);
            var report2 = GetFullReport(2, 1);
            var readedReports = new List<IFullWeeklyReport>() { report1, report2 };
            var reportDto1 = GetReportDto(1, 1);
            var reportDto2 = GetReportDto(2, 1);

            fixture.WeeklyReportRepository.Setup(x => x.ReadAll(1,1)).Returns(readedReports);
            fixture.ReportCommands.Setup(x => x.fullReportToDto(report1)).Returns(reportDto1);
            fixture.ReportCommands.Setup(x => x.fullReportToDto(report2)).Returns(reportDto2);

            var manager = fixture.GetReportManager();
            var reports = (List<ReportsDto>)manager.readAll(1,1);
            fixture.WeeklyReportRepository.Verify(x => x.ReadAll(1,1), Times.Once);
            fixture.ReportCommands.Verify(x => x.fullReportToDto(report1), Times.Once);
            fixture.ReportCommands.Verify(x => x.fullReportToDto(report2), Times.Once);
        }
        [Fact]
        public void ShoulReturnNullOndReadAllCompanies()
        {
            var fixture = new WeeklyReportManagerFixture();

            var readedReports = new List<IFullWeeklyReport>();

            fixture.WeeklyReportRepository.Setup(x => x.ReadAll(1, 1)).Returns(readedReports);

            var manager = fixture.GetReportManager();
            var reports = (List<ReportsDto>)manager.readAll(1, 1);
            fixture.WeeklyReportRepository.Verify(x => x.ReadAll(1, 1), Times.Once);
            reports.Should().BeNull();
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(5, 5, 5)]
        public void ShouldReadReportByID(int companyId, int memberId, int reportId)
        {
            var fixture = new WeeklyReportManagerFixture();
            var report = GetReport(1, 1);
            var fullReport = GetFullReport(1, 1);
            var reportDto = GetReportDto(1, 1);

            fixture.WeeklyReportRepository.Setup(el => el.Read(companyId, memberId, reportId)).Returns(fullReport);
            fixture.ReportCommands.Setup(el => el.fullReportToDto(fullReport)).Returns(reportDto);

            var manager = fixture.GetReportManager();
            var radedMember = manager.read(companyId, memberId, reportId);
            radedMember.Should().BeOfType<ReportsDto>();
            fixture.ReportCommands.Verify(el => el.fullReportToDto(fullReport), Times.Once);
            fixture.WeeklyReportRepository.Verify(el => el.Read(companyId, memberId, reportId), Times.Once);
        }
        [Fact]
        public void ShouldReadReportsInInterval()
        {
            var fixture = new WeeklyReportManagerFixture();
            var report = GetReport(1, 1);
            var fullReport = GetFullReport(1, 1);
            var reportDto = GetReportDto(1, 1);
            var fullReportList = new List<IFullWeeklyReport>() { fullReport };
            var start = new DateTime();
            var end = new DateTime().AddDays(5);

            fixture.WeeklyReportRepository.Setup(el => el.ReadReportsInInterval(1, 1, start, end, "")).Returns(fullReportList);
            fixture.ReportCommands.Setup(el => el.fullReportToDto(fullReport)).Returns(reportDto);

            var manager = fixture.GetReportManager();
            var reportr = manager.ReadReportsInInterval(1, 1, start, end);
            reportr.Should().BeOfType<List<ReportsDto>>();
            fixture.ReportCommands.Verify(el => el.fullReportToDto(fullReport), Times.Once);
            fixture.WeeklyReportRepository.Verify(el => el.ReadReportsInInterval(1, 1, start, end, ""), Times.Once);
        }
        [Fact]
        public void ShouldReadReportsInCorrectInterval()
        {
            var fixture = new WeeklyReportManagerFixture();
            var report = GetReport(1, 1);
            var fullReport = GetFullReport(1, 1);
            var reportDto = GetReportDto(1, 1);
            var fullReportList = new List<IFullWeeklyReport>() { fullReport };
            var start = new DateTime();
            var end = new DateTime().AddDays(5);

            fixture.WeeklyReportRepository.Setup(el => el.ReadReportsInInterval(1, 1, start, end, "")).Returns(fullReportList);
            fixture.ReportCommands.Setup(el => el.fullReportToDto(fullReport)).Returns(reportDto);

            var manager = fixture.GetReportManager();
            var reportr = manager.ReadReportsInInterval(1, 1, end, start);
            reportr.Should().BeOfType<List<ReportsDto>>();
            fixture.ReportCommands.Verify(el => el.fullReportToDto(fullReport), Times.Once);
            fixture.WeeklyReportRepository.Verify(el => el.ReadReportsInInterval(1, 1, start, end, ""), Times.Once);
        }
        [Fact]
        public void ShouldReturnNullOnReadReportsInInterval()
        {
            var fixture = new WeeklyReportManagerFixture();
            var report = GetReport(1, 1);
            var fullReport = GetFullReport(1, 1);
            var reportDto = GetReportDto(1, 1);
            var fullReportList = new List<IFullWeeklyReport>() {  };
            var start = new DateTime();
            var end = new DateTime().AddDays(5);

            fixture.WeeklyReportRepository.Setup(el => el.ReadReportsInInterval(1, 1, start, end, "")).Returns(fullReportList);

            var manager = fixture.GetReportManager();
            var reportr = manager.ReadReportsInInterval(1, 1, start, end);
            reportr.Should().BeNull();
            fixture.WeeklyReportRepository.Verify(el => el.ReadReportsInInterval(1, 1, start, end, ""), Times.Once);
        }
        [Fact]
        public void ShouldDeleteReport()
        {
            var fixture = new WeeklyReportManagerFixture();
            var reportDto = GetReportDto(1, 1);
            var report = GetReport(1, 1);

            fixture.ReportCommands.Setup(el => el.dtoToReport(reportDto)).Returns(report);
            fixture.WeeklyReportRepository.Setup(x => x.Delete(report));
            var manager = fixture.GetReportManager();

            manager.delete(reportDto);
            fixture.WeeklyReportRepository.Verify(el => el.Delete(report), Times.Once);
        }

        [Fact]
        public void ShouldCreateReport()
        {
            var fixture = new WeeklyReportManagerFixture();
            var report = GetReport(1, 1);
            var reportDto = GetReportDto(1, 1);

            fixture.WeeklyReportRepository.Setup(el => el.Create(report)).Returns(report);
            fixture.ReportCommands.Setup(el => el.dtoToReport(reportDto)).Returns(report);

            var manager = fixture.GetReportManager();
            var newReport = manager.create(reportDto);
            newReport.Should().BeOfType<WeeklyReport>();
            fixture.WeeklyReportRepository.Verify(el => el.Create(report), Times.Once);
            fixture.ReportCommands.Verify(el => el.dtoToReport(reportDto), Times.Once);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 5)]
        public void ShouldUpdateMember(int id, int authorId)
        {
            var fixture = new WeeklyReportManagerFixture();
            var oldReportDto = GetReportDto(id, authorId);
            var newReportDto = GetReportDto(0, authorId);
            var newReport = GetReport(id, authorId);

            fixture.ReportCommands.Setup(el => el.dtoToReport(newReportDto)).Returns(newReport);
            fixture.WeeklyReportRepository.Setup(el => el.Update(newReport));

            var manager = fixture.GetReportManager();
            manager.update(oldReportDto, newReportDto);
            fixture.ReportCommands.Verify(el => el.dtoToReport(newReportDto), Times.Once);
            fixture.WeeklyReportRepository.Verify(el => el.Update(newReport), Times.Once);

            newReport.ID.Should().Be(newReportDto.ID);
        }

        [Theory]
        [InlineData("morale", "Morale")]
        [InlineData("stress", "Stress")]
        [InlineData("workload", "Workload")]
        [InlineData("overall", "Overall")]
        [InlineData("", "Overall")]
        public void ShouldReadAverageOldReports(string filter, string status)
        {
            var fixture = new WeeklyReportManagerFixture();
            var date = DateTime.Now.FirstDateInWeek(IWeeklyReport.StartOfWeek);
            var oldReport = new OldReport()
            {
                Date = date,
                StatusLevel = 2
            };
            var oldReportList = new List<IOldReport>() { oldReport };
            fixture.WeeklyReportRepository.Setup(el => el.ReadAverageOldReports(1, 1, date, date, "extended", filter))
                .Returns(oldReportList);

            var manager = fixture.GetReportManager();
            var reportr = manager.ReadAverageOldReports(1, 1, date, date, "extended", filter);

            reportr.FilterName.Should().Be(status);
            fixture.WeeklyReportRepository.Verify(el => 
            el.ReadAverageOldReports(1, 1, date, date, "extended", filter), Times.Once);
            
        }
        [Fact]
        public void ShouldReadReportHistory()
        {
            var fixture = new WeeklyReportManagerFixture();
            var start = DateTime.Now.FirstDateInWeek(IWeeklyReport.StartOfWeek);
            var oldReportList = new List<IFullWeeklyReport>() { };

            fixture.WeeklyReportRepository.Setup(el => el.ReadReportsInInterval(1, 1, start, start, ""))
                .Returns(oldReportList);

            var manager = fixture.GetReportManager();
            var reportr = manager.ReadReportHistory(1, 1, start, start, "");
            reportr.Should().NotBeNull();
        }
        [Fact]
        public void ShouldBeNullReadAverageOldReports()
        {
            var fixture = new WeeklyReportManagerFixture();
            var date = DateTime.Now.FirstDateInWeek(IWeeklyReport.StartOfWeek);
            var oldReportList = new List<IOldReport>() {  };


            fixture.WeeklyReportRepository.Setup(el => el.ReadAverageOldReports(1, 1, date, date, "extended", ""))
                .Returns(oldReportList);

            var manager = fixture.GetReportManager();
            var reportr = manager.ReadAverageOldReports(1, 1, date, date, "extended", "");
            reportr.Should().BeNull();
        }
        [Theory]
        [InlineData("2021-12-20")]
        [InlineData("2021-12-13")]
        [InlineData("2021-12-6")]
        public void ReadIndividualOldReports(DateTime recordDate)
        {
            var fixture = new WeeklyReportManagerFixture();
            var currentDate = new DateTime(2021, 12, 20);
            var individualOldReport = new IndividualOldReport()
            {
                Date = recordDate,
                StatusLevel = 2,
                FirstName = "tom",
                LastName = "uncle",
                AuthorId=1
            };
            var listOldReports = new List<IIndividualOldReport>() { individualOldReport };
            fixture.WeeklyReportRepository.Setup(el => el.ReadMemberOldReports(1, 1, currentDate, currentDate, "", ""))
                .Returns(listOldReports);

            var manager = fixture.GetReportManager();
            var reportr = manager.ReadIndividualOldReports(1, 1, currentDate, currentDate, "", "");
            var enumerator = reportr.GetEnumerator();
            enumerator.MoveNext();
            var memberReport = enumerator.Current;

            reportr.Count.Should().Be(1);
            memberReport.LastName.Should().Be(individualOldReport.LastName);
            memberReport.FirstName.Should().Be(individualOldReport.FirstName);
            memberReport.AuthorId.Should().Be(individualOldReport.AuthorId);
            int weekIndex = (int)((currentDate - recordDate).TotalDays / 7);
            memberReport.StatusLevel[weekIndex].Should().Be(2);
            fixture.WeeklyReportRepository.Verify(el =>
            el.ReadMemberOldReports(1, 1, currentDate, currentDate, "", ""), Times.Once);

        }

        public IWeeklyReport GetReport(int id, int authorId)
        {
            var moraleGrade = new Grade { Level = Level.VeryLow };
            var stressGrade = new Grade { Level = Level.Low };
            var workloadGrade = new Grade { Level = Level.Average, Commentary = "Usual amount of stress" };
            const string HighThisWeek = "My high this week";
            const string LowThisWeek = "My low this week";
            var reportDate = new DateTime(2021, 11, 10);
            var expectedWeekStart = new DateTime(2021, 11, 8);
            var expectedWeekEnd = new DateTime(2021, 11, 14);
            var anythingElse = "Anything else";

            return new WeeklyReport
            {
                ID = id,
                AuthorId = authorId,
                MoraleGradeId = 1,
                StressGradeId = 1,
                WorkloadGradeId = 1,
                MoraleGrade = moraleGrade,
                StressGrade = stressGrade,
                WorkloadGrade = workloadGrade,
                HighThisWeek = HighThisWeek,
                LowThisWeek = LowThisWeek,
                AnythingElse = anythingElse,
                Date = reportDate
            };
        }
        public IFullWeeklyReport GetFullReport(int id, int authorId)
        {
            const string HighThisWeek = "My high this week";
            const string LowThisWeek = "My low this week";
            var reportDate = new DateTime(2021, 11, 10);
            var anythingElse = "Anything else";
            var commentary = "nope";

            return new FullWeeklyReport
            {
                ID = id,
                AuthorId = authorId,
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
        public ReportsDto GetReportDto(int id, int authorId)
        {
            var moraleGrade = new Grade { Level = Level.VeryLow };
            var stressGrade = new Grade { Level = Level.Low };
            var workloadGrade = new Grade { Level = Level.Average, Commentary = "Usual amount of stress" };
            const string HighThisWeek = "My high this week";
            const string LowThisWeek = "My low this week";
            var reportDate = new DateTime(2021, 11, 10);
            var expectedWeekStart = new DateTime(2021, 11, 8);
            var expectedWeekEnd = new DateTime(2021, 11, 14);
            var anythingElse = "Anything else";

            return new ReportsDto
            {
                ID = id,
                AuthorId = authorId,
                MoraleGradeId = 1,
                StressGradeId = 1,
                WorkloadGradeId = 1,
                MoraleGrade = moraleGrade,
                StressGrade = stressGrade,
                WorkloadGrade = workloadGrade,
                HighThisWeek = HighThisWeek,
                LowThisWeek = LowThisWeek,
                AnythingElse = anythingElse,
                Date = reportDate
            };
        }
        public class WeeklyReportManagerFixture
        {
            public WeeklyReportManagerFixture()
            {
                WeeklyReportRepository = new Mock<IWeeklyReportRepository>();
                ReportCommands = new Mock<IReportCommands>();
            }

            public Mock<IWeeklyReportRepository> WeeklyReportRepository { get; private set; }
            public Mock<IReportCommands> ReportCommands { get; private set; }

            public WeeklyReportManager GetReportManager()
            {
                return new WeeklyReportManager(WeeklyReportRepository.Object, ReportCommands.Object);
            }
        }
    }
}
