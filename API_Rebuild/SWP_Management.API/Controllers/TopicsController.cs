using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/topics")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;

        public TopicController(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        [HttpGet]
        public IActionResult GetTopics()
        {
            var topics = _topicRepository.GetList();
            return Ok(topics);
        }

        [HttpGet("{topicId}")]
        public IActionResult GetTopic(string topicId)
        {
            var topic = _topicRepository.GetById(topicId);
            if (topic == null)
            {
                return NotFound();
            }
            return Ok(topic);
        }

        [HttpPost]
        public IActionResult CreateTopic(Topic topic)
        {
            _topicRepository.Add(topic);
            return CreatedAtAction(nameof(GetTopic), new { topicId = topic.Id }, topic);
        }

        [HttpPut("{topicId}")]
        public IActionResult UpdateTopic(string topicId, Topic topic)
        {
            if (topicId != topic.Id)
            {
                return BadRequest();
            }

            var existingTopic = _topicRepository.GetById(topicId);
            if (existingTopic == null)
            {
                return NotFound();
            }

            _topicRepository.Update(topic);
            return NoContent();
        }

        [HttpDelete("{topicId}")]
        public IActionResult DeleteTopic(string topicId)
        {
            var topic = _topicRepository.GetById(topicId);
            if (topic == null)
            {
                return NotFound();
            }

            _topicRepository.Delete(topicId);
            return NoContent();
        }
    }
}
