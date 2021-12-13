using System;
using System.Collections.Generic;
using System.Net.Mail;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class TeamMemberTests
    {
        [Fact]
        public void ShouldCreateTeamMember()
        {
            var fixture = new TeamMemberTestsFixture();
            var tm = fixture.GetTeamMember();
            Assert.Equal(fixture.ExpectedFirstName, tm.FirstName);
            Assert.Equal(fixture.ExpectedLastName, tm.LastName);
            Assert.Equal(fixture.ExpectedTitle, tm.Title);
            Assert.Equal(fixture.ExpectedEmail, tm.Email);
            Assert.Equal(1, tm.CompanyId);
        }

        [Fact]
        public void ShouldBeAbleToChangeFirstName()
        {
            var tm = new TeamMemberTestsFixture().GetTeamMember();
            tm.FirstName = "NewFirstName";
            Assert.Equal("NewFirstName", tm.FirstName);
        }

        [Fact]
        public void ShouldBeAbleToChangeLastName()
        {
            var tm = new TeamMemberTestsFixture().GetTeamMember();
            tm.LastName = "NewLastName";
            Assert.Equal("NewLastName", tm.LastName);
        }

        [Fact]
        public void ShouldBeAbleToChangeTitle()
        {
            var tm = new TeamMemberTestsFixture().GetTeamMember();
            tm.Title = "NewTitle";
            Assert.Equal("NewTitle", tm.Title);
        }

        [Fact]
        public void ShouldBeAbleToChangeEmail()
        {
            var tm = new TeamMemberTestsFixture().GetTeamMember();
            var newEmail = "new_mail@example.com";
            tm.Email = newEmail;
            Assert.Equal(newEmail, tm.Email);
        }


        [Fact]
        public void ShouldGenerateInviteLink()
        {
            var tm = new TeamMemberTestsFixture().GetTeamMember();

            // Invite link is a link
            Assert.True(Uri.IsWellFormedUriString(tm.InviteLink, UriKind.Absolute));

            // Invite link hasn't set method
            //Assert.Null(typeof(TeamMember)?.GetProperty(nameof(TeamMember.InviteLink))?.GetSetMethod());

            // Invite link is unique for each ID
            var tm2 = new TeamMember
            {
                ID = 2,
                FirstName = "FirstName",
                LastName = "LastName",
                Title = "Title",
                Email = "mail@example.com",
            };
            Assert.NotEqual(tm2.InviteLink, tm.InviteLink);

            // TODO: check if server responds to invite urls ASAP
        }

        public class TeamMemberTestsFixture
        {
            public readonly string ExpectedFirstName = "FirstName";
            public readonly string ExpectedLastName = "LastName";
            public readonly string ExpectedTitle = "Title";
            public readonly string ExpectedEmail = "mail@example.com";
            public readonly string ExpectedInviteLink = "https://weeklyreport.entreleadership.com/accept/dfgdfg84";

            public TeamMember GetTeamMember()
            {
                return new TeamMember
                {
                    FirstName = ExpectedFirstName,
                    LastName = ExpectedLastName,
                    Title = ExpectedTitle,
                    Email = ExpectedEmail,
                    InviteLink = ExpectedInviteLink,
                    CompanyId = 1
                };
            }
        }
    }
}
