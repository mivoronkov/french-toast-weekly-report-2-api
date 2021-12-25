using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Dto
{
    public class HistoryReportDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<IWeeklyInformation> WeeklyInformation { get; set; }
        public ICollection<IWeeklyNotations> WeeklyNotations { get; set; }
        public string AvatarPath { get; set; }

    }
}
