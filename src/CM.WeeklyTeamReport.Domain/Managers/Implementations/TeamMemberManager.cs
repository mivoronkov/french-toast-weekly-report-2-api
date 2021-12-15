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
        public TeamMemberManager(ITeamMemberRepository teamMemberRepository, ICompanyRepository companyRepository) 
        {
            _teamMemberRepository = teamMemberRepository;
            _companyRepository = companyRepository;
        }

        public ITeamMember create(TeamMemberDto teamMember)
        {
            var newTeamMember = dtoToTeamMember(teamMember);
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
            var teamMembersDto = teamMembers.Select(el => teamMemberToDto(el, companyName)).ToList();

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
            var teamMemberDto = teamMemberToDto(teamMember, companyName);

            return teamMemberDto;
        }

        public void update(TeamMemberDto oldEntity, TeamMemberDto newEntity)
        {
            newEntity.ID = oldEntity.ID;
            newEntity.CompanyId = oldEntity.CompanyId;
            var teamMember = dtoToTeamMember(newEntity);
            _teamMemberRepository.Update(teamMember);
        }

        private TeamMemberDto teamMemberToDto(ITeamMember teamMember, string company)
        {
            var teamMemberDto = new TeamMemberDto();
            teamMemberDto.ID = teamMember?.ID;
            teamMemberDto.FirstName = teamMember.FirstName;
            teamMemberDto.LastName = teamMember.LastName;
            teamMemberDto.Title = teamMember.Title;
            teamMemberDto.Email = teamMember.Email;
            teamMemberDto.CompanyName = company;
            teamMemberDto.CompanyId = teamMember.CompanyId;
            teamMemberDto.InviteLink = teamMember?.InviteLink;

            return teamMemberDto;
        }
        private ITeamMember dtoToTeamMember(TeamMemberDto teamMemberDto)
        {
            var teamMember = new TeamMember();
            teamMember.ID = (int)teamMemberDto.ID;
            teamMember.FirstName = teamMemberDto.FirstName;
            teamMember.LastName = teamMemberDto.LastName;
            teamMember.Email = teamMemberDto.Email;
            teamMember.Title = teamMemberDto.Title;
            teamMember.CompanyId = (int)teamMemberDto.CompanyId;
            teamMember.InviteLink = teamMemberDto.InviteLink;

            return teamMember;
        }
    }
}
