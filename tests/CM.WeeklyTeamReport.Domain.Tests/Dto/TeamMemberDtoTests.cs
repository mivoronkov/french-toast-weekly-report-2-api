using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class TeamMemberDtoTests
    {
        [Fact]
        public void ShouldCreateTeamMemberDto()
        {
            var fixture = new TeamMemberTestsFixture();
            var tm = fixture.GetTeamMemberDto();
            Assert.Equal(fixture.ExpectedFirstName, tm.FirstName);
            Assert.Equal(fixture.ExpectedLastName, tm.LastName);
            Assert.Equal(fixture.ExpectedTitle, tm.Title);
            Assert.Equal(fixture.ExpectedEmail, tm.Email);
            Assert.Equal(fixture.ExpectedSub, tm.Sub);
            Assert.Equal(1, tm.CompanyId);
        }
        public class TeamMemberTestsFixture
        {
            public readonly string ExpectedFirstName = "FirstName";
            public readonly string ExpectedLastName = "LastName";
            public readonly string ExpectedTitle = "Title";
            public readonly string ExpectedEmail = "mail@example.com";
            public readonly string ExpectedSub = "auth0|1";
            public readonly string ExpectedInviteLink = "https://weeklyreport.entreleadership.com/accept/dfgdfg84";

            public TeamMemberDto GetTeamMemberDto()
            {
                return new TeamMemberDto
                {
                    FirstName = ExpectedFirstName,
                    LastName = ExpectedLastName,
                    Title = ExpectedTitle,
                    Email = ExpectedEmail,
                    Sub = ExpectedSub,
                    InviteLink = ExpectedInviteLink,
                    CompanyId = 1
                };
            }
        }
    }
}
