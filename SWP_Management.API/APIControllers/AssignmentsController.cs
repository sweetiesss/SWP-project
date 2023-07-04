using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;
using System.Text.Json;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/assignments")]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentController(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        [HttpGet]
        public IActionResult GetAssignments()
        {
            var assignments = _assignmentRepository.GetList();
            return Ok(assignments);
        }

        [HttpGet("{assignmentId}")]
        public IActionResult GetAssignment(string assignmentId)
        {
            var assignment = _assignmentRepository.GetById(assignmentId);
            if (assignment == null)
            {
                return NotFound();
            }
            return Ok(assignment);
        }

        [HttpPost]
        public IActionResult CreateAssignment(Assignment assignment)
        {
            _assignmentRepository.Add(assignment);
            return CreatedAtAction(nameof(GetAssignment), new { assignmentId = assignment.Id }, assignment);
        }

        [HttpPut("{assignmentId}")]
        public IActionResult UpdateAssignment(string assignmentId, Assignment assignment)
        {
            if (assignmentId != assignment.Id)
            {
                return BadRequest();
            }

            var existingAssignment = _assignmentRepository.GetById(assignmentId);
            if (existingAssignment == null)
            {
                return NotFound();
            }

            _assignmentRepository.Update(assignment);
            return NoContent();
        }

        [HttpDelete("{assignmentId}")]
        public IActionResult DeleteAssignment(string assignmentId)
        {
            var assignment = _assignmentRepository.GetById(assignmentId);
            if (assignment == null)
            {
                return NotFound();
            }

            _assignmentRepository.Delete(assignmentId);
            return NoContent();
        }
    }
}
