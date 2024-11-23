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
            await _postService.CreatePostAsync(post);
            return CreatedAtAction(nameof(GetPostById), new { id = post.id }, post);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(int id, Post post)
        {
            if (id != post.id) return BadRequest();
            await _postService.UpdatePostAsync(post);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }
        
        [HttpPost("{postId}/comments")]
        public async Task<ActionResult> CreateComment(int postId, Comment comment)
        {
            comment.post_id = postId; // Установим идентификатор поста для комментария
            await _postService.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { postId = postId, id = comment.id }, comment);
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
        public async Task<ActionResult> UpdateComment(int postId, int id, Comment comment)
        {
            if (id != comment.id) return BadRequest();
            comment.post_id = postId; // Обновляем идентификатор поста
            await _postService.UpdateCommentAsync(comment);
            return NoContent();
        }

        [HttpDelete("{postId}/comments/{id}")]
        public async Task<ActionResult> DeleteComment(int postId, int id)
        {
            await _postService.DeleteCommentAsync(postId, id);
            return NoContent();
        }
    }
}