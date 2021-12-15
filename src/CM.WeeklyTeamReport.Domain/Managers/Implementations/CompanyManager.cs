using CM.WeeklyTeamReport.Domain.Commands;
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
        private readonly ICompanyCommand _companyCommand;

        public CompanyManager(ICompanyRepository companyRepository, ICompanyCommand companyCommand) {
            _repository = companyRepository;
            _companyCommand = companyCommand;
        }

        public ICompany create(CompanyDto companyDto)
        {
            var newCompany = _companyCommand.dtoToCompany(companyDto);
            newCompany.CreationDate = DateTime.Today;
            return _repository.Create(newCompany);
        }
        public CompanyDto read(int entityId)
        {
            var company = _repository.Read(entityId);
            var companyDto = company!=null ? _companyCommand.companyToDto(company) : null;
            return companyDto;
        }
        public ICollection<CompanyDto> readAll()
        {
            var companies = _repository.ReadAll();
            var companiesDto = companies.Select(el => _companyCommand.companyToDto(el)).ToList();
            return companiesDto;
        }
        public void update(CompanyDto oldEntity, CompanyDto newEntity)
        {
            newEntity.ID = oldEntity.ID;
            var company = _companyCommand.dtoToCompany(newEntity);
            _repository.Update(company);
        }
        public void delete(int entityId)
        {
            _repository.Delete(entityId);
        }
    }
}
