using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class CompanyCommandTests
    {
        public CompanyCommand commands = new CompanyCommand();
        [Fact]
        public void ShouldCreateCompanyFromDto()
        {
            string companyName = "CompanyName";
            var creationDate = DateTime.Now;
            var companyDto = new CompanyDto
            {
                ID = 1,
                Name = companyName,
                CreationDate = creationDate
            };
            var company = commands.dtoToCompany(companyDto);
            Assert.Equal(company.Name, companyDto.Name);
            Assert.Equal(company.CreationDate, companyDto.CreationDate);
            Assert.Equal(companyDto.ID, company.ID);
        }
        [Fact]
        public void ShouldCreateDtoFromCompany()
        {
            string companyName = "CompanyName";
            var creationDate = DateTime.Now;
            var company = new Company
            {
                ID = 1,
                Name = companyName,
                CreationDate = creationDate
            };
            var companyDto = commands.companyToDto(company);
            Assert.Equal(company.Name, companyDto.Name);
            Assert.Equal(company.CreationDate, companyDto.CreationDate);
            Assert.Equal(companyDto.ID, company.ID);
        }
    }
}
