using Xunit;
using FluentAssertions;
using System;
using System.Collections.Generic;

namespace CM.WeeklyTeamReport.Domain.IntegrationTests
{
    public class TeamMemberRepositoryTests
    {
        [Fact]
        public void ShouldPerformBasicCRUD()
        {
            var teamMemberRepo = new TeamMemberRepository();
            var companyRepo = new CompanyRepository();
            var company = companyRepo.Create(new Company { Name = "Test company" });
            var teamMember = teamMemberRepo.Create(new TeamMember
            {
                FirstName = "Anatoliy",
                LastName = "Kolodkin",
                Title = "CEO",
                Email = new System.Net.Mail.MailAddress("akolodkin@ankocorp.com"),
                CompanyId = company.ID
            });
            teamMember.ID.Should().NotBe(0);
            var readTeamMember = teamMemberRepo.Read(teamMember.ID);
            readTeamMember.FirstName.Should().Be(teamMember.FirstName);
            readTeamMember.LastName.Should().Be(teamMember.LastName);
            readTeamMember.Title.Should().Be(teamMember.Title);
            readTeamMember.Email.Address.Should().Be(teamMember.Email.Address);
            var newMail = new System.Net.Mail.MailAddress("newmail@ankocorp.com");
            teamMember.Email = newMail;
            teamMemberRepo.Update(teamMember);
            readTeamMember = teamMemberRepo.Read(teamMember.ID);
            readTeamMember.Email.Should().Be(newMail);
            teamMemberRepo.Delete(teamMember);
            readTeamMember = teamMemberRepo.Read(teamMember.ID);
            readTeamMember.Should().BeNull();
            companyRepo.Delete(company);
        }

        [Fact]
        public void ShouldHandleReportingMembers()
        {
            var companyRepo = new CompanyRepository();
            var company = companyRepo.Create(new Company { Name = "Test company" });
            var tmRepo = new TeamMemberRepository();
            var teamMember = tmRepo.Create(
                new TeamMember
                {
                    FirstName = "F",
                    LastName = "L",
                    Title = "T",
                    Email = new System.Net.Mail.MailAddress("mail@example.com"),
                    CompanyId = company.ID
                }
            );
            var reportingMembers = new List<TeamMember>()
            {
                tmRepo.Create(new TeamMember
                {
                    FirstName = "F2",
                    LastName = "L2",
                    Title = "T2",
                    Email = new System.Net.Mail.MailAddress("mail2@example.com"),
                    CompanyId = company.ID
                }),
                tmRepo.Create(new TeamMember
                {
                    FirstName = "F3",
                    LastName = "L3",
                    Title = "T3",
                    Email = new System.Net.Mail.MailAddress("mail3@example.com"),
                    CompanyId = company.ID
                })
            };
            var leadersToReport = new List<TeamMember>()
            {
                tmRepo.Create(new TeamMember
                {
                    FirstName = "F4",
                    LastName = "L4",
                    Title = "T4",
                    Email = new System.Net.Mail.MailAddress("mail4@example.com"),
                    CompanyId = company.ID
                }),
                tmRepo.Create(new TeamMember
                {
                    FirstName = "F5",
                    LastName = "L5",
                    Title = "T5",
                    Email = new System.Net.Mail.MailAddress("mail5@example.com"),
                    CompanyId = company.ID
                })
            };

            tmRepo.GetReportingMembers(teamMember).Should().BeEmpty();
            tmRepo.GetLeadersToReport(teamMember).Should().BeEmpty();

            tmRepo.AddReportingMember(reportingMembers[0], teamMember);
            tmRepo.AddReportingMember(reportingMembers[1], teamMember);
            tmRepo.AddReportingMember(teamMember, leadersToReport[0]);
            tmRepo.AddReportingMember(teamMember, leadersToReport[1]);

            tmRepo.GetReportingMembers(teamMember).Should().BeEquivalentTo(reportingMembers);
            tmRepo.GetLeadersToReport(teamMember).Should().BeEquivalentTo(leadersToReport);

            tmRepo.RemoveReportingMember(reportingMembers[0], teamMember);
            tmRepo.RemoveReportingMember(reportingMembers[1], teamMember);
            tmRepo.RemoveReportingMember(teamMember, leadersToReport[0]);
            tmRepo.RemoveReportingMember(teamMember, leadersToReport[1]);

            tmRepo.GetReportingMembers(teamMember).Should().BeEmpty();
            tmRepo.GetLeadersToReport(teamMember).Should().BeEmpty();

            foreach (var tm in reportingMembers)
                tmRepo.Delete(tm);
            foreach (var tm in leadersToReport)
                tmRepo.Delete(tm);
            tmRepo.Delete(teamMember);
            companyRepo.Delete(company);
        }
    }
}
