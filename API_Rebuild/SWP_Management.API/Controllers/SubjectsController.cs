using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/subjects")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        public IActionResult GetSubjects()
        {
            var subjects = _subjectRepository.GetList();
            return Ok(subjects);
        }

        [HttpGet("{subjectId}")]
        public IActionResult GetSubject(string subjectId)
        {
            var subject = _subjectRepository.GetById(subjectId);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        [HttpPost]
        public IActionResult CreateSubject(Subject subject)
        {
            _subjectRepository.Add(subject);
            return CreatedAtAction(nameof(GetSubject), new { subjectId = subject.Id }, subject);
        }

        [HttpPut("{subjectId}")]
        public IActionResult UpdateSubject(string subjectId, Subject subject)
        {
            if (subjectId != subject.Id)
            {
                return BadRequest();
            }

            var existingSubject = _subjectRepository.GetById(subjectId);
            if (existingSubject == null)
            {
                return NotFound();
            }

            _subjectRepository.Update(subject);
            return NoContent();
        }

        [HttpDelete("{subjectId}")]
        public IActionResult DeleteSubject(string subjectId)
        {
            var subject = _subjectRepository.GetById(subjectId);
            if (subject == null)
            {
                return NotFound();
            }

            _subjectRepository.Delete(subjectId);
            return NoContent();
        }
    }
}
