using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Entities.Implementations
{
    public class WeeklyInformation : IWeeklyInformation
    {
        public string StateName { get;set; }
        public int StateLevel { get; set; }
        public string Comments { get; set; }
    }
}
