using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface ITeamMemberManager
    {
        public ICollection<ITeamMember> readAllmembers();
        public ITeamMember readTeamMember(int id);
        public ITeamMember createTeamMember(ITeamMember newTeamMember);
        public void updateTeamMember(ITeamMember entity);
        public void deleteTeamMember(int entityIdy);


    }
}
