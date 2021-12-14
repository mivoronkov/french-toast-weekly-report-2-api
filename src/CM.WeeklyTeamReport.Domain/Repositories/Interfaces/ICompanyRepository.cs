using CM.WeeklyTeamReport.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.Domain.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        public ICompany Create(ICompany entity);

        public ICompany Read(int entityId);
        public string GetCompanyName(int entityId);

        public void Update(ICompany entity);

        public void Delete(ICompany entity);

        public void Delete(int entityId);

        public ICollection<ICompany> ReadAll();
    }
}
