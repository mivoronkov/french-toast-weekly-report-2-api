using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Dto
{
    public class OverviewReportDto
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int[] MoraleLevel { get; set; } = new int[10];
        public int[] StressLevel { get; set; } = new int[10];
        public int[] WorkloadLevel { get; set; } = new int[10];
        public OverviewReportDto() { }
    }
}
