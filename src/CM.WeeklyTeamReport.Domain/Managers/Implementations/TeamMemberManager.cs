using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Dto;
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
        private readonly MemberCommands _memberCommands;

        public TeamMemberManager(ITeamMemberRepository teamMemberRepository, ICompanyRepository companyRepository, MemberCommands memberCommands) 
        {
            _teamMemberRepository = teamMemberRepository;
            _companyRepository = companyRepository;
            _memberCommands = memberCommands;
        }

        public ITeamMember create(TeamMemberDto teamMember)
        {
            var newTeamMember = _memberCommands.dtoToTeamMember(teamMember);
            return _teamMemberRepository.Create(newTeamMember);
        }

        public void delete(int companyId, int teamMemberId)
        {
            _teamMemberRepository.Delete(teamMemberId);
        }

        public ICollection<TeamMemberDto> readAll(int companyId)
        {
            var teamMembers = _teamMemberRepository.ReadAll(companyId);
            string companyName = _companyRepository.GetCompanyName(companyId);
            var teamMembersDto = teamMembers.Select(el => _memberCommands.teamMemberToDto(el, companyName)).ToList();

            return teamMembersDto;
        }

        public TeamMemberDto read(int companyId, int teamMemberId)
        {
            var teamMember = _teamMemberRepository.Read(companyId, teamMemberId);
            if(teamMember == null)
            {
                return null;
            }
            string companyName = _companyRepository.GetCompanyName(companyId);
            var teamMemberDto = _memberCommands.teamMemberToDto(teamMember, companyName);

            return teamMemberDto;
        }

        public void update(TeamMemberDto oldEntity, TeamMemberDto newEntity)
        {
            newEntity.ID = oldEntity.ID;
            newEntity.CompanyId = oldEntity.CompanyId;
            var teamMember = _memberCommands.dtoToTeamMember(newEntity);
            _teamMemberRepository.Update(teamMember);
        }        
    }
}
