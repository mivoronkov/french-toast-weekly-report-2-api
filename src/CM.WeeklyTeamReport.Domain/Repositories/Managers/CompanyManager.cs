using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Managers
{
    public class CompanyManager: ICompanyManager
    {
        private readonly ICompanyRepository _repository;
        public CompanyManager(ICompanyRepository companyRepository) {
            _repository = companyRepository;
        }

        public ICompany createCompany(ICompany newCompany)
        {
            return _repository.Create(newCompany);
        }
        public ICompany createCompany(string name, DateTime? creationDate)
        {
            var newCompany = new Company();
            newCompany.Name = name;
            newCompany.CreationDate = creationDate;
            return _repository.Create(newCompany);
        }
        public ICompany readCompany(int entityIdy)
        {
            return _repository.Read(entityIdy);
        }
        public ICollection<ICompany> readAllCompanies()
        {
            return _repository.ReadAll();
        }
        public void updateCompany(ICompany entity)
        {
            _repository.Update(entity);
        }
        public void deleteCompany(ICompany entity)
        {
            _repository.Delete(entity);
        }
        public void deleteCompany(int entityIdy)
        {
            _repository.Delete(entityIdy);
        }
    }
}
