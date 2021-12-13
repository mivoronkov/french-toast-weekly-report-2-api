using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    interface IReportingBetweenTeamMembersRepository<TEntity> where TEntity : IReportingBetweenTeamMembers
    {
    public TEntity Create(TEntity entity);

    public TEntity Read(int entityId);

    public void Update(TEntity entity);

    public void Delete(TEntity entity);

    public void Delete(int entityId);

    public ICollection<TEntity> ReadAll();
}
}
