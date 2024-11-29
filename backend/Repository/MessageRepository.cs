using CommunicationService;
using CommunicationService.DTO;
using Microsoft.EntityFrameworkCore;

namespace ProfessionalCommunicationService;

public class MessageRepository
{
    private readonly ApplicationDbContext _context;

    public MessageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Message>> GetMessagesAsync(int senderId, int receiverId)
    {
        return await _context.messages
            .Where(m => (m.sender_id == senderId && m.receiver_id == receiverId) ||
                        (m.sender_id == receiverId && m.receiver_id == senderId))
            .ToListAsync();
    }

    public async Task<ResponseStatus> AddMessageAsync(Message message)
    {
        try
        {
            message.sent_at = DateTime.Now.ToUniversalTime();
            await _context.messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return ResponseStatus.Success; // Успешное добавление сообщения
        }
        catch
        {
            return ResponseStatus.Error;
        }
    }

    public async Task<Message> GetMessageByIdAsync(int id)
    {
        // Найти сообщение по идентификатору
        return await _context.messages.FindAsync(id);
    }

    public async Task<ResponseStatus> UpdateMessageAsync(MessageDTO updatedMessage,int id)
    {
        // Найти сообщение по идентификатору
        var existingMessage = await _context.messages.FindAsync(id);
        if (existingMessage == null)
        {
            return ResponseStatus.NotFound; // Сообщение не найдено
        }

        // Обновить свойства существующего сообщения
        existingMessage.content = updatedMessage.content;
        existingMessage.sent_at = existingMessage.sent_at.ToUniversalTime();

        // Сохранить изменения в базе данных
        await _context.SaveChangesAsync();
        return ResponseStatus.Success; // Успешное обновление
    }

    public async Task<ResponseStatus> DeleteMessageAsync(int id)
    {
        // Найти сообщение по идентификатору
        var message = await _context.messages.FindAsync(id);

        if (message != null)
        {
            // Удалить сообщение из контекста
            _context.messages.Remove(message);
            await _context.SaveChangesAsync(); // Сохранить изменения в базе данных
            return ResponseStatus.Success; // Успешное удаление
        }
        else
        {
            return ResponseStatus.NotFound; // Сообщение не найдено
        }
    }
}