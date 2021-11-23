using CM.WeeklyTeamReport.Domain;
using FluentAssertions;
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
