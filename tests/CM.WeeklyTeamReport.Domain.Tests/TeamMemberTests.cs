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
            var tm = new TeamMember("FirstName", "LastName", "Title", email);
            Assert.Equal("FirstName", tm.FirstName);
            Assert.Equal("LastName", tm.LastName);
            Assert.Equal("Title", tm.Title);
            Assert.Equal("mail@example.com", tm.Email.Address);
        }

        [Fact]
        public void ShouldBeAbleToChangeFirstName()
        {
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember("FirstName", "LastName", "Title", email);
            tm.FirstName = "NewFirstName";
            Assert.Equal("NewFirstName", tm.FirstName);
        }

        [Fact]
        public void ShouldBeAbleToChangeLastName()
        {
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember("FirstName", "LastName", "Title", email);
            tm.LastName = "NewLastName";
            Assert.Equal("NewLastName", tm.LastName);
        }

        [Fact]
        public void ShouldBeAbleToChangeTitle()
        {
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember("FirstName", "LastName", "Title", email);
            tm.Title = "NewTitle";
            Assert.Equal("NewTitle", tm.Title);
        }

        [Fact]
        public void ShouldBeAbleToChangeEmail()
        {
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember("FirstName", "LastName", "Title", email);
            var newEmail = new MailAddress("new_mail@example.com");
            tm.Email = newEmail;
            Assert.Equal(newEmail, tm.Email);
        }

        [Fact]
        public void ShouldNotAcceptInvalidEmail()
        {
            Assert.Throws<FormatException>(() => { new TeamMember("1", "1", "1", "Invalid email format"); });
        }

        [Fact]
        public void ShouldHaveLeadersToReport()
        {
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember("FirstName", "LastName", "Title", email);

            var l1 = new TeamMember("1", "1", "1", email);
            var l2 = new TeamMember("2", "2", "2", email);
            var l3 = new TeamMember("3", "3", "3", email);

            // Can assign
            tm.LeadersToReport = new List<TeamMember>() { l1, l2 };
            Assert.Equal(2, tm.LeadersToReport.Count);
            Assert.Contains(l1, tm.LeadersToReport);
            Assert.Contains(l2, tm.LeadersToReport);

            // Can add
            tm.LeadersToReport.Add(l3);
            Assert.Equal(3, tm.LeadersToReport.Count);
            Assert.Contains(l1, tm.LeadersToReport);
            Assert.Contains(l2, tm.LeadersToReport);
            Assert.Contains(l3, tm.LeadersToReport);

            // Can remove
            tm.LeadersToReport.Remove(l1);
            Assert.Equal(2, tm.LeadersToReport.Count);
            Assert.Contains(l2, tm.LeadersToReport);
            Assert.Contains(l3, tm.LeadersToReport);
        }

        [Fact]
        public void ShouldHaveReportingMembers()
        {
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember("FirstName", "LastName", "Title", email);

            var m1 = new TeamMember("1", "1", "1", email);
            var m2 = new TeamMember("2", "2", "2", email);
            var m3 = new TeamMember("3", "3", "3", email);

            // Can assign
            tm.ReportingMembers = new HashSet<TeamMember>() { m1, m2 };
            Assert.Equal(2, tm.ReportingMembers.Count);
            Assert.Contains(m1, tm.ReportingMembers);
            Assert.Contains(m2, tm.ReportingMembers);

            // Can add
            tm.ReportingMembers.Add(m3);
            Assert.Equal(3, tm.ReportingMembers.Count);
            Assert.Contains(m1, tm.ReportingMembers);
            Assert.Contains(m2, tm.ReportingMembers);
            Assert.Contains(m3, tm.ReportingMembers);

            // Can remove
            tm.ReportingMembers.Remove(m1);
            Assert.Equal(2, tm.ReportingMembers.Count);
            Assert.Contains(m2, tm.ReportingMembers);
            Assert.Contains(m3, tm.ReportingMembers);
        }

        [Fact]
        public void ShouldGenerateInviteLink()
        {
            var email = new MailAddress("mail@example.com");
            var tm = new TeamMember("FirstName", "LastName", "Title", email);

            // Invite link is a link
            Assert.True(Uri.IsWellFormedUriString(tm.InviteLink, UriKind.Absolute));

            // Invite link hasn't set method
            Assert.Null(typeof(TeamMember).GetProperty(nameof(TeamMember.InviteLink)).GetSetMethod());

            // Invite link is unique for each ID
            var tm2 = new TeamMember(1, "FirstName", "LastName", "Title", email);
            Assert.NotEqual(tm2.InviteLink, tm.InviteLink);

            // TODO: check if server responds to invite urls ASAP
        }
    }
}
