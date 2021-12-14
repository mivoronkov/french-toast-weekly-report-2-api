using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface ITeamMemberRepository
    {
        public ITeamMember Create(ITeamMember entity);

        public ITeamMember Read(int entityId);

        public void Update(ITeamMember entity);

        public void Delete(ITeamMember entity);

        public void Delete(int entityId);

        public ICollection<ITeamMember> ReadAll();
    }
}