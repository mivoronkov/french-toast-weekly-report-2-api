using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CM.WeeklyTeamReport.WebAPI.Controllers.Tests
{
    public class CompaniesControllerTests
    {
        [Fact]
        public void ShouldReturnAllCompanies()
        {
            var fixture = new CompaniesControllerFixture();
            fixture.CompanyManager
                .Setup(x => x.readAll())
                .Returns(new List<CompanyDto>() {
                    new CompanyDto { Name = "Trevor Philips Industries" },
                    new CompanyDto { Name = "Aperture Science" }
                });
            var controller = fixture.GetCompaniesController();
            var companies = (ICollection<CompanyDto>)((OkObjectResult)controller.Get()).Value;

            companies.Should().NotBeNull();
            companies.Should().HaveCount(2);

            fixture
                .CompanyManager
                .Verify(x => x.readAll(), Times.Once);
        }
        [Fact]
        public void ShouldReturnNotFoundOnReadAll()
        {
            var fixture = new CompaniesControllerFixture();
            fixture.CompanyManager
                .Setup(x => x.readAll())
                .Returns((List<CompanyDto>)null);
            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Get();
            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void ShouldReturnSingleCompany()
        {
            var fixture = new CompaniesControllerFixture();
            fixture.CompanyManager
                .Setup(x => x.read(76))
                .Returns(new CompanyDto { Name = "Vault Tec" });

            var controller = fixture.GetCompaniesController();
            var company = (CompanyDto)((OkObjectResult)controller.Get(76)).Value;

            company.Should().NotBeNull();

            fixture
                .CompanyManager
                .Verify(x => x.read(76), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFound()
        {
            var fixture = new CompaniesControllerFixture();
            fixture.CompanyManager
                .Setup(x => x.read(112))
                .Returns((CompanyDto)null);
            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Get(112);
            actionResult.Should().BeOfType<NotFoundResult>();

            fixture
                .CompanyManager
                .Verify(x => x.read(112), Times.Once);
        }


        [Fact]
        public void ShouldCreateCompany()
        {
            var fixture = new CompaniesControllerFixture();
            var company = new CompanyDto()
            {
                Name = "New company",
                CreationDate = DateTime.Now
            };
            fixture.CompanyManager
                .Setup(x => x.create(company))
                .Returns(new Company
                {
                    ID = 1,
                    Name = company.Name,
                    CreationDate = company.CreationDate
                });
            var controller = fixture.GetCompaniesController();
            var returnedCompany = (Company)((CreatedResult)controller.Post(company)).Value;

            returnedCompany.Should().NotBeNull();
            returnedCompany.ID.Should().NotBe(0);

            fixture
                .CompanyManager
                .Verify(x => x.create(company), Times.Once);
        }

        [Fact]
        public void ShouldReturnServerErrorIfCannotCreate()
        {
            var fixture = new CompaniesControllerFixture();
            var company = new CompanyDto()
            {
                Name = "New company",
                CreationDate = DateTime.Now
            };
            fixture.CompanyManager
                .Setup(x => x.create(company))
                .Returns((Company)null);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Post(company);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .CompanyManager
                .Verify(x => x.create(company), Times.Once);
        }

        [Fact]
        public void ShouldUpdateCompany()
        {
            var fixture = new CompaniesControllerFixture();
            var company = new Company()
            {
                ID = 100,
                Name = "New company",
                CreationDate = DateTime.Now
            };
            var companyDto = new CompanyDto()
            {
                ID = 100,
                Name = "New company",
                CreationDate = DateTime.Now
            };
            var companyDto2 = new CompanyDto()
            {
                ID = 100,
                Name = "New company",
                CreationDate = DateTime.Now
            };
            fixture.CompanyManager
                .Setup(x => x.read(company.ID))
                .Returns(companyDto);
            fixture.CompanyManager
                .Setup(x => x.update(companyDto, companyDto2));
            company.Name = "Name 2";
            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Put(companyDto, company.ID);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .CompanyManager
                .Verify(x => x.read(company.ID), Times.Once);
        }
        [Fact]
        public void ShouldReturnNotFoundOnUpdate()
        {
            var fixture = new CompaniesControllerFixture();
            var company = new Company()
            {
                ID = 123,
                Name = "New company",
                CreationDate = DateTime.Now
            };
            var companyDto = new CompanyDto()
            {
                ID = 100,
                Name = "New company",
                CreationDate = DateTime.Now
            };
            var companyDto2 = new CompanyDto()
            {
                ID = 100,
                Name = "New company",
                CreationDate = DateTime.Now
            };

            fixture.CompanyManager
                .Setup(x => x.update(companyDto, companyDto2));
            fixture.CompanyManager
                .Setup(x => x.read(company.ID))
                .Returns((CompanyDto)null);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Put(companyDto, company.ID);
            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void ShouldDeleteCompany()
        {
            var fixture = new CompaniesControllerFixture();
            var company = new Company()
            {
                ID = 123,
                Name = "New company",
                CreationDate = DateTime.Now
            };
            var companyDto = new CompanyDto()
            {
                ID = 100,
                Name = "New company",
                CreationDate = DateTime.Now
            };
            fixture.CompanyManager
                .Setup(x => x.delete(company.ID));
            fixture.CompanyManager
                .Setup(x => x.read(company.ID))
                .Returns(companyDto);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Delete(company.ID);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .CompanyManager
                .Verify(x => x.delete(company.ID), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnDelete()
        {
            var fixture = new CompaniesControllerFixture();
            var company = new Company()
            {
                ID = 123,
                Name = "New company",
                CreationDate = DateTime.Now
            };
            fixture.CompanyManager
                .Setup(x => x.delete(company.ID));
            fixture.CompanyManager
                .Setup(x => x.read(company.ID))
                .Returns((CompanyDto)null);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Delete(company.ID);
            actionResult.Should().BeOfType<NotFoundResult>();

            fixture
                .CompanyManager
                .Verify(x => x.delete(company.ID), Times.Never);
        }

        public class CompaniesControllerFixture
        {
            public CompaniesControllerFixture()
            {
                CompanyManager = new Mock<ICompanyManager>();
            }

            public Mock<ICompanyManager> CompanyManager { get; private set; }

            public CompaniesController GetCompaniesController()
            {
                return new CompaniesController(CompanyManager.Object);
            }
        }
    }
}
