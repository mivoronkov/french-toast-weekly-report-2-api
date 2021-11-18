using System;

namespace CM.WeeklyTeamReport.Domain
{
    public class WeeklyReport
    {
        const DayOfWeek StartOfWeek = DayOfWeek.Monday;

        public WeeklyReport(
            TeamMember author, 
            Estimation moraleEstimation, 
            Estimation stressEstimation, 
            Estimation workloadEstimation, 
            string highThisWeek, 
            string lowThisWeek, 
            DateTime reportDate)
        {
            Author = author;
            MoraleEstimation = moraleEstimation;
            StressEstimation = stressEstimation;
            WorkloadEstimation = workloadEstimation;
            HighThisWeek = highThisWeek;
            LowThisWeek = lowThisWeek;
            Date = reportDate;
        }

        public TeamMember Author { get; }
        public Estimation MoraleEstimation { get; }
        public Estimation StressEstimation { get; }
        public Estimation WorkloadEstimation { get; }
        public string HighThisWeek { get; }
        public string LowThisWeek { get; }
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

    public class Estimation
    {
        public Level Level { get; }
        public string Commentary { get; }

        public Estimation(Level estimationLevel, string commentary = null)
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
