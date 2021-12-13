using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.IntegrationTests
{
    public class CompanyRepositoryTests
    {
        [Fact]
        public void ShouldPerformBasicCRUD()
        {
            var repository = new CompanyRepository(SetupTests.Configuration);
            var company = repository.Create(new Company { Name = "Test company" });
            company.ID.Should().NotBe(0);
            var readCompany = repository.Read(company.ID);
            readCompany.ID.Should().Be(company.ID);
            readCompany.Name.Should().Be(company.Name);
            readCompany.CreationDate.Should().Be(company.CreationDate);
            var newName = "New name";
            company.Name = newName;
            repository.Update(company);
            readCompany = repository.Read(company.ID);
            readCompany.Name.Should().Be(newName);
            repository.Delete(company);
            readCompany = repository.Read(company.ID);
            readCompany.Should().BeNull();
        }

        [Fact]
        public void ShouldReadAll()
        {
            var repository = new CompanyRepository(SetupTests.Configuration);
            var result = repository.ReadAll();
            result.Should().AllBeOfType<Company>();
        }

        [Fact]
        public void ShouldReturnEmployees()
        {
            var companyRepo = new CompanyRepository(SetupTests.Configuration);
            var company = companyRepo.Create(new Company { Name = "Test company" });
            var tmRepo = new TeamMemberRepository(SetupTests.Configuration);
            var tms = new List<TeamMember>(){
                tmRepo.Create(new TeamMember {
                    FirstName = "F1",
                    LastName = "L1",
                    Title = "T1",
                    Email = "m1@mail.com",
                    CompanyId = company.ID
                }),
                tmRepo.Create(new TeamMember
                {
                    FirstName = "F2",
                    LastName = "L2",
                    Title = "T2",
                    Email = "m2@mail.com",
                    CompanyId = company.ID
                }),
                tmRepo.Create(new TeamMember
                {
                    FirstName = "F3",
                    LastName = "L3",
                    Title = "T3",
                    Email = "m3@mail.com",
                    CompanyId = company.ID
                })
            };
            companyRepo.GetTeamMembers(company).Should().BeEquivalentTo(tms);
            foreach (var tm in tms)
            {
                tmRepo.Delete(tm);
            }
            companyRepo.Delete(company);
        }
    }
}
