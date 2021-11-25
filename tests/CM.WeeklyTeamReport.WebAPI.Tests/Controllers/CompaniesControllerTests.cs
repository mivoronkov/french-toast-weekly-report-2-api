using CM.WeeklyTeamReport.Domain;
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
            fixture.CompanyRepository
                .Setup(x => x.ReadAll())
                .Returns(new List<Company>() {
                    new Company { Name = "Trevor Philips Industries" },
                    new Company { Name = "Aperture Science" }
                });
            var controller = fixture.GetCompaniesController();
            var companies = (ICollection<Company>)((OkObjectResult)controller.Get().Result).Value;

            companies.Should().NotBeNull();
            companies.Should().HaveCount(2);

            fixture
                .CompanyRepository
                .Verify(x => x.ReadAll(), Times.Once);
        }

        [Fact]
        public void ShouldReturnSingleCompany()
        {
            var fixture = new CompaniesControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Read(76))
                .Returns(new Company { Name = "Vault Tec" });

            var controller = fixture.GetCompaniesController();
            var company = (Company)((OkObjectResult)controller.GetSingle(76).Result).Value;

            company.Should().NotBeNull();

            fixture
                .CompanyRepository
                .Verify(x => x.Read(76), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFound()
        {
            var fixture = new CompaniesControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Read(112))
                .Returns((Company)null);
            var controller = fixture.GetCompaniesController();
            var actionResult = controller.GetSingle(112).Result;
            actionResult.Should().BeOfType<NotFoundObjectResult>();

            fixture
                .CompanyRepository
                .Verify(x => x.Read(112), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-23)]
        public void ShouldReturnBadRequest(int requestId)
        {
            var fixture = new CompaniesControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Read(requestId))
                .Returns((Company)null);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.GetSingle(requestId).Result;
            actionResult.Should().BeOfType<BadRequestResult>();

            fixture
                .CompanyRepository
                .Verify(x => x.Read(requestId), Times.Never);
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
            fixture.CompanyRepository
                .Setup(x => x.Create(company))
                .Returns(new Company
                {
                    ID = 1,
                    Name = company.Name,
                    CreationDate = company.CreationDate
                });
            var controller = fixture.GetCompaniesController();
            var returnedCompany = (Company)((CreatedResult)controller.Create(company).Result).Value;

            returnedCompany.Should().NotBeNull();
            returnedCompany.ID.Should().NotBe(0);

            fixture
                .CompanyRepository
                .Verify(x => x.Create(company), Times.Once);
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
            fixture.CompanyRepository
                .Setup(x => x.Create(company))
                .Returns((Company)null);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Create(company).Result;
            actionResult.Should().BeOfType<StatusCodeResult>();
            ((StatusCodeResult)actionResult).StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

            fixture
                .CompanyRepository
                .Verify(x => x.Create(company), Times.Once);
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
            fixture.CompanyRepository
                .Setup(x => x.Read(company.ID))
                .Returns(company);
            fixture.CompanyRepository
                .Setup(x => x.Update(company));
            company.Name = "Name 2";
            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Put(company.ID, company).Result;
            actionResult.Should().BeOfType<OkObjectResult>();

            fixture
                .CompanyRepository
                .Verify(x => x.Read(company.ID), Times.Once);
            fixture
                .CompanyRepository
                .Verify(x => x.Update(company), Times.Once);
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
            fixture.CompanyRepository
                .Setup(x => x.Read(100))
                .Returns((Company)null);
            fixture.CompanyRepository
                .Setup(x => x.Create(company))
                .Returns(company);
            fixture.CompanyRepository
                .Setup(x => x.Update(company));

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Put(company.ID, company).Result;
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
            fixture.CompanyRepository
                .Setup(x => x.Delete(company.ID));
            fixture.CompanyRepository
                .Setup(x => x.Read(company.ID))
                .Returns(company);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Delete(company.ID);
            actionResult.Should().BeOfType<NoContentResult>();

            fixture
                .CompanyRepository
                .Verify(x => x.Delete(company.ID), Times.Once);
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
            fixture.CompanyRepository
                .Setup(x => x.Delete(company));
            fixture.CompanyRepository
                .Setup(x => x.Read(company.ID))
                .Returns((Company)null);

            var controller = fixture.GetCompaniesController();
            var actionResult = controller.Delete(company.ID);
            actionResult.Should().BeOfType<NotFoundResult>();

            fixture
                .CompanyRepository
                .Verify(x => x.Delete(company), Times.Never);
        }

        public class CompaniesControllerFixture
        {
            public CompaniesControllerFixture()
            {
                CompanyRepository = new Mock<IRepository<Company>>();
            }

            public Mock<IRepository<Company>> CompanyRepository { get; private set; }

            public CompaniesController GetCompaniesController()
            {
                return new CompaniesController(CompanyRepository.Object);
            }
        }
    }
}
