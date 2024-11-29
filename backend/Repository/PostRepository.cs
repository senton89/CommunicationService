using CommunicationService;
using CommunicationService.DTO;
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

    public async Task<ResponseStatus> AddPostAsync(Post post)
    {
        try
        {
            await _context.posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return ResponseStatus.Success;
        }
        catch
        {
            return ResponseStatus.Error;
        }
    }

    public async Task<ResponseStatus> UpdatePostAsync(PostDTO postDto,int id)
    {
        try
        {
            var existingPost = await GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return ResponseStatus.NotFound;
            }
            existingPost.content = postDto.content;
            existingPost.updated_at = DateTime.Now.ToUniversalTime();

            _context.posts.Update(existingPost);
            await _context.SaveChangesAsync();
            return ResponseStatus.Success;
        }
        catch
        {
            return ResponseStatus.Error;
        }
    }

    public async Task<ResponseStatus> DeletePostAsync(int id)
    {
        var post = await GetPostByIdAsync(id);
        if (post != null)
        {
            _context.posts.Remove(post);
            await _context.SaveChangesAsync();
            return ResponseStatus.Success;
        }
        return ResponseStatus.NotFound;
    }

    // Методы для работы с комментариями
    public async Task<ResponseStatus> AddCommentAsync(Comment comment)
    {
        try
        {
            comment.created_at = DateTime.Now.ToUniversalTime();
            comment.updated_at = DateTime.Now.ToUniversalTime();
            await _context.comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return ResponseStatus.Success;
        }
        catch
        {
            return ResponseStatus.Error;
        }
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

    public async Task<ResponseStatus> UpdateCommentAsync(CommentDTO commentDto, int id)
    {
        try
        {
            var existingComment = await GetCommentByIdAsync(commentDto.post_id, id);
            if (existingComment == null)
            {
                return ResponseStatus.NotFound;
            }

            existingComment.content = commentDto.content;
            existingComment.updated_at = DateTime.Now.ToUniversalTime();
            existingComment.created_at = existingComment.created_at.ToUniversalTime();
            
            _context.comments.Update(existingComment);
            await _context.SaveChangesAsync();
            return ResponseStatus.Success;
        }
        catch
        {
            return ResponseStatus.Error;
        }
    }

    public async Task<ResponseStatus> DeleteCommentAsync(int postId, int id)
    {
        var comment = await GetCommentByIdAsync(postId, id);
        if (comment != null)
        {
            _context.comments.Remove(comment);
            await _context.SaveChangesAsync();
            return ResponseStatus.Success;
        }
        return ResponseStatus.NotFound;
    }
}