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
    }
}