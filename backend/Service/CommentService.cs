namespace ProfessionalCommunicationService;

public class CommentService
{
    private readonly CommentRepository _commentRepository;

    public CommentService(CommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return await _commentRepository.GetCommentsByPostIdAsync(postId);
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        return await _commentRepository.GetCommentByIdAsync(id);
    }
    
    public async Task CreateCommentAsync(Comment comment)
    {
        // Здесь можно добавить логику для проверки валидности комментария
        await _commentRepository.AddCommentAsync(comment);
    }
    
    public async Task UpdateCommentAsync(Comment comment)
    {
        await _commentRepository.UpdateCommentAsync(comment);
    }

    public async Task DeleteCommentAsync(int id)
    {
        await _commentRepository.DeleteCommentAsync(id);
    }
}