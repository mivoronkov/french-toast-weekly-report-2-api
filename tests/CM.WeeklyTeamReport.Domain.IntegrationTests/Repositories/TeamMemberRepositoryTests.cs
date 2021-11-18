using Xunit;
using FluentAssertions;

namespace CM.WeeklyTeamReport.Domain.IntegrationTests
{
    public class TeamMemberRepositoryTests
    {
        [Fact]
        public void ShouldPerformBasicCRUD()
        {
            var repository = new TeamMemberRepository();
            var teamMember = repository.Create(new TeamMember(
                "Anatoliy",
                "Kolodkin",
                "CEO",
                "akolodkin@ankocorp.com"
                ));
            teamMember.ID.Should().NotBe(0);
            var readTeamMember = repository.Read(teamMember.ID);
            readTeamMember.FirstName.Should().Be(teamMember.FirstName);
            readTeamMember.LastName.Should().Be(teamMember.LastName);
            readTeamMember.Title.Should().Be(teamMember.Title);
            readTeamMember.Email.Address.Should().Be(teamMember.Email.Address);
            var newMail = new System.Net.Mail.MailAddress("newmail@ankocorp.com");
            teamMember.Email = newMail;
            repository.Update(teamMember);
            readTeamMember = repository.Read(teamMember.ID);
            readTeamMember.Email.Should().Be(newMail);
            repository.Delete(teamMember);
            readTeamMember = repository.Read(teamMember.ID);
            readTeamMember.Should().BeNull();
        }
    }
}
