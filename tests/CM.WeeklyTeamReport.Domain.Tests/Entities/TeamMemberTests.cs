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
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember { 
                FirstName = "FirstName", 
                LastName = "LastName", 
                Title = "Title", 
                Email = email 
            };
            Assert.Equal("FirstName", tm.FirstName);
            Assert.Equal("LastName", tm.LastName);
            Assert.Equal("Title", tm.Title);
            Assert.Equal("mail@example.com", tm.Email.Address);
        }

        [Fact]
        public void ShouldBeAbleToChangeFirstName()
        {
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Title = "Title",
                Email = email
            };
            tm.FirstName = "NewFirstName";
            Assert.Equal("NewFirstName", tm.FirstName);
        }

        [Fact]
        public void ShouldBeAbleToChangeLastName()
        {
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Title = "Title",
                Email = email
            };
            tm.LastName = "NewLastName";
            Assert.Equal("NewLastName", tm.LastName);
        }

        [Fact]
        public void ShouldBeAbleToChangeTitle()
        {
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Title = "Title",
                Email = email
            };
            tm.Title = "NewTitle";
            Assert.Equal("NewTitle", tm.Title);
        }

        [Fact]
        public void ShouldBeAbleToChangeEmail()
        {
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Title = "Title",
                Email = email
            };
            var newEmail = new MailAddress("new_mail@example.com");
            tm.Email = newEmail;
            Assert.Equal(newEmail, tm.Email);
        }


        [Fact]
        public void ShouldGenerateInviteLink()
        {
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember
            {
                ID = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                Title = "Title",
                Email = email
            };

            // Invite link is a link
            Assert.True(Uri.IsWellFormedUriString(tm.InviteLink, UriKind.Absolute));

            // Invite link hasn't set method
            Assert.Null(typeof(TeamMember)?.GetProperty(nameof(TeamMember.InviteLink))?.GetSetMethod());

            // Invite link is unique for each ID
            var tm2 = new TeamMember
            {
                ID = 2,
                FirstName = "FirstName",
                LastName = "LastName",
                Title = "Title",
                Email = email
            };
            Assert.NotEqual(tm2.InviteLink, tm.InviteLink);

            // TODO: check if server responds to invite urls ASAP
        }
    }
}
