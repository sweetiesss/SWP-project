using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class coursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public coursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var courses = _courseRepository.GetList();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null)
                return NotFound();

            return Ok(course);
        }

        [HttpPost]
        public IActionResult Add(Course course)
        {
            _courseRepository.Add(course);
            _courseRepository.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Course course)
        {
            var existingCourse = _courseRepository.GetById(id);
            if (existingCourse == null)
                return NotFound();

            existingCourse.Name = course.Name;
            existingCourse.DateStart = course.DateStart;
            existingCourse.DateEnd = course.DateEnd;
            // Cập nhật các thuộc tính khác của course tại đây

            _courseRepository.Update(existingCourse);
            _courseRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null)
                return NotFound();

            _courseRepository.Delete(course);
            _courseRepository.SaveChanges();

            return NoContent();
        }
    }
}
