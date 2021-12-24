using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class IndividualOldReportTests
    {
        [Fact]
        public void ShouldCreateIndividualOldReport()
        {
            string name = "name";
            string lastName = "lastName";
            var creationDate = DateTime.Now;
            var oldReport = new IndividualOldReport
            {
               AuthorId=1,
               FirstName= name,
               Date= creationDate,
               LastName= lastName,
               StatusLevel=1
            };
            Assert.Equal(name, oldReport.FirstName);
            Assert.Equal(lastName, oldReport.LastName);
            Assert.Equal(1, oldReport.AuthorId);
            Assert.Equal(1, oldReport.StatusLevel);
            Assert.Equal(creationDate, oldReport.Date);
        }
    }
}
