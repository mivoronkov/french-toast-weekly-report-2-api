using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Commands
{
    public interface ICompanyCommand
    {
        public CompanyDto companyToDto(ICompany company);

        public Company dtoToCompany(CompanyDto companyDto);
        
    }
}
