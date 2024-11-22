using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{senderId}/{receiverId}")]
        public async Task<ActionResult<List<Message>>> GetMessages(int senderId, int receiverId)
        {
            var messages = await _messageService.GetMessagesAsync(senderId, receiverId);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(Message message)
        {
            await _messageService.SendMessageAsync(message);
            return CreatedAtAction(nameof(GetMessages), new { senderId = message.sender_id, receiverId = message.receiver_id }, message);
        }

        // Другие методы для работы с сообщениями, например, для удаления
    }
}