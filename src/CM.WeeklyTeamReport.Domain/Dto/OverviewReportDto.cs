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
        public ICollection<int> MoraleGrade { get; set; } = new List<int>();
        public ICollection<int> StressGrade { get; set; } = new List<int>();
        public ICollection<int> WorkloadGrade { get; set; } = new List<int>();
        public OverviewReportDto() { }
    }
}
