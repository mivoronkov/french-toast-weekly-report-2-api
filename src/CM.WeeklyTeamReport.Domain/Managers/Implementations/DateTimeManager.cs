using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Managers.Implementations
{
    public class DateTimeManager: IDateTimeManager
    {
        public DateTimeManager() { }

        public DateTime TakeDateTime()
        {
            return DateTime.Now;
        }

        public DateTime TakeDateTime(int days)
        {
            return DateTime.Now.AddDays(days);
        }

        public DateTime TakeMonday()
        {
            return TakeDateTime().FirstDateInWeek(IWeeklyReport.StartOfWeek); 
        }

        public DateTime TakeMonday(int days)
        {
            return TakeDateTime(days).FirstDateInWeek(IWeeklyReport.StartOfWeek);
        }

        public DateTime TakeMonday(DateTime weekDay)
        {
            return weekDay.FirstDateInWeek(IWeeklyReport.StartOfWeek);
        }
    }
}
