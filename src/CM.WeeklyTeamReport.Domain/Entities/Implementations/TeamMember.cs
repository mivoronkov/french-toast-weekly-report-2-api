using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Net.Mail;

namespace CM.WeeklyTeamReport.Domain
{
    public class TeamMember : ITeamMember
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public int CompanyId { get; set; }

        public string InviteLink { get; set; }
        public TeamMember(){}
    }
}
