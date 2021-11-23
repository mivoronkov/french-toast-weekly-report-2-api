using System;
using System.Collections.Generic;

namespace CM.WeeklyTeamReport.Domain
{
    public class Company : IEntity
    {
        public override int ID { get; set; }

        public string Name { get; set; }

        public DateTime? CreationDate { get; set; }

        public Company() { }
    }
}
