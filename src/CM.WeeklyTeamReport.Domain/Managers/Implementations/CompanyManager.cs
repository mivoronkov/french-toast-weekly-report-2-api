using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
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

        public ICompany create(CompanyDto companyDto)
        {
            var newCompany = dtoToCompany(companyDto);
            newCompany.CreationDate = DateTime.Today;
            return _repository.Create(newCompany);
        }
        public CompanyDto read(int entityId)
        {
            var company = _repository.Read(entityId);
            var companyDto = company!=null ? companyToDto(company) : null;
            return companyDto;
        }
        public ICollection<CompanyDto> readAll()
        {
            var companies = _repository.ReadAll();
            var companiesDto = companies.Select(el => companyToDto(el)).ToList();
            return companiesDto;
        }
        public void update(CompanyDto entity, CompanyDto companyDto)
        {
            companyDto.ID = entity.ID;
            var company = dtoToCompany(companyDto);
            _repository.Update(company);
        }
        public void delete(int entityId)
        {
            _repository.Delete(entityId);
        }
        private CompanyDto companyToDto(ICompany company) 
        {
            var companyDto = new CompanyDto();
            companyDto.ID = company.ID;
            companyDto.Name = company.Name;
            companyDto.CreationDate = company.CreationDate;
            return companyDto;
        }
        private Company dtoToCompany(CompanyDto companyDto)
        {
            var company = new Company();
            company.ID = (int)companyDto.ID;
            company.Name = companyDto.Name;
            company.CreationDate = companyDto.CreationDate;
            return company;
        }
    }
}
