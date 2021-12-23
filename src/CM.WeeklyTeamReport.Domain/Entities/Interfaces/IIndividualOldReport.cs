using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Entities.Interfaces
{
    public interface IIndividualOldReport
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }
        public int MoraleLevel { get; set; }
        public int StressLevel { get; set; }
        public int WorkloadLevel { get; set; }
        public int Overall { get; set; }
    }
}
