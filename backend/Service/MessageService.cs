using CommunicationService;
using CommunicationService.DTO;

namespace ProfessionalCommunicationService;

public class MessageService
{
    private readonly MessageRepository _messageRepository;

    public MessageService(MessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<List<Message>> GetMessagesByUserIdAsync(int userId)
    {
        return await _messageRepository.GetMessagesByUserIdAsync(userId);
    }
 public async Task<List<Message>> GetMessagesAsync(int senderId, int receiverId)
    {
        return await _messageRepository.GetMessagesAsync(senderId, receiverId);
    }

    public async Task<ResponseStatus> SendMessageAsync(Message message)
    {
        return await _messageRepository.AddMessageAsync(message);
    }
    public async Task<ResponseStatus> UpdateMessageAsync(MessageDTO updatedMessage,int id)
    {
        return await _messageRepository.UpdateMessageAsync(updatedMessage,id);
    }

    public async Task<ResponseStatus> DeleteMessageAsync(int id)
    {
        return await _messageRepository.DeleteMessageAsync(id);
    }
}