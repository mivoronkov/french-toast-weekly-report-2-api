using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class CompanyTests
    {
        [Fact]
        public void ShouldCreateCompany()
        {
            string companyName = "CompanyName";
            var creationDate = DateTime.Now;
            var company = new Company { 
                Name = companyName,
                CreationDate = creationDate
            };
            Assert.Equal(companyName, company.Name);
            Assert.Equal(creationDate, company.CreationDate);
        }

        [Fact]
        public void ShouldAllowToSetName()
        {
            string companyName = "CompanyName";
            var company = new Company { Name = companyName };
            Assert.Equal(companyName, company.Name);

            companyName = "AnotherCompanyName";
            company.Name = companyName;
            Assert.Equal(companyName, company.Name);
        }
    }
}
