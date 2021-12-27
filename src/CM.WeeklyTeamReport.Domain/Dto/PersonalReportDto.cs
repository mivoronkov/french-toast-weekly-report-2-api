using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Dto
{
    // Тип отчёта, используемый на странице клиента "My Reports"
    public class PersonalReportDto
    {
        public ICollection<IWeeklyInformation> WeeklyInformation { get; set; }
        public ICollection<IWeeklyNotations> WeeklyNotations { get; set; }
        public DateTime DateStart { get; set; }

        public PersonalReportDto() { }
    }
}
