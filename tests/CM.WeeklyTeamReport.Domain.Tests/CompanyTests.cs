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

        [Fact]
        public void ShouldHaveTeamMembers()
        {
            string companyName = "CompanyName";
            var company = new Company(companyName);

            var m1 = new TeamMember("1", "1", "1", "email@example.com");
            var m2 = new TeamMember("2", "2", "2", "email@example.com");
            var m3 = new TeamMember("3", "3", "3", "email@example.com");

            // Can assign
            company.TeamMembers = new HashSet<TeamMember>() { m1, m2 };
            Assert.Equal(2, company.TeamMembers.Count);
            Assert.Contains(m1, company.TeamMembers);
            Assert.Contains(m2, company.TeamMembers);

            // Can add
            company.TeamMembers.Add(m3);
            Assert.Equal(3, company.TeamMembers.Count);
            Assert.Contains(m1, company.TeamMembers);
            Assert.Contains(m2, company.TeamMembers);
            Assert.Contains(m3, company.TeamMembers);

            // Can remove
            company.TeamMembers.Remove(m1);
            Assert.Equal(2, company.TeamMembers.Count);
            Assert.Contains(m2, company.TeamMembers);
            Assert.Contains(m3, company.TeamMembers);
        }
    }
}
