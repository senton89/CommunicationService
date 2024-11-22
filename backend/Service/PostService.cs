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
}