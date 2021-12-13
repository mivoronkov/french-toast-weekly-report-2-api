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
    public class GradeRepository : IGradeRepository
    {
        public IGrade Create(IGrade entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IGrade entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public IGrade Read(int entityId)
        {
            throw new NotImplementedException();
        }

        public ICollection<IGrade> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(IGrade entity)
        {
            throw new NotImplementedException();
        }
    }
}
