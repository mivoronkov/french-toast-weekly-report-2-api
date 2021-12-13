using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Entities.Interfaces
{
    interface IWeeklyReport
    {
        static DayOfWeek StartOfWeek = DayOfWeek.Monday;
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
