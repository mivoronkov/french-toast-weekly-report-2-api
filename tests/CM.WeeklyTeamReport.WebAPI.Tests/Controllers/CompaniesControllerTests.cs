using CM.WeeklyTeamReport.Domain;
using FluentAssertions;
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
            var companies = controller.Get();

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
