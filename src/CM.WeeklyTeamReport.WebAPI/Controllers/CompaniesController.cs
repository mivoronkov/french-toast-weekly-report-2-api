using CM.WeeklyTeamReport.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
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

        [HttpPost]
        public ActionResult<Company> Create(Company company)
        {
            var createdCompany = _repository.Create(company);
            if (createdCompany == null)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return Created("/api/companies/" + createdCompany.ID, createdCompany);
        }

        [HttpPut("{companyId}")]
        public ActionResult<Company> Put(Company company)
        {
            var existingCompany = _repository.Read(company.ID);
            if (existingCompany == null)
                return Create(company);
            _repository.Update(company);
            return Ok(company);
        }

        [HttpDelete("{companyId}")]
        public IActionResult Delete(Company company)
        {
            _repository.Delete(company);
            return NoContent();
        }
    }
}
