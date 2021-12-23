using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Entities.Interfaces
{
    public interface IOldReport
    {
        public DateTime Date { get; set; }
        public int? MoraleLevel { get; set; } 
        public int? StressLevel { get; set; }
        public int? WorkloadLevel { get; set; }
        public int? Overall { get; set; }
    }
}
