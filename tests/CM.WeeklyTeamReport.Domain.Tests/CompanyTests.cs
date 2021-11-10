using System;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class CompanyTests
    {
        [Fact]
        public void ShouldCreateCompany()
        {
            string companyName = "CompanyName";
            var company = new Company(companyName);
            Assert.Equal(companyName, company.Name);
        }

        [Fact]
        public void ShouldAllowToSetName()
        {
            string companyName = "CompanyName";
            var company = new Company(companyName);
            Assert.Equal(companyName, company.Name);

            companyName = "AnotherCompanyName";
            company.Name = companyName;
            Assert.Equal(companyName, company.Name);
        }
    }
}
