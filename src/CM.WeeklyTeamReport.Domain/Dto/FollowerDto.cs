using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Dto
{
    public class FollowerDto
    {
        public FollowerDto() { }
        public int ID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
