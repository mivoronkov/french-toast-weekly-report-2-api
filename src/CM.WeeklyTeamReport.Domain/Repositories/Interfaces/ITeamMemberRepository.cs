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
        public ICollection<ITeamMember> ReadAll();
        public ICollection<ITeamMember> ReadAll(int companyID);
        public ITeamMember Read(int entityId);
        public ITeamMember Read(int companyId, int entityId);
        public void Update(ITeamMember entity);
        public void Delete(ITeamMember entity);
        public void Delete(int entityId);

    }
}