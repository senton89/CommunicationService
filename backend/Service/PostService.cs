namespace ProfessionalCommunicationService;

public class PostService
{
    private readonly PostRepository _postRepository;

    public PostService(PostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<List<Post>> GetPostsByTopicIdAsync(int topicId)
    {
        return await _postRepository.GetPostsByTopicIdAsync(topicId);
    }
    public async Task<List<Post>> GetAllPostsAsync()
    {
        return await _postRepository.GetAllPostsAsync();
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        return await _postRepository.GetPostByIdAsync(id);
    }

    public async Task CreatePostAsync(Post post)
    {
        // Здесь можно добавить логику для проверки валидности поста
        await _postRepository.AddPostAsync(post);
    }

    public async Task UpdatePostAsync(Post post)
    {
        await _postRepository.UpdatePostAsync(post);
    }

    public async Task DeletePostAsync(int id)
    {
        await _postRepository.DeletePostAsync(id);
    }

    // Методы для работы с комментариями
    public async Task CreateCommentAsync(Comment comment)
    {
        await _postRepository.AddCommentAsync(comment);
    }

    public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return await _postRepository.GetCommentsByPostIdAsync(postId);
    }

    public async Task<Comment> GetCommentByIdAsync(int postId, int id)
    {
        return await _postRepository.GetCommentByIdAsync(postId, id);
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        await _postRepository.UpdateCommentAsync(comment);
    }

    public async Task DeleteCommentAsync(int postId, int id)
    {
        await _postRepository.DeleteCommentAsync(postId, id);
    }
}