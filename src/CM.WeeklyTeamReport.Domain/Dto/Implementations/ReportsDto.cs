using CM.WeeklyTeamReport.Domain.Dto.Interfaces;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Dto.Implementations
{
    public class ReportsDto: IWeeklyReportDto
    {
        public int ID { get; set; }

        public int AuthorId { get; set; }

        public int MoraleGradeId { get; set; }
        public IGrade MoraleGrade { get; set; }

        public int StressGradeId { get; set; }
        public IGrade StressGrade { get; set; }

        public int WorkloadGradeId { get; set; }
        public IGrade WorkloadGrade { get; set; }
        public string HighThisWeek { get; set; }
        public string LowThisWeek { get; set; }
        public string AnythingElse { get; set; }
        public DateTime Date { get; set; }

        public DateTime WeekStartDate { get; }
        public DateTime WeekEndDate { get; }
    }
}
