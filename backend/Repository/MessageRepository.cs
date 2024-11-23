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

    public async Task AddMessageAsync(Message message)
    {
        await _context.messages.AddAsync(message);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteMessageAsync(int id)
    {
        // Найти сообщение по идентификатору
        var message = await _context.messages.FindAsync(id);

        if (message != null)
        {
            // Удалить сообщение из контекста
            _context.messages.Remove(message);
            await _context.SaveChangesAsync(); // Сохранить изменения в базе данных
            return true;
        }
        else
        {
            return false;
        }
    }
}