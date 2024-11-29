using CommunicationService;
using CommunicationService.DTO;

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

    public async Task<ResponseStatus> CreatePostAsync(Post post)
    {
        // Здесь можно добавить логику для проверки валидности поста
        return await _postRepository.AddPostAsync(post);
    }

    public async Task<ResponseStatus> UpdatePostAsync(PostDTO postDto,int id)
    {
        // Здесь можно добавить логику для проверки валидности поста перед обновлением
        return await _postRepository.UpdatePostAsync(postDto,id);
    }

    public async Task<ResponseStatus> DeletePostAsync(int id)
    {
        return await _postRepository.DeletePostAsync(id);
    }

    // Методы для работы с комментариями
    public async Task<ResponseStatus> CreateCommentAsync(Comment comment)
    {
        // Здесь можно добавить логику для проверки валидности комментария
        return await _postRepository.AddCommentAsync(comment);
    }

    public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return await _postRepository.GetCommentsByPostIdAsync(postId);
    }

    public async Task<Comment> GetCommentByIdAsync(int postId, int id)
    {
        return await _postRepository.GetCommentByIdAsync(postId, id);
    }

    public async Task<ResponseStatus> UpdateCommentAsync(CommentDTO commentDto,int id)
    {
        // Здесь можно добавить логику для проверки валидности комментария перед обновлением
        return await _postRepository.UpdateCommentAsync(commentDto,id);
    }

    public async Task<ResponseStatus> DeleteCommentAsync(int postId, int id)
    {
        return await _postRepository.DeleteCommentAsync(postId, id);
    }
}