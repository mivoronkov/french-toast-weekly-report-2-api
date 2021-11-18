using System.Collections.Generic;

namespace CM.WeeklyTeamReport.Domain
{
    public class Company
    {
        public string Name { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }

        public Company(string name)
        {
            Name = name;
        }
    }
}
