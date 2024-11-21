using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfessionalCommunicationService
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // Получение пользователя по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // Получение всех пользователей
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // Регистрация пользователя
        [HttpPost]
        public async Task<ActionResult> RegisterUser(User user)
        {
            await _userService.RegisterUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.id }, user);
        }

        // Обновление информации о пользователе
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            if (id != user.id) return BadRequest();
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        // Удаление пользователя
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var userExists = await _userService.UserExistsAsync(id);
            if (!userExists) return NotFound();
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        // Отправка сообщения
        [HttpPost("messages")]
        public async Task<ActionResult<Message>> SendMessage(Message message)
        {
            var sentMessage = await _userService.SendMessageAsync(message);
            return CreatedAtAction(nameof(GetMessageById), new { id = sentMessage.id }, sentMessage);
        }

        // Получение всех сообщений пользователя
        [HttpGet("{id}/messages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesByUserId(int id)
        {
            var messages = await _userService.GetMessagesByUserIdAsync(id);
            return Ok(messages);
        }

        // Получение сообщения по ID
        [HttpGet("messages/{id}")]
        public async Task<ActionResult<Message>> GetMessageById(int id)
        {
            var message = await _userService.GetMessageByIdAsync(id);
            if (message == null) return NotFound();
            return Ok(message);
        }

        // Создание поста
        [HttpPost("posts")]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            var createdPost = await _userService.CreatePostAsync(post);
            return CreatedAtAction(nameof(GetPostById), new { id = createdPost.id }, createdPost);
        }

        // Получение всех постов
        [HttpGet("posts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            var posts = await _userService.GetAllPostsAsync();
            return Ok(posts);
        }

        // Получение поста по ID
        [HttpGet("posts/{id}")]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var post = await _userService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        // Добавление комментария к посту
        [HttpPost("posts/{postId}/comments")]
        public async Task<ActionResult<Comment>> AddComment(int postId, Comment comment)
        {
            comment.post_id = postId; // Установка PostId для комментария
            var createdComment = await _userService.AddCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = createdComment.id }, createdComment);
        }

        // Получение комментариев к посту
        [HttpGet("posts/{postId}/comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByPostId(int postId)
        {
            var comments = await _userService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        // Получение комментария по ID
        [HttpGet("comments/{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            var comment = await _userService.GetCommentByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }
    }
}