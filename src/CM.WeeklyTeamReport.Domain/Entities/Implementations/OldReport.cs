using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Entities.Implementations
{
    public class OldReport: IOldReport
    {
        public DateTime Date { get; set; }
        public int StatusLevel { get; set; }
        public OldReport() { }
    }
}
