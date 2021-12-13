using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
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
                .Setup(x => x.readAllCompanies())
                .Returns(new List<ICompany>() {
                    new Company { Name = "Trevor Philips Industries" },
                    new Company { Name = "Aperture Science" }
                });
            var controller = fixture.GetCompaniesController();
            var companies = (ICollection<Company>)((OkObjectResult)controller.Get()).Value;

            companies.Should().NotBeNull();
            companies.Should().HaveCount(2);

            fixture
                .CompanyManager
                .Verify(x => x.readAllCompanies(), Times.Once);
        }

        [Fact]
        public void ShouldReturnSingleCompany()
        {
            var fixture = new CompaniesControllerFixture();
            fixture.CompanyManager
                .Setup(x => x.readCompany(76))
                .Returns(new Company { Name = "Vault Tec" });

            var controller = fixture.GetCompaniesController();
            var company = (Company)((OkObjectResult)controller.Get(76)).Value;

            company.Should().NotBeNull();

            fixture
                .CompanyManager
                .Verify(x => x.readCompany(76), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFound()
        {
            var fixture = new CompaniesControllerFixture();
            fixture.CompanyManager
                .Setup(x => x.readCompany(112))
                .Returns((Company)null);
            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Get(112);
            actionResult.Should().BeOfType<NotFoundObjectResult>();

            fixture
                .CompanyManager
                .Verify(x => x.readCompany(112), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-23)]
        public void ShouldReturnBadRequest(int requestId)
        {
            var fixture = new CompaniesControllerFixture();
            fixture.CompanyManager
                .Setup(x => x.readCompany(requestId))
                .Returns((Company)null);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Get(requestId);
            actionResult.Should().BeOfType<BadRequestResult>();

            fixture
                .CompanyManager
                .Verify(x => x.readCompany(requestId), Times.Never);
        }

        [Fact]
        public void ShouldCreateCompany()
        {
            var fixture = new CompaniesControllerFixture();
            var company = new Company()
            {
                Name = "New company",
                CreationDate = DateTime.Now
            };
            fixture.CompanyManager
                .Setup(x => x.createCompany(company))
                .Returns(new Company
                {
                    ID = 1,
                    Name = company.Name,
                    CreationDate = company.CreationDate
                });
            var controller = fixture.GetCompaniesController();
            var returnedCompany = (Company)((CreatedResult)controller.Post(company.Name, company.CreationDate)).Value;

            returnedCompany.Should().NotBeNull();
            returnedCompany.ID.Should().NotBe(0);

            fixture
                .CompanyManager
                .Verify(x => x.createCompany(company), Times.Once);
        }

        [Fact]
        public void ShouldReturnServerErrorIfCannotCreate()
        {
            var fixture = new CompaniesControllerFixture();
            var company = new Company()
            {
                Name = "New company",
                CreationDate = DateTime.Now
            };
            fixture.CompanyManager
                .Setup(x => x.createCompany(company))
                .Returns((Company)null);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Post(company.Name, company.CreationDate);
            actionResult.Should().BeOfType<StatusCodeResult>();
            ((StatusCodeResult)actionResult).StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

            fixture
                .CompanyManager
                .Verify(x => x.createCompany(company), Times.Once);
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
            fixture.CompanyManager
                .Setup(x => x.readCompany(company.ID))
                .Returns(company);
            fixture.CompanyManager
                .Setup(x => x.updateCompany(company));
            company.Name = "Name 2";
            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Put(company.Name, company.ID);
            actionResult.Should().BeOfType<OkObjectResult>();

            fixture
                .CompanyManager
                .Verify(x => x.readCompany(company.ID), Times.Once);
            fixture
                .CompanyManager
                .Verify(x => x.updateCompany(company), Times.Once);
        }

        [Fact]
        public void ShouldCreateOnUpdateIfNotFound()
        {
            var fixture = new CompaniesControllerFixture();
            var company = new Company()
            {
                ID = 100,
                Name = "New company",
                CreationDate = DateTime.Now
            };
            fixture.CompanyManager
                .Setup(x => x.readCompany(100))
                .Returns((Company)null);
            fixture.CompanyManager
                .Setup(x => x.createCompany(company))
                .Returns(company);
            fixture.CompanyManager
                .Setup(x => x.updateCompany(company));

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Put(company.Name, company.ID);
            actionResult.Should().BeOfType<CreatedResult>();
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
            fixture.CompanyManager
                .Setup(x => x.deleteCompany(company.ID));
            fixture.CompanyManager
                .Setup(x => x.readCompany(company.ID))
                .Returns(company);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Delete(company.ID);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .CompanyManager
                .Verify(x => x.deleteCompany(company.ID), Times.Once);
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
                .Setup(x => x.deleteCompany(company));
            fixture.CompanyManager
                .Setup(x => x.readCompany(company.ID))
                .Returns((Company)null);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Delete(company.ID);
            actionResult.Should().BeOfType<NotFoundResult>();

            fixture
                .CompanyManager
                .Verify(x => x.deleteCompany(company), Times.Never);
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
