using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Dto.Implementations
{
    public class ReportsDto
    {
        public int ID { get; set; }

        public int AuthorId { get; set; }

        public int MoraleGradeId { get; set; }
        public Grade MoraleGrade { get; set; }

        public int StressGradeId { get; set; }
        public Grade StressGrade { get; set; }

        public int WorkloadGradeId { get; set; }
        public Grade WorkloadGrade { get; set; }
        public string HighThisWeek { get; set; }
        public string LowThisWeek { get; set; }
        public string AnythingElse { get; set; }
        public DateTime Date { get; set; }

        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
    }
}
