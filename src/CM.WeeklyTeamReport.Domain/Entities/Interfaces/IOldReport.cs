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
        public int StatusLevel { get; set; } 
    }
}
