using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentRepository.GetList();
            return Ok(students);
        }

        [HttpGet("{studentId}")]
        public IActionResult GetStudent(string studentId)
        {
            var student = _studentRepository.GetById(studentId);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            _studentRepository.Add(student);
            return CreatedAtAction(nameof(GetStudent), new { studentId = student.Id }, student);
        }

        [HttpPut("{studentId}")]
        public IActionResult UpdateStudent(string studentId, Student student)
        {
            if (studentId != student.Id)
            {
                return BadRequest();
            }

            var existingStudent = _studentRepository.GetById(studentId);
            if (existingStudent == null)
            {
                return NotFound();
            }

            _studentRepository.Update(student);
            return NoContent();
        }

        [HttpDelete("{studentId}")]
        public IActionResult DeleteStudent(string studentId)
        {
            var student = _studentRepository.GetById(studentId);
            if (student == null)
            {
                return NotFound();
            }

            _studentRepository.Delete(studentId);
            return NoContent();
        }
    }
}
