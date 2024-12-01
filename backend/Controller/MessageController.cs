using Microsoft.AspNetCore.Mvc;
using CommunicationService;
using CommunicationService.DTO;

namespace ProfessionalCommunicationService
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Message>>> GetMessages(int userId)
        {
            var messages = await _messageService.GetMessagesByUserIdAsync(userId);
            return Ok(messages);
        }
        
        [HttpGet("{senderId}/{receiverId}")]
        public async Task<ActionResult<List<Message>>> GetMessages(int senderId, int receiverId)
        {
            var messages = await _messageService.GetMessagesAsync(senderId, receiverId);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(Message message)
        {
            var responseStatus = await _messageService.SendMessageAsync(message);

            switch (responseStatus)
            {
                case ResponseStatus.Success:
                    return CreatedAtAction(nameof(GetMessages), new { senderId = message.sender_id, receiverId = message.receiver_id }, message);
                case ResponseStatus.Error:
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while sending the message.");
                default:
                    return BadRequest("Invalid message data.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMessage(int id, MessageDTO updatedMessage)
        {
            var responseStatus = await _messageService.UpdateMessageAsync(updatedMessage,id);

            switch (responseStatus)
            {
                case ResponseStatus.Success:
                    return NoContent(); // Успешное обновление
                case ResponseStatus.NotFound:
                    return NotFound(); // Сообщение не найдено
                case ResponseStatus.Error:
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the message.");
                default:
                    return BadRequest("Invalid message data.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var responseStatus = await _messageService.DeleteMessageAsync(id);

            switch (responseStatus)
            {
                case ResponseStatus.Success:
                    return NoContent();
                case ResponseStatus.NotFound:
                    return NotFound();
                case ResponseStatus.Error:
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the message.");
                default:
                    return BadRequest("Invalid message ID.");
            }
        }
    }
}