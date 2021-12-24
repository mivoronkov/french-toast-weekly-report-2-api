using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Entities.Implementations
{
    public class WeeklyNotations : IWeeklyNotations
    {
        public string Text { get; set ; }
        public string Title { get; set; }
    }
}
