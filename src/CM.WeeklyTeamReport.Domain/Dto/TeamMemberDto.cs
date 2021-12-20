using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Dto
{
    public class TeamMemberDto
    {
        public int? ID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public string Sub { get; set; }

        public string CompanyName { get; set; }
        public int CompanyId { get; set; }

        public string InviteLink { get; set; }
    }
}
