using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Entities.Interfaces
{
    public interface IFullWeeklyReport
    {
        public int ID { get; set; }
        public int AuthorId { get; set; }
        public int MoraleGradeId { get; set; }
        public int MoraleLevel { get; set; }
        public string MoraleCommentary { get; set; }
        public int StressGradeId { get; set; }
        public int StressLevel { get; set; }
        public string StressCommentary { get; set; }
        public int WorkloadGradeId { get; set; }
        public int WorkloadLevel { get; set; }
        public string WorkloadCommentary { get; set; }
        public string HighThisWeek { get; set; }
        public string LowThisWeek { get; set; }
        public string AnythingElse { get; set; }
        public DateTime Date { get; set; }

    }
}
