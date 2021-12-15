using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Commands
{
    public class CompanyCommand
    {
        public static CompanyDto companyToDto(ICompany company)
        {
            var companyDto = new CompanyDto();
            companyDto.ID = company.ID;
            companyDto.Name = company.Name;
            companyDto.CreationDate = company.CreationDate;
            return companyDto;
        }
        public static Company dtoToCompany(CompanyDto companyDto)
        {
            var company = new Company();
            company.ID = (int)companyDto.ID;
            company.Name = companyDto.Name;
            company.CreationDate = companyDto.CreationDate;
            return company;
        }
    }
}
