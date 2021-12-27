using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Commands
{
    public class LinkCommands : ILinkCommands
    {
        public IEnumerable<int> LinksDifference(ICollection<int> subtractedLinks, ICollection<int> resultLinks)
        {
            return resultLinks.Except(subtractedLinks);
        }
    }
}
