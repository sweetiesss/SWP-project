using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class assignmentsController : ControllerBase
    {
        private readonly SWP_DATAContext _context;
        private readonly IAssignmentRepository _assignmentRepository;

        public assignmentsController(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        // GET: api/assignments
        [HttpGet]
        public IActionResult GetList()
        {
            var entities = _assignmentRepository.GetList();
            return Ok(entities);
        }

        // GET: api/assignments/5
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var entity = _assignmentRepository.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        // PUT: api/Assignments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Assignment entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            _assignmentRepository.Update(entity);
            return NoContent();
        }

        // POST: api/Assignments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post([FromBody] Assignment entity)
        {
            _assignmentRepository.Add(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        // DELETE: api/Assignments/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _assignmentRepository.Delete(id);
            return NoContent();
        }

        private bool AssignmentExists(string id)
        {
            return (_context.Assignments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
