using System;
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
                return $"https://weeklyreport.entreleadership.com/accept/{HashCode.Combine(ID)}";
            }
        }

        public TeamMember(
            int id,
            string firstName,
            string lastName,
            string title,
            MailAddress email)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Title = title;
            Email = email;
            LeadersToReport = new List<TeamMember>();
            ReportingMembers = new List<TeamMember>();
        }

        public TeamMember(
            string firstName,
            string lastName,
            string title,
            MailAddress email) : this(0, firstName, lastName, title, email)
        {
        }

        public TeamMember(
            int id,
            string firstName,
            string lastName,
            string title,
            string email)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Title = title;
            Email = new MailAddress(email);
            LeadersToReport = new List<TeamMember>();
            ReportingMembers = new List<TeamMember>();
        }

        public TeamMember(
            string firstName,
            string lastName,
            string title,
            string email) : this(0, firstName, lastName, title, email)
        { 
        }
    }
}
