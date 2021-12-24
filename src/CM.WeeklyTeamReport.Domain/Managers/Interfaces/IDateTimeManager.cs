using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Managers.Interfaces
{
    public interface IDateTimeManager
    {
        public DateTime TakeDateTime();
        public DateTime TakeDateTime(int days);
        public DateTime TakeMonday();
        public DateTime TakeMonday(int days);
        public DateTime TakeMonday(DateTime weekDay);
        public DateTime TakeSunday();
        public DateTime TakeSunday(int days);
        public DateTime TakeSunday(DateTime weekDay);

    }
}
