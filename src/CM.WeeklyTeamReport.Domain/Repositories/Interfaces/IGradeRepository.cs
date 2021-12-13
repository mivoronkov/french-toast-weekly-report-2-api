using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface IGradeRepository
    {
        public IGrade Create(IGrade entity);

        public IGrade Read(int entityId);

        public void Update(IGrade entity);

        public void Delete(IGrade entity);

        public void Delete(int entityId);

        public ICollection<IGrade> ReadAll();
    }
}