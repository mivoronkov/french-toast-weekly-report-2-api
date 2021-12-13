using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface IReportingBetweenTeamMembersRepository
    {
    public IReportingBetweenTeamMembers Create(IReportingBetweenTeamMembers entity);

    public IReportingBetweenTeamMembers Read(int entityId);

    public void Update(IReportingBetweenTeamMembers entity);

    public void Delete(IReportingBetweenTeamMembers entity);

    public void Delete(int entityId);

    public ICollection<IReportingBetweenTeamMembers> ReadAll();
}
}
