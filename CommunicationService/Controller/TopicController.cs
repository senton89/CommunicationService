using Microsoft.AspNetCore.Mvc;

namespace ProfessionalCommunicationService
{
    [ApiController]
    [Route("api/topics")]
    public class TopicController : ControllerBase
    {
        private readonly TopicService _topicService;

        public TopicController(TopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Topic>>> GetAllTopics()
        {
            var topics = await _topicService.GetAllTopicsAsync();
            return Ok(topics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> GetTopicById(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null) return NotFound();
            return Ok(topic);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTopic(Topic topic)
        {
            await _topicService.CreateTopicAsync(topic);
            return CreatedAtAction(nameof(GetTopicById), new { id = topic.id }, topic);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTopic(int id, Topic topic)
        {
            if (id != topic.id) return BadRequest();
            await _topicService.UpdateTopicAsync(topic);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTopic(int id)
        {
            await _topicService.DeleteTopicAsync(id);
            return NoContent();
        }
    }
}