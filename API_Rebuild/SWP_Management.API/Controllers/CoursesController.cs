using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = _courseRepository.GetList();
            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        public IActionResult GetCourse(string courseId)
        {
            var course = _courseRepository.GetById(courseId);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public IActionResult CreateCourse(Course course)
        {
            _courseRepository.Add(course);
            return CreatedAtAction(nameof(GetCourse), new { courseId = course.Id }, course);
        }

        [HttpPut("{courseId}")]
        public IActionResult UpdateCourse(string courseId, Course course)
        {
            if (courseId != course.Id)
            {
                return BadRequest();
            }

            var existingCourse = _courseRepository.GetById(courseId);
            if (existingCourse == null)
            {
                return NotFound();
            }

            _courseRepository.Update(course);
            return NoContent();
        }

        [HttpDelete("{courseId}")]
        public IActionResult DeleteCourse(string courseId)
        {
            var course = _courseRepository.GetById(courseId);
            if (course == null)
            {
                return NotFound();
            }

            _courseRepository.Delete(courseId);
            return NoContent();
        }
    }
}
