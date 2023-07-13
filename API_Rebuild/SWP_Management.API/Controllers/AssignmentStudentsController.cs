using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/assignmentstudents")]
    public class AssignmentStudentController : ControllerBase
    {
        private readonly IAssignmentStudentRepository _assignmentStudentRepository;

        public AssignmentStudentController(IAssignmentStudentRepository assignmentStudentRepository)
        {
            _assignmentStudentRepository = assignmentStudentRepository;
        }

        [HttpGet]
        public IActionResult GetAssignmentStudents()
        {
            var assignmentStudents = _assignmentStudentRepository.GetList();
            return Ok(assignmentStudents);
        }

        [HttpGet("{assignmentStudentId}")]
        public IActionResult GetAssignmentStudent(int assignmentStudentId)
        {
            var assignmentStudent = _assignmentStudentRepository.GetById(assignmentStudentId);
            if (assignmentStudent == null)
            {
                return NotFound();
            }
            return Ok(assignmentStudent);
        }

        [HttpPost]
        public IActionResult CreateAssignmentStudent(AssignmentStudent assignmentStudent)
        {
            _assignmentStudentRepository.Add(assignmentStudent);
            return CreatedAtAction(nameof(GetAssignmentStudent), new { assignmentStudentId = assignmentStudent.Id }, assignmentStudent);
        }

        [HttpPut("{assignmentStudentId}")]
        public IActionResult UpdateAssignmentStudent(int assignmentStudentId, AssignmentStudent assignmentStudent)
        {
            if (assignmentStudentId != assignmentStudent.Id)
            {
                return BadRequest();
            }

            var existingAssignmentStudent = _assignmentStudentRepository.GetById(assignmentStudentId);
            if (existingAssignmentStudent == null)
            {
                return NotFound();
            }

            _assignmentStudentRepository.Update(assignmentStudent);
            return NoContent();
        }

        [HttpDelete("{assignmentStudentId}")]
        public IActionResult DeleteAssignmentStudent(int assignmentStudentId)
        {
            var assignmentStudent = _assignmentStudentRepository.GetById(assignmentStudentId);
            if (assignmentStudent == null)
            {
                return NotFound();
            }

            _assignmentStudentRepository.Delete(assignmentStudentId);
            return NoContent();
        }
    }
}
