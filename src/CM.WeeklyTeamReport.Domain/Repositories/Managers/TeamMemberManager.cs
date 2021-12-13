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
    }
}
