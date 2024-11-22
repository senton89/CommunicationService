using Microsoft.EntityFrameworkCore;

namespace ProfessionalCommunicationService;

public class TopicRepository
{

    private readonly ApplicationDbContext _context;

    public TopicRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Topic>> GetAllTopicsAsync()
    {
        return await _context.topics.ToListAsync();
    }

    public async Task<Topic> GetTopicByIdAsync(int id)
    {
        return await _context.topics.FindAsync(id);
    }

    public async Task AddTopicAsync(Topic topic)
    {
        await _context.topics.AddAsync(topic);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTopicAsync(Topic topic)
    {
        _context.topics.Update(topic);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTopicAsync(int id)
    {
        var topic = await GetTopicByIdAsync(id);
        if (topic != null)
        {
            _context.topics.Remove(topic);
            await _context.SaveChangesAsync();
        }
    }
}

