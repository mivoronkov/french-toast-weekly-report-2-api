using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Managers.Implementations;
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
    public class DateTimeManagerTests
    {
        [Fact]
        public void ShouldGetToday()
        {
            var manger = new DateTimeManager();
            var date = manger.TakeDateTime();
            Assert.IsType<DateTime>(date);
            Assert.Equal(DateTime.Now.ToShortDateString(), date.ToShortDateString());
        }
        [Fact]
        public void ShouldGetYesterday()
        {
            var manger = new DateTimeManager();
            var date = manger.TakeDateTime(-1);
            Assert.Equal(DateTime.Now.AddDays(-1).ToShortDateString(), date.ToShortDateString());
        }
        [Fact]
        public void ShouldGetMonday()
        {
            var manger = new DateTimeManager();
            var date = manger.TakeMonday();
            var monday = DateTime.Now.FirstDateInWeek(IWeeklyReport.StartOfWeek);
            Assert.Equal(monday.ToShortDateString(), date.ToShortDateString());
        }
        [Fact]
        public void ShouldGetLastMonday()
        {
            var manger = new DateTimeManager();
            var date = manger.TakeMonday(-7);
            var monday = DateTime.Now.AddDays(-7).FirstDateInWeek(IWeeklyReport.StartOfWeek);
            Assert.Equal(monday.ToShortDateString(), date.ToShortDateString());
        }
    }
}
