using CM.WeeklyTeamReport.Domain.Dto;
using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class HistoryReportDtoTests
    {
        [Fact]
        public void ShouldCreateHistoryReportDto()
        {
            var workloadInformation = new WeeklyInformation()
            {
                StateLevel = 1,
                Comments = "fd",
                StateName = "Workload"
            };
            var informationList = new List<IWeeklyInformation>() { workloadInformation };
            var anythingNotations = new WeeklyNotations() { Title = "Anything Else", Text = "P" };
            var notationsList = new List<IWeeklyNotations>() { anythingNotations };
            var dto = new HistoryReportDto
            {
                ID = 1,
                AvatarPath="p",
                FirstName="f",
                LastName="l",
                WeeklyInformation = informationList,
                WeeklyNotations = notationsList

            };
            Assert.Equal(1, dto.ID);
            Assert.Equal("p", dto.AvatarPath);
            Assert.Equal("l", dto.LastName);
            Assert.Equal("f", dto.FirstName);
            Assert.Equal(informationList, dto.WeeklyInformation);
            Assert.Equal(notationsList, dto.WeeklyNotations);
        }
    }
}
