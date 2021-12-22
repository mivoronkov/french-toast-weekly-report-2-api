using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Dto
{
    public class IntListDto
    {
        public IntListDto() { }
        public List<int> Leaders { get; set; }
        public List<int> Followers { get; set; }
    }
}
