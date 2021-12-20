using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class MemberCommandsTests
    {
        public MemberCommands commands = new MemberCommands();
        [Fact]
        public void ShouldCreateMemberFromDto()
        {
            var fixture = new TeamMemberTestsFixture();
            var tmDto = fixture.GetTeamMemberDto();
            
            var tm = commands.dtoToTeamMember(tmDto);

            Assert.Equal(tm.FirstName, tmDto.FirstName);
            Assert.Equal(tm.LastName, tmDto.LastName);
            Assert.Equal(tm.Title, tmDto.Title);
            Assert.Equal(tm.Email, tmDto.Email);
            Assert.Equal(tm.Sub, tmDto.Sub);
            Assert.Equal(tm.CompanyId, tmDto.CompanyId);
        }
        [Fact]
        public void ShouldCreateDtoFromMember()
        {
            var fixture = new TeamMemberTestsFixture();
            var tm = fixture.GetTeamMember();
            var tmDto = commands.teamMemberToDto(tm, "Sony");

            Assert.Equal("Sony", tmDto.CompanyName);
            Assert.Equal(tm.FirstName, tmDto.FirstName);
            Assert.Equal(tm.LastName, tmDto.LastName);
            Assert.Equal(tm.Title, tmDto.Title);
            Assert.Equal(tm.Email, tmDto.Email);
            Assert.Equal(tm.Sub, tmDto.Sub);
            Assert.Equal(tm.CompanyId, tmDto.CompanyId);
        }
        public class TeamMemberTestsFixture
        {
            public readonly int ExpectedId = 1;
            public readonly string ExpectedFirstName = "FirstName";
            public readonly string ExpectedLastName = "LastName";
            public readonly string ExpectedTitle = "Title";
            public readonly string ExpectedEmail = "mail@example.com";
            public readonly string ExpectedSub = "auth0|1";
            public readonly string ExpectedInviteLink = "https://weeklyreport.entreleadership.com/accept/dfgdfg84";
            public readonly string ExpectedCompanyName = "Sony";

            public TeamMemberDto GetTeamMemberDto()
            {
                return new TeamMemberDto
                {
                    ID = ExpectedId,
                    FirstName = ExpectedFirstName,
                    LastName = ExpectedLastName,
                    Title = ExpectedTitle,
                    Email = ExpectedEmail,
                    Sub = ExpectedSub,
                    InviteLink = ExpectedInviteLink,
                    CompanyName = ExpectedCompanyName,
                    CompanyId = 1
                };
            }
            public TeamMember GetTeamMember()
            {
                return new TeamMember
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
