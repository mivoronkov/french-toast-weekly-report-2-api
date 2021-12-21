using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Dto
{
    public class TeamMemberDto
    {
        public int? ID { get; set; }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        public string Email { get; set; }

        public string Sub { get; set; }

        public string CompanyName { get; set; }
        public int CompanyId { get; set; }

        public string InviteLink { get; set; }
    }
}
