using Microsoft.EntityFrameworkCore;

namespace ProfessionalCommunicationService;

public class CommentRepository

{

    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return await _context.comments
            .Where(c => c.post_id == postId)
            .ToListAsync();
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        return await _context.comments.FindAsync(id);
    }

    public async Task AddCommentAsync(Comment comment)
    {
        await _context.comments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        _context.comments.Update(comment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int id)
    {
        var comment = await GetCommentByIdAsync(id);
        if (comment != null)
        {
            _context.comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}

