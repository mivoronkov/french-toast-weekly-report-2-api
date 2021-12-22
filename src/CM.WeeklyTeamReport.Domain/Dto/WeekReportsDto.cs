using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Dto
{
    public class WeekReportsDto
    {
        public WeekReportsDto() { }
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AuthorId { get; set; }
        public int MoraleLevel { get; set; }
        public int StressLevel { get; set; }
        public int WorkloadLevel { get; set; }
        public DateTime Date { get; set; }
    }
}
