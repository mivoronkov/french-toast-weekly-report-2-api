using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;

namespace CM.WeeklyTeamReport.Domain
{
    public class Company : ICompany
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime? CreationDate { get; set; }

        public Company() { }
    }
}
