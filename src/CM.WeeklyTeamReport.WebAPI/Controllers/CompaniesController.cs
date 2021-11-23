using CM.WeeklyTeamReport.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private IRepository<Company> _repository;

        public CompaniesController(IRepository<Company> repository)
        {
            _repository = repository;
        }

        // GET: CompaniesController
        [HttpGet]
        public ICollection<Company> Get()
        {
            return _repository.ReadAll();
        }

        [HttpGet("{companyId}")]
        public ActionResult<Company> GetSingle(int companyId)
        {
            if (companyId < 1)
                return BadRequest();
            var company = _repository.Read(companyId);
            if (company == null)
                return NotFound(company);
            return Ok(company);
        }
    }
}
