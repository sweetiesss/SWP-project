using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/studentcourses")]
    public class StudentCourseController : ControllerBase
    {
        private readonly IStudentCourseRepository _studentCourseRepository;

        public StudentCourseController(IStudentCourseRepository studentCourseRepository)
        {
            _studentCourseRepository = studentCourseRepository;
        }

        [HttpGet]
        public IActionResult GetStudentCourses()
        {
            var studentCourses = _studentCourseRepository.GetList();
            return Ok(studentCourses);
        }

        [HttpGet("{studentCourseId}")]
        public IActionResult GetStudentCourse(int studentCourseId)
        {
            var studentCourse = _studentCourseRepository.GetById(studentCourseId);
            if (studentCourse == null)
            {
                return NotFound();
            }
            return Ok(studentCourse);
        }

        [HttpPost]
        public IActionResult CreateStudentCourse(StudentCourse studentCourse)
        {
            _studentCourseRepository.Add(studentCourse);
            return CreatedAtAction(nameof(GetStudentCourse), new { studentCourseId = studentCourse.Id }, studentCourse);
        }

        [HttpPut("{studentCourseId}")]
        public IActionResult UpdateStudentCourse(int studentCourseId, StudentCourse studentCourse)
        {
            if (studentCourseId != studentCourse.Id)
            {
                return BadRequest();
            }

            var existingStudentCourse = _studentCourseRepository.GetById(studentCourseId);
            if (existingStudentCourse == null)
            {
                return NotFound();
            }

            _studentCourseRepository.Update(studentCourse);
            return NoContent();
        }

        [HttpDelete("{studentCourseId}")]
        public IActionResult DeleteStudentCourse(int studentCourseId)
        {
            var studentCourse = _studentCourseRepository.GetById(studentCourseId);
            if (studentCourse == null)
            {
                return NotFound();
            }

            _studentCourseRepository.Delete(studentCourseId);
            return NoContent();
        }
    }
}
