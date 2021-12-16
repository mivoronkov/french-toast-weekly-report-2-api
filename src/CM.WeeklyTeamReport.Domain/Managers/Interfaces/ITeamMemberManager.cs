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
        public ICollection<TeamMemberDto> readAll(int companyId);
        public TeamMemberDto read(int companyId, int teamMemberId);
        public ITeamMember create(TeamMemberDto newTeamMember);
        public void update(TeamMemberDto oldEntity, TeamMemberDto newEntity);
        public void delete(int companyId, int entityIdy);
    }
}
