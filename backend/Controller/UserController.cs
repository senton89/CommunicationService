using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CommunicationService;
using CommunicationService.DTO;
using Microsoft.AspNetCore.Authentication;

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
        
        // Получение всех пользователей
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // Получение пользователя по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        
        // Получение пользователя по Username
        [HttpGet("u={username}")]
        public async Task<ActionResult<User>> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null) return NotFound();
            return Ok(user);
        }
        
        // Получение пользователя по Email
        [HttpGet("e={email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // Регистрация пользователя
        [HttpPost("signup")]
        public async Task<ActionResult> RegisterUser(UserDTO userDto)
        {
            if(userDto.email == null)
                return Conflict("Email is required.");
            if(userDto.password == null)
                return Conflict("Password is required.");
            
            var registrationStatus = await _userService.RegisterUserAsync(userDto);
            switch (registrationStatus)
            {
                case RegistrationStatus.Success:
                    // Если регистрация успешна, автоматически аутентифицируем пользователя
                    return await AuthentificateUser(userDto);
                case RegistrationStatus.UsernameExists:
                    return Conflict("Username already exists.");
                case RegistrationStatus.EmailExists:
                    return Conflict("Email already exists.");
                case RegistrationStatus.Error:
                default:
                    return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        
        // Авторизация пользователя
        [HttpPost("signin")]
        public async Task<ActionResult> AuthentificateUser (UserDTO userDto)
        {
            var resultUser = await _userService.AuthentificateUserAsync(userDto.username, userDto.password);
            
            if (resultUser!=null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userDto.username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuthentication");
        
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                await HttpContext.SignInAsync("MyCookieAuthentication", new ClaimsPrincipal(claimsIdentity), authProperties);

                return Ok(resultUser);
            }
            return Unauthorized();
        }

        // Обновление информации о пользователе
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserDTO userDto)
        { 
            await _userService.UpdateUserAsync(userDto, id);
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
        public async Task<ActionResult<Message>> SendMessage(MessageDTO messageDto)
        {
            var sentMessage = await _userService.SendMessageAsync(messageDto);
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
        
    }
}