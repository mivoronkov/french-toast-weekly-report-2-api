using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Entities.Interfaces
{
    public interface IGrade
    {
        public Level Level { get; set; }
        public string Commentary { get; set; }
        public int ID { get; set; }
    }

    public enum Level
    {
        VeryLow,
        Low,
        Average,
        High,
        VeryHigh
    }
}
