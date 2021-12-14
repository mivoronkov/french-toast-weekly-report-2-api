using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface ITeamMemberManager
    {
        public ICollection<ITeamMemberDto> readAllmembers(int companyId);
        public ITeamMemberDto readTeamMember(int companyId, int teamMemberId);
        public ITeamMember createTeamMember(ITeamMemberDto newTeamMember);
        public void updateTeamMember(ITeamMemberDto oldEntity, ITeamMemberDto newEntity);
        public void deleteTeamMember(int companyId, int entityIdy);


    }
}
