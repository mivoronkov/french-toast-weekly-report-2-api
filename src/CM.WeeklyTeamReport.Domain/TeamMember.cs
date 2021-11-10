using System.Collections.Generic;


namespace CM.WeeklyTeamReport.Domain
{
    public class TeamMember
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public ICollection<TeamMember> LeadersToReport { get; set; }

        public ICollection<TeamMember> ReportingMembers { get; set; }

        public string InviteLink { get {
                return $"https://weeklyreport.entreleadership.com/accept/{GetHashCode()}";
            } }

        public TeamMember(string firstName, string lastName, string title)
        {
            FirstName = firstName;
            LastName = lastName;
            Title = title;
        }
    }
}
