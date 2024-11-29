using CommunicationService;
using CommunicationService.DTO;
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
        
        [HttpGet("t={title}")]
        public async Task<ActionResult<Topic>> GetTopicByTitle(string title)
        {
            var topic = await _topicService.GetTopicByTitleAsync(title);
            if (topic == null) return NotFound();
            return Ok(topic);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTopic(TopicDTO topicDto)
        {
            var responseStatus = await _topicService.CreateTopicAsync(topicDto);
            switch (responseStatus)
            {
                case ResponseStatus.Success:
                    return CreatedAtAction(nameof(GetTopicById), new { id = topicDto.id },await _topicService.GetTopicByTitleAsync(topicDto.title));
                case ResponseStatus.Exists:
                    return Conflict("Topic with this title already exists.");
                case ResponseStatus.Error:
                default:
                    return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTopic(int id, TopicDTO topicDto)
        {
            var responseStatus = await _topicService.UpdateTopicAsync(topicDto,id);
            switch (responseStatus)
            {
                case ResponseStatus.Success:
                    return Ok(await _topicService.GetTopicByIdAsync(id));
                case ResponseStatus.NotFound:
                    return NotFound();
                case ResponseStatus.Error:
                default:
                    return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTopic(int id)
        {
            var responseStatus = await _topicService.DeleteTopicAsync(id);
            switch (responseStatus)
            {
                case ResponseStatus.Success:
                    return NoContent();
                case ResponseStatus.Error:
                    return StatusCode(404, "The topic could not be found.");
                default:
                    return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}