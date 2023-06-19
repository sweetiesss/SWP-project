using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWP_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class assignmentStudentsController : ControllerBase
    {
        private readonly IAssignmentStudentRepository _assignmentStudentRepository;

        public assignmentStudentsController(IAssignmentStudentRepository assignmentStudentRepository)
        {
            _assignmentStudentRepository = assignmentStudentRepository;
        }

        // GET: api/AssignmentStudents
        [HttpGet]
        public IActionResult GetList()
        {
            var entities = _assignmentStudentRepository.GetList();
            return Ok(entities);
        }

        // GET: api/AssignmentStudents/{taskId}/{studentId}
        [HttpGet("{taskId}/{studentId}")]
        public IActionResult GetById(string taskId, string studentId)
        {
            var entity = _assignmentStudentRepository.GetById(taskId, studentId);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        // POST: api/AssignmentStudents
        [HttpPost]
        public IActionResult Post([FromBody] AssignmentStudent entity)
        {
            _assignmentStudentRepository.Add(entity);
            return CreatedAtAction(nameof(GetById), new { taskId = entity.TaskId, studentId = entity.StudentId }, entity);
        }

        // PUT: api/AssignmentStudents/{taskId}/{studentId}
        [HttpPut("{taskId}/{studentId}")]
        public IActionResult Put(string taskId, string studentId, [FromBody] AssignmentStudent entity)
        {
            if (taskId != entity.TaskId || studentId != entity.StudentId)
            {
                return BadRequest();
            }

            _assignmentStudentRepository.Update(entity);
            return NoContent();
        }

        // DELETE: api/AssignmentStudents/{taskId}/{studentId}
        [HttpDelete("{taskId}/{studentId}")]
        public IActionResult Delete(string taskId, string studentId)
        {
            _assignmentStudentRepository.Delete(taskId, studentId);
            return NoContent();
        }
    }
}
