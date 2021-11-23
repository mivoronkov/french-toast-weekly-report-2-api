using System;
using System.Net.Mail;

namespace CM.WeeklyTeamReport.Domain
{
    public class TeamMember : IEntity
    {
        public override int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public MailAddress Email { get; set; }

        public int CompanyId { get; set; }

        public string InviteLink {
            get {
                return $"https://weeklyreport.entreleadership.com/accept/{HashCode.Combine(ID)}";
            }
        }

        public TeamMember()
        { 
        }
    }
}
