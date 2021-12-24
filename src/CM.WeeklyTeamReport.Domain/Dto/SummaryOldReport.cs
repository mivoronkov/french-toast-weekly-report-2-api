using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Dto
{
    public class SummaryOldReport
    {
        public SummaryOldReport() { }
        public AverageOldReportDto AverageOldReportDto { get; set; }
        public ICollection<OverviewReportDto> OverviewReportsDtos { get; set; }
    }
}
