using System;

namespace CM.WeeklyTeamReport.Domain
{
    public class WeeklyReport : IEntity
    {
        const DayOfWeek StartOfWeek = DayOfWeek.Monday;

        public override int ID { get; set; }

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

    public class Grade : IEntity
    {
        public Level Level { get; set; }
        public string Commentary { get; set; }
        public override int ID { get; set; }
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
