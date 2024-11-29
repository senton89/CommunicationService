using CommunicationService;
using CommunicationService.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<Topic> GetTopicByTitleAsync(string title)
    {
        return await _context.topics.FirstOrDefaultAsync(x => x.title == title);
    }

    public async Task<ResponseStatus> AddTopicAsync(TopicDTO topicDto)
    {
        var existingTopicName = await _context.topics
            .AnyAsync(t => t.title == topicDto.title);
        if (existingTopicName)
        {
            return ResponseStatus.Exists;
        }
        try
        {
            var topic = new Topic
            {
                title = topicDto.title,
                author_id = topicDto.author_id,
                created_at = DateTime.Now.ToUniversalTime(),
                updated_at = DateTime.Now.ToUniversalTime()
            };
            await _context.topics.AddAsync(topic);
            await _context.SaveChangesAsync();
        }
        catch 
        {
            return ResponseStatus.Error;
        }
        return ResponseStatus.Success;
    }

    public async Task<ResponseStatus> UpdateTopicAsync(TopicDTO topicDto,int id)
    {
        try
        {
            var existingTopic = await _context.topics.FindAsync(id);
            if (existingTopic == null)
            {
                return ResponseStatus.NotFound;
            }

            existingTopic.title = topicDto.title;
            existingTopic.author_id = topicDto.author_id;
            existingTopic.updated_at = DateTime.Now.ToUniversalTime();
            // _context.topics.Update(existingTopic);
            await _context.SaveChangesAsync();
        }
        catch 
        {
            return ResponseStatus.Error;
        }
        return ResponseStatus.Success;
    }

    public async Task<ResponseStatus> DeleteTopicAsync(int id)
    {
        var topic = await GetTopicByIdAsync(id);
        if (topic != null)
        {
            _context.topics.Remove(topic);
            await _context.SaveChangesAsync();
            return ResponseStatus.Success;
        }
        return ResponseStatus.Error;
    }
}

