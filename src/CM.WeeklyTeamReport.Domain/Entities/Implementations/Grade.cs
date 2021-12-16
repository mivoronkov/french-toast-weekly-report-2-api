using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Entities.Implementations
{
    public class Grade : IGrade
    {
        public Level Level { get; set; }
        public string Commentary { get; set; }
        public int ID { get; set; }
        public Grade() { }
    }
}
