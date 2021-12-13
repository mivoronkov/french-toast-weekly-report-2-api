using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Repositories.Managers;
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
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private CompanyManager _manager;
        public CompaniesController(CompanyManager companyManager)
        {
            _manager = companyManager;
        }
        // Get api/<CompanyController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _manager.readAllCompanies();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        // Get api/<CompanyController>/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _manager.readCompany(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        // PUT api/<CompanyController>/id
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] string name, int id)
        {
            var result = _manager.readCompany(id);
            if (result == null)
            {
                return NotFound();
            }
            result.Name = name;
            _manager.updateCompany(result);
            return NoContent();
        }
        // DELETE api/<CompanyController>/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _manager.readCompany(id);
            if (result == null)
            {
                return NotFound();
            }
            _manager.deleteCompany(id);
            return NoContent();
        }
    }
}
