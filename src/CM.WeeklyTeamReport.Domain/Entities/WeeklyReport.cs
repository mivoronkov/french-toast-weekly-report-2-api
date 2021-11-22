using System;

namespace CM.WeeklyTeamReport.Domain
{
    public class WeeklyReport : IEntity
    {
        const DayOfWeek StartOfWeek = DayOfWeek.Monday;

        public override int ID { get; set; }

        public int AuthorId { get; set; }
        public TeamMember Author { get; }

        public int MoraleGradeId { get; set; }
        public Grade MoraleGrade { get; }

        public int StressGradeId { get; set; }
        public Grade StressGrade { get; }

        public int WorkloadGradeId { get; set; }
        public Grade WorkloadGrade { get; }
        public string HighThisWeek { get; set; }
        public string LowThisWeek { get; set; }
        public string AnythingElse { get; set; }
        public DateTime Date { get; set; }

        public DateTime WeekStartDate {
            get {
                return Date.FirstDateInWeek(StartOfWeek);
            }
        }

        public DateTime WeekEndDate {
            get {
                return WeekStartDate.AddDays(6);
            }
        }

        public WeeklyReport() { }
    }

    public class Grade
    {
        public Level Level { get; }
        public string Commentary { get; }

        public Grade(Level estimationLevel, string commentary = null)
        {
            Level = estimationLevel;
            Commentary = commentary;
        }
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
