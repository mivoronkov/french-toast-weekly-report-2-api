using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Managers;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class CompanyManagerTests
    {
        [Fact]
        public void ShouldReadAllCompanies()
        {
            var fixture = new CompanyManagerFixture();
            var company1 = new Company { Name = "Trevor Philips Industries", ID = 1 };
            var company2 = new Company { Name = "Aperture Science", ID = 2 };
            var readedCompanies = new List<ICompany>() {company1,company2};
            var companyDto1 = new CompanyDto { Name = "Trevor Philips Industries", ID = 1 };
            var companyDto2 = new CompanyDto { Name = "Aperture Science", ID = 2 };

            fixture.CompanyRepository.Setup(x => x.ReadAll()).Returns(readedCompanies);
            fixture.CompanyCommands.Setup(x => x.companyToDto(company1)).Returns(companyDto1);
            fixture.CompanyCommands.Setup(x => x.companyToDto(company2)).Returns(companyDto2);
            var manager = fixture.GetCompanyManager();

            var companies = (List <CompanyDto>)manager.readAll();
            fixture.CompanyRepository.Verify(x => x.ReadAll(), Times.Once);
            fixture.CompanyCommands.Verify(x => x.companyToDto(company1), Times.Once);
            fixture.CompanyCommands.Verify(x => x.companyToDto(company2), Times.Once);

            companies.Should().HaveCount(2);
            companies[0].ID.Should().Be(1);
            companies[0].Name.Should().Be("Trevor Philips Industries");
            companies[1].ID.Should().Be(2);
            companies[1].Name.Should().Be("Aperture Science");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void ShouldReadCompanyByID(int id)
        {
            var fixture = new CompanyManagerFixture();
            var companyStub = new Company { Name = "Trevor Philips Industries", ID = id };
            var companyDtoStub = new CompanyDto { Name = "Trevor Philips Industries", ID = id };
            fixture.CompanyRepository.Setup(x => x.Read(id)).Returns(companyStub);
            fixture.CompanyCommands.Setup(x => x.companyToDto(companyStub)).Returns(companyDtoStub);
            var manager = fixture.GetCompanyManager();

            var company = manager.read(id);
            fixture.CompanyRepository.Verify(x => x.Read(id), Times.Once);
            fixture.CompanyCommands.Verify(x => x.companyToDto(companyStub), Times.Once);
            company.Should().NotBeNull();
            company.Should().BeOfType<CompanyDto>();
            company.ID.Should().Be(id);
            company.Name.Should().Be("Trevor Philips Industries");
        }

        [Fact]
        public void ShouldReturnNull()
        {
            var fixture = new CompanyManagerFixture();
            fixture.CompanyRepository.Setup(x => x.Read(1)).Returns((Company)null);
            var manager = fixture.GetCompanyManager();

            var company = manager.read(1);
            company.Should().BeNull();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void ShouldDeleteCompanyByID(int id)
        {
            var fixture = new CompanyManagerFixture();
            var company = new Company { Name = "Trevor Philips Industries", ID = id };
            fixture.CompanyRepository.Setup(x => x.Delete(id));
            var manager = fixture.GetCompanyManager();

            manager.delete(id);
            fixture.CompanyRepository.Verify(x => x.Delete(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void ShouldCreateCompany(int id)
        {
            var fixture = new CompanyManagerFixture();
            var companyDto = new CompanyDto { Name = "Trevor Philips Industries", ID = id };
            var company = new Company { Name = "Trevor Philips Industries", ID = id };
            var newCompany = new Company { Name = "Trevor Philips Industries", ID = id };

            fixture.CompanyCommands.Setup(x => x.dtoToCompany(companyDto)).Returns(company);
            fixture.CompanyRepository.Setup(x => x.Create(company)).Returns(newCompany);

            var manager = fixture.GetCompanyManager();
            var createdCompany = manager.create(companyDto);
            fixture.CompanyRepository.Verify(x => x.Create(company), Times.Once);
            fixture.CompanyCommands.Verify(x => x.dtoToCompany(companyDto), Times.Once);
            createdCompany.ID.Should().Be(companyDto.ID);
            createdCompany.Name.Should().Be(companyDto.Name);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void ShouldUpdateCompany(int id)
        {
            var fixture = new CompanyManagerFixture();
            var oldCompanyDto = new CompanyDto { Name = "Trevor Philips Industries", ID = id };
            var newCompanyDto = new CompanyDto { Name = "Trevor Philips Industries", ID = 0 };
            var newCompany = new Company { Name = "Trevor Philips Industries", ID = id };

            fixture.CompanyCommands.Setup(x => x.dtoToCompany(newCompanyDto)).Returns(newCompany);
            fixture.CompanyRepository.Setup(x => x.Update(newCompany));

            var manager = fixture.GetCompanyManager();
            manager.update(oldCompanyDto, newCompanyDto);
            fixture.CompanyCommands.Verify(x => x.dtoToCompany(newCompanyDto), Times.Once);
            fixture.CompanyRepository.Verify(x => x.Update(newCompany), Times.Once);
            newCompanyDto.ID.Should().Be(oldCompanyDto.ID);
            newCompanyDto.Name.Should().Be("Trevor Philips Industries");
        }


        public class CompanyManagerFixture
        {
            public CompanyManagerFixture()
            {
                CompanyRepository = new Mock<ICompanyRepository>();
                CompanyCommands = new Mock<ICompanyCommand>();
            }

            public Mock<ICompanyRepository> CompanyRepository { get; private set; }
            public Mock<ICompanyCommand> CompanyCommands { get; private set; }

            public CompanyManager GetCompanyManager()
            {
                return new CompanyManager(CompanyRepository.Object, CompanyCommands.Object);
            }
        }
    }
}
