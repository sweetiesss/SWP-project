using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/lecturers")]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerRepository _lecturerRepository;

        public LecturerController(ILecturerRepository lecturerRepository)
        {
            _lecturerRepository = lecturerRepository;
        }

        [HttpGet]
        public IActionResult GetLecturers()
        {
            var lecturers = _lecturerRepository.GetList();
            return Ok(lecturers);
        }

        [HttpGet("{lecturerId}")]
        public IActionResult GetLecturer(string lecturerId)
        {
            var lecturer = _lecturerRepository.GetById(lecturerId);
            if (lecturer == null)
            {
                return NotFound();
            }
            return Ok(lecturer);
        }

        [HttpPost]
        public IActionResult CreateLecturer(Lecturer lecturer)
        {
            _lecturerRepository.Add(lecturer);
            return CreatedAtAction(nameof(GetLecturer), new { lecturerId = lecturer.Id }, lecturer);
        }

        [HttpPut("{lecturerId}")]
        public IActionResult UpdateLecturer(string lecturerId, Lecturer lecturer)
        {
            if (lecturerId != lecturer.Id)
            {
                return BadRequest();
            }

            var existingLecturer = _lecturerRepository.GetById(lecturerId);
            if (existingLecturer == null)
            {
                return NotFound();
            }

            _lecturerRepository.Update(lecturer);
            return NoContent();
        }

        [HttpDelete("{lecturerId}")]
        public IActionResult DeleteLecturer(string lecturerId)
        {
            var lecturer = _lecturerRepository.GetById(lecturerId);
            if (lecturer == null)
            {
                return NotFound();
            }

            _lecturerRepository.Delete(lecturerId);
            return NoContent();
        }
    }
}
