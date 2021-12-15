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
        public ICompany create(CompanyDto companyDto);
        public CompanyDto read(int entityId);
        public ICollection<CompanyDto> readAll();
        public void update(CompanyDto oldEntity, CompanyDto newEntity);
        public void delete(int entityId);
    }
}
