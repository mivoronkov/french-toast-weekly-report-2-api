using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Entities.Implementations
{
    public class IndividualOldReport: IIndividualOldReport
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }
        public int StatusLevel { get; set; }
        public IndividualOldReport() { }
    }
}
