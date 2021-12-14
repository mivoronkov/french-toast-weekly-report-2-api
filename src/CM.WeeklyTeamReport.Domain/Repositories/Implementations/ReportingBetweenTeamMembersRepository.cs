using CM.WeeklyTeamReport.Domain.Entities.Implementations;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Implementations
{
    public class ReportingBetweenTeamMembersRepository : IReportingBetweenMembersRepository
    {
        public IReportingBetweenMembers Create(IReportingBetweenMembers entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IReportingBetweenMembers entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public IReportingBetweenMembers Read(int entityId)
        {
            throw new NotImplementedException();
        }

        public ICollection<IReportingBetweenMembers> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(IReportingBetweenMembers entity)
        {
            throw new NotImplementedException();
        }
    }
}
