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
        public int[] StatusLevel { get; set; }
        public OverviewReportDto() { }
    }
}
