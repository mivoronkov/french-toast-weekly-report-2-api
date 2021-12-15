using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class CompanyDtoTests
    {
        [Fact]
        public void ShouldCreateCompanyDto()
        {
            string companyName = "CompanyName";
            var creationDate = DateTime.Now;
            var companyDto = new CompanyDto
            {
                ID = 1,
                Name = companyName,
                CreationDate = creationDate
            };
            Assert.Equal(companyName, companyDto.Name);
            Assert.Equal(creationDate, companyDto.CreationDate);
            Assert.Equal(1, companyDto.ID);
        }        
    }
}
