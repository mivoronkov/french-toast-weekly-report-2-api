using System;

namespace CM.WeeklyTeamReport.Domain
{
    public class WeeklyReport
    {
        const DayOfWeek StartOfWeek = DayOfWeek.Monday;

        public WeeklyReport(
            TeamMember author, 
            Grade moraleGrade, 
            Grade stressGrade, 
            Grade workloadGrade, 
            string highThisWeek, 
            string lowThisWeek,
            DateTime reportDate,
            string? anythingElse = null)
        {
            Author = author;
            MoraleGrade = moraleGrade;
            StressGrade = stressGrade;
            WorkloadGrade = workloadGrade;
            HighThisWeek = highThisWeek;
            LowThisWeek = lowThisWeek;
            Date = reportDate;
            AnythingElse = anythingElse;
        }

        public TeamMember Author { get; }
        public Grade MoraleGrade { get; }
        public Grade StressGrade { get; }
        public Grade WorkloadGrade { get; }
        public string HighThisWeek { get; }
        public string LowThisWeek { get; }
        public string? AnythingElse { get; }
        public DateTime Date { get; }

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
