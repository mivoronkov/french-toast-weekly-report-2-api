using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Dto
{
    public interface ICompanyDto
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }


    }
}
