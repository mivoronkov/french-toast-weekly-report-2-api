using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Managers
{
    public class TeamMemberManager: ITeamMemberManager
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly ICompanyRepository _companyRepository;
        public TeamMemberManager(ITeamMemberRepository teamMemberRepository, ICompanyRepository companyRepository) 
        {
            _teamMemberRepository = teamMemberRepository;
            _companyRepository = companyRepository;
        }

        public ITeamMember createTeamMember(ITeamMember newTeamMember)
        {
            throw new NotImplementedException();
        }

        public void deleteTeamMember(int entityIdy)
        {
            throw new NotImplementedException();
        }

        public ICollection<ITeamMember> readAllmembers()
        {
            throw new NotImplementedException();
        }

        public ITeamMember readTeamMember(int id)
        {
            throw new NotImplementedException();
        }

        public void updateTeamMember(ITeamMember entity)
        {
            throw new NotImplementedException();
        }
    }
}
