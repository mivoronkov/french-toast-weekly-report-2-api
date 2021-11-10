using System;

namespace CM.WeeklyTeamReport.Domain
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDateInWeek(this DateTime dt, DayOfWeek weekStartDay)
        {
            while (dt.DayOfWeek != weekStartDay)
                dt = dt.AddDays(-1);
            return dt;
        }
    }
}
