using System.Collections.Generic;

namespace CM.WeeklyTeamReport.Domain.Exceptions
{
    public class DbRecordNotFoundException : KeyNotFoundException
    {
        public DbRecordNotFoundException(string msg) : base(msg)
        {
        }
    }
}
