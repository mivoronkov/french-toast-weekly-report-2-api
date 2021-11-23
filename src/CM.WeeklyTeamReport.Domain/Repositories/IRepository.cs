using System.Collections.Generic;

namespace CM.WeeklyTeamReport.Domain
{
    public interface IRepository<TEntity>
    {
        public TEntity Create(TEntity entity);

        public TEntity Read(int entityId);

        public void Update(TEntity entity);

        public void Delete(TEntity entity);

        public ICollection<TEntity> ReadAll();
    }
}
