using System.Collections.Generic;
using System.Net.Mail;

namespace CM.WeeklyTeamReport.Domain
{
    public class TeamMember
    {
        public int ID { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public ICollection<TeamMember> LeadersToReport { get; set; }

        public ICollection<TeamMember> ReportingMembers { get; set; }

        public MailAddress Email { get; set; }

        // TODO: Email confirmation

        public string InviteLink {
            get {
                return $"https://weeklyreport.entreleadership.com/accept/{GetHashCode()}";
            }
        }

        public TeamMember(
            string firstName, 
            string lastName, 
            string title,
            MailAddress email)
        {
            FirstName = firstName;
            LastName = lastName;
            Title = title;
            Email = email;
        }

        public TeamMember(
            string firstName,
            string lastName,
            string title,
            string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Title = title;
            Email = new MailAddress(email);
        }
    }
}
