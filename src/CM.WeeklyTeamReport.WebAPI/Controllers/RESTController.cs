﻿using CM.WeeklyTeamReport.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.WeeklyTeamReport.WebAPI.Controllers
{
    public abstract class RESTController<TEntity> : ControllerBase where TEntity : IEntity
    {
        protected IRepository<TEntity> _repository;

        public abstract string EntitiesEndpoint { get; }

        public RESTController(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        // GET /api/entities/
        [HttpGet]
        public virtual IActionResult Get()
        {
            return Ok(_repository.ReadAll());
        }

        // GET /api/entities/{id}
        [HttpGet("{id}")]
        public virtual IActionResult GetSingle(int id)
        {
            if (id < 1)
                return BadRequest();
            var entity = _repository.Read(id);
            if (entity == null)
                return NotFound(entity);
            return Ok(entity);
        }

        // POST /api/entities/
        [HttpPost]
        public virtual IActionResult Create(TEntity entity)
        {
            var createdEntity = _repository.Create(entity);
            if (createdEntity == null)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return Created($"{EntitiesEndpoint}/{createdEntity.ID}", createdEntity);
        }


        // PUT /api/entities/{id}
        [HttpPut("{id}")]
        public virtual IActionResult Put(int id, TEntity entity)
        {
            var existingCompany = _repository.Read(id);
            if (existingCompany == null)
                return Create(entity);
            entity.ID = id;
            _repository.Update(entity);
            return Ok(entity);
        }

        // DELETE /api/entities/{id}
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {
            if (_repository.Read(id) == null)
                return NotFound();
            _repository.Delete(id);
            return NoContent();
        }
    }
}