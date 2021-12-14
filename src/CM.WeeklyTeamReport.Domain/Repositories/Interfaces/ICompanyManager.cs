using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface ICompanyManager
    {
        public ICompany createCompany(ICompany newCompany);
        public ICompany createCompany(string name, DateTime? creationDate);
        public ICompany readCompany(int entityIdy);
        public ICollection<ICompany> readAllCompanies();
        public void updateCompany(ICompany entity);
        public void deleteCompany(ICompany entity);
        public void deleteCompany(int entityIdy);
    }
}
