using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface ICompanyManager
    {
        public ICompany createCompany(ICompanyDto companyDto);
        public ICompanyDto readCompany(int entityId);
        public ICollection<ICompanyDto> readAllCompanies();
        public void updateCompany(ICompanyDto oldEntity, ICompanyDto newEntity);
        public void deleteCompany(int entityId);
    }
}
