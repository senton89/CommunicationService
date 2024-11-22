namespace ProfessionalCommunicationService;

public class MessageService
{

    private readonly MessageRepository _messageRepository;

    public MessageService(MessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<List<Message>> GetMessagesAsync(int senderId, int receiverId)
    {
        return await _messageRepository.GetMessagesAsync(senderId, receiverId);
    }

    public async Task SendMessageAsync(Message message)
    {
        // Здесь можно добавить логику для проверки валидности сообщения
        await _messageRepository.AddMessageAsync(message);
    }

    // Другие методы для работы с сообщениями, например, для удаления или редактирования

}