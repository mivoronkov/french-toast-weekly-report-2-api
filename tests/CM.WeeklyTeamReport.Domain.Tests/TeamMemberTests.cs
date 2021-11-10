using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class TeamMemberTests
    {
        [Fact]
        public void ShouldCreateTeamMember()
        {
            var tm = new TeamMember("FirstName", "LastName", "Title");
            Assert.Equal("FirstName", tm.FirstName);
            Assert.Equal("LastName", tm.LastName);
            Assert.Equal("Title", tm.Title);
        }

        [Fact]
        public void ShouldBeAbleToChangeFirstName()
        {
            var tm = new TeamMember("FirstName", "LastName", "Title");
            tm.FirstName = "NewFirstName";
            Assert.Equal("NewFirstName", tm.FirstName);
        }

        [Fact]
        public void ShouldBeAbleToChangeLastName()
        {
            var tm = new TeamMember("FirstName", "LastName", "Title");
            tm.LastName = "NewLastName";
            Assert.Equal("NewLastName", tm.LastName);
        }

        [Fact]
        public void ShouldBeAbleToChangeTitle()
        {
            var tm = new TeamMember("FirstName", "LastName", "Title");
            tm.Title = "NewTitle";
            Assert.Equal("NewTitle", tm.Title);
        }

        [Fact]
        public void ShouldHaveLeadersToReport()
        {
            var tm = new TeamMember("FirstName", "LastName", "Title");

            var l1 = new TeamMember("1", "1", "1");
            var l2 = new TeamMember("2", "2", "2");
            var l3 = new TeamMember("3", "3", "3");

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
            var tm = new TeamMember("FirstName", "LastName", "Title");

            var m1 = new TeamMember("1", "1", "1");
            var m2 = new TeamMember("2", "2", "2");
            var m3 = new TeamMember("3", "3", "3");

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
            var tm = new TeamMember("FirstName", "LastName", "Title");

            // Invite link is a link
            Assert.True(Uri.IsWellFormedUriString(tm.InviteLink, UriKind.Absolute));

            // Invite link hasn't set method
            Assert.Null(typeof(TeamMember).GetProperty(nameof(TeamMember.InviteLink)).GetSetMethod());

            // Invite link is unique for each team member
            var tm2 = new TeamMember("FirstName", "LastName", "Title");
            Assert.NotEqual(tm2.InviteLink, tm.InviteLink);

            // TODO: check if server responds to invite urls ASAP
        }
    }
}
