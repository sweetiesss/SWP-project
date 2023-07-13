using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/semesters")]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterRepository _semesterRepository;

        public SemesterController(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }

        [HttpGet]
        public IActionResult GetSemesters()
        {
            var semesters = _semesterRepository.GetList();
            return Ok(semesters);
        }

        [HttpGet("{semesterId}")]
        public IActionResult GetSemester(string semesterId)
        {
            var semester = _semesterRepository.GetById(semesterId);
            if (semester == null)
            {
                return NotFound();
            }
            return Ok(semester);
        }

        [HttpPost]
        public IActionResult CreateSemester(Semester semester)
        {
            _semesterRepository.Add(semester);
            return CreatedAtAction(nameof(GetSemester), new { semesterId = semester.Id }, semester);
        }

        [HttpPut("{semesterId}")]
        public IActionResult UpdateSemester(string semesterId, Semester semester)
        {
            if (semesterId != semester.Id)
            {
                return BadRequest();
            }

            var existingSemester = _semesterRepository.GetById(semesterId);
            if (existingSemester == null)
            {
                return NotFound();
            }

            _semesterRepository.Update(semester);
            return NoContent();
        }

        [HttpDelete("{semesterId}")]
        public IActionResult DeleteSemester(string semesterId)
        {
            var semester = _semesterRepository.GetById(semesterId);
            if (semester == null)
            {
                return NotFound();
            }

            _semesterRepository.Delete(semesterId);
            return NoContent();
        }
    }
}
