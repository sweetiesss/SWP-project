using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/subjecttopics")]
    public class SubjectTopicController : ControllerBase
    {
        private readonly ISubjectTopicRepository _subjectTopicRepository;

        public SubjectTopicController(ISubjectTopicRepository subjectTopicRepository)
        {
            _subjectTopicRepository = subjectTopicRepository;
        }

        [HttpGet]
        public IActionResult GetSubjectTopics()
        {
            var subjectTopics = _subjectTopicRepository.GetList();
            return Ok(subjectTopics);
        }

        [HttpGet("{subjectTopicId}")]
        public IActionResult GetSubjectTopic(int subjectTopicId)
        {
            var subjectTopic = _subjectTopicRepository.GetById(subjectTopicId);
            if (subjectTopic == null)
            {
                return NotFound();
            }
            return Ok(subjectTopic);
        }

        [HttpPost]
        public IActionResult CreateSubjectTopic(SubjectTopic subjectTopic)
        {
            _subjectTopicRepository.Add(subjectTopic);
            return CreatedAtAction(nameof(GetSubjectTopic), new { subjectTopicId = subjectTopic.Id }, subjectTopic);
        }

        [HttpPut("{subjectTopicId}")]
        public IActionResult UpdateSubjectTopic(int subjectTopicId, SubjectTopic subjectTopic)
        {
            if (subjectTopicId != subjectTopic.Id)
            {
                return BadRequest();
            }

            var existingSubjectTopic = _subjectTopicRepository.GetById(subjectTopicId);
            if (existingSubjectTopic == null)
            {
                return NotFound();
            }

            _subjectTopicRepository.Update(subjectTopic);
            return NoContent();
        }

        [HttpDelete("{subjectTopicId}")]
        public IActionResult DeleteSubjectTopic(int subjectTopicId)
        {
            var subjectTopic = _subjectTopicRepository.GetById(subjectTopicId);
            if (subjectTopic == null)
            {
                return NotFound();
            }

            _subjectTopicRepository.Delete(subjectTopicId);
            return NoContent();
        }
    }
}
