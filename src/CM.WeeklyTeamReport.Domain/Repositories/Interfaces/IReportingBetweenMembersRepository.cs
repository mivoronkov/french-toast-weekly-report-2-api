using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface IReportingBetweenMembersRepository
    {
    public IReportingBetweenMembers Create(IReportingBetweenMembers entity);

    public IReportingBetweenMembers Read(int entityId);

    public void Update(IReportingBetweenMembers entity);

    public void Delete(IReportingBetweenMembers entity);

    public void Delete(int entityId);

    public ICollection<IReportingBetweenMembers> ReadAll();
}
}
