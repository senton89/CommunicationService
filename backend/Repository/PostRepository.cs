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

    public async Task<List<Post>> GetAllPostsAsync()
    {
        return await _context.posts.ToListAsync();
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

    // Методы для работы с комментариями
    public async Task AddCommentAsync(Comment comment)
    {
        await _context.comments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return await _context.comments
            .Where(c => c.post_id == postId)
            .ToListAsync();
    }

    public async Task<Comment> GetCommentByIdAsync(int postId, int id)
    {
        return await _context.comments
            .FirstOrDefaultAsync(c => c.post_id == postId && c.id == id);
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        _context.comments.Update(comment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int postId, int id)
    {
        var comment = await GetCommentByIdAsync(postId, id);
        if (comment != null)
        {
            _context.comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}