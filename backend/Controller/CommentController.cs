using Microsoft.AspNetCore.Mvc;

namespace ProfessionalCommunicationService
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }
    }
}