using Microsoft.EntityFrameworkCore;

namespace ProfessionalCommunicationService;

public class PostRepository
{

    private readonly ApplicationDbContext _context;

    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Post>> GetPostsByTopicIdAsync(int topicId)
    {
        return await _context.posts
            .Where(p => p.topic_id == topicId)
            .ToListAsync();
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        return await _context.posts.FindAsync(id);
    }

    public async Task AddPostAsync(Post post)
    {
        await _context.posts.AddAsync(post);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePostAsync(Post post)
    {
        _context.posts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePostAsync(int id)
    {
        var post = await GetPostByIdAsync(id);
        if (post != null)
        {
            _context.posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}