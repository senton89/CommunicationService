using CommunicationService;
using CommunicationService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ProfessionalCommunicationService
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("topic/{topicId}")]
        public async Task<ActionResult<List<Post>>> GetPostsByTopicId(int topicId)
        {
            var posts = await _postService.GetPostsByTopicIdAsync(topicId);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePost(Post post)
        {
            var responseStatus = await _postService.CreatePostAsync(post);
            switch (responseStatus)
            {
                case ResponseStatus.Success:
                    return CreatedAtAction(nameof(GetPostById), new { id = post.id }, post);
                case ResponseStatus.Error:
                default:
                    return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(int id, PostDTO postDto)
        {
            var responseStatus = await _postService.UpdatePostAsync(postDto,id);
            switch (responseStatus)
            {
                case ResponseStatus.Success:
                    return NoContent();
                case ResponseStatus.NotFound:
                    return NotFound("Post not found.");
                case ResponseStatus.Error:
                default:
                    return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var responseStatus = await _postService.DeletePostAsync(id);
            switch (responseStatus)
            {
                case ResponseStatus.Success:
                    return NoContent();
                case ResponseStatus.NotFound:
                    return NotFound("Post not found.");
                case ResponseStatus.Error:
                default:
                    return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        
        [HttpPost("{postId}/comments")]
        public async Task<ActionResult> CreateComment(int postId, Comment comment)
        {
            comment.post_id = postId; // Установим идентификатор поста для комментария
            var responseStatus = await _postService.CreateCommentAsync(comment);
            switch (responseStatus)
            {
                case ResponseStatus.Success:
                    return CreatedAtAction(nameof(GetCommentById), new { postId = postId, id = comment.id }, comment);
                case ResponseStatus.Exists:
                    return Conflict("Comment with this ID already exists.");
                case ResponseStatus.Error:
                default:
                    return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{postId}/comments")]
        public async Task<ActionResult<List<Comment>>> GetCommentsByPostId(int postId)
        {
            var comments = await _postService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        [HttpGet("{postId}/comments/{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int postId, int id)
        {
            var comment = await _postService.GetCommentByIdAsync(postId, id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpPut("{postId}/comments/{id}")]
        public async Task<ActionResult> UpdateComment(int postId, int id, CommentDTO commentDto)
        {
            commentDto.post_id = postId; // Обновляем идентификатор поста
            var responseStatus = await _postService.UpdateCommentAsync(commentDto,id);
            switch (responseStatus)
            {
                case ResponseStatus.Success:
                    return NoContent();
                case ResponseStatus.NotFound:
                    return NotFound("Comment not found.");
                case ResponseStatus.Error:
                default:
                    return StatusCode (500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{postId}/comments/{id}")]
        public async Task<ActionResult> DeleteComment(int postId, int id)
        {
            var responseStatus = await _postService.DeleteCommentAsync(postId, id);
            switch (responseStatus)
            {
                case ResponseStatus.Success:
                    return NoContent();
                case ResponseStatus.NotFound:
                    return NotFound("Comment not found.");
                case ResponseStatus.Error:
                default:
                    return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}