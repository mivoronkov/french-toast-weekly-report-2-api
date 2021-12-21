using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Dto
{
    public class CompanyDto
    {
        public int? ID { get; set; } = 0;

        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public CompanyDto() { }
    }
}
