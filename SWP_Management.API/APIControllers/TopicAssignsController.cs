using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/topicassigns")]
    public class TopicAssignController : ControllerBase
    {
        private readonly ITopicAssignRepository _topicAssignRepository;

        public TopicAssignController(ITopicAssignRepository topicAssignRepository)
        {
            _topicAssignRepository = topicAssignRepository;
        }

        [HttpGet]
        public IActionResult GetTopicAssigns()
        {
            var topicAssigns = _topicAssignRepository.GetList();
            return Ok(topicAssigns);
        }

        [HttpGet("{topicAssignId}")]
        public IActionResult GetTopicAssign(int topicAssignId)
        {
            var topicAssign = _topicAssignRepository.GetById(topicAssignId);
            if (topicAssign == null)
            {
                return NotFound();
            }
            return Ok(topicAssign);
        }

        [HttpPost]
        public IActionResult CreateTopicAssign(TopicAssign topicAssign)
        {
            _topicAssignRepository.Add(topicAssign);
            return CreatedAtAction(nameof(GetTopicAssign), new { topicAssignId = topicAssign.Id }, topicAssign);
        }

        [HttpPut("{topicAssignId}")]
        public IActionResult UpdateTopicAssign(int topicAssignId, TopicAssign topicAssign)
        {
            if (topicAssignId != topicAssign.Id)
            {
                return BadRequest();
            }

            var existingTopicAssign = _topicAssignRepository.GetById(topicAssignId);
            if (existingTopicAssign == null)
            {
                return NotFound();
            }

            _topicAssignRepository.Update(topicAssign);
            return NoContent();
        }

        [HttpDelete("{topicAssignId}")]
        public IActionResult DeleteTopicAssign(int topicAssignId)
        {
            var topicAssign = _topicAssignRepository.GetById(topicAssignId);
            if (topicAssign == null)
            {
                return NotFound();
            }

            _topicAssignRepository.Delete(topicAssignId);
            return NoContent();
        }
    }
}
