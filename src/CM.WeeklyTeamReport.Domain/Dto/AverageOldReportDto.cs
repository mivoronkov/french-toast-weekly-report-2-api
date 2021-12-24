using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Dto
{
    public class AverageOldReportDto
    {
        public int[] StatusLevel { get; set; }
        public string FilterName { get; set; }
        public AverageOldReportDto() { }
    }
}
