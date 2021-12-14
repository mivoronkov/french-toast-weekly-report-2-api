using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Entities.Interfaces
{
    public interface ITeamMember
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public int CompanyId { get; set; }

        public string InviteLink { get; set; }
    }
}
