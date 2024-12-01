using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommunicationService;
using CommunicationService.DTO;

namespace ProfessionalCommunicationService
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Получение пользователя по ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        // Получение пользователя по имени пользователя
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }
        
        // Получение пользователя по почте пользователя
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        // Регистрация пользователя
        public async Task<RegistrationStatus> RegisterUserAsync(UserDTO userDto)
        {
            return await _userRepository.AddUserAsync(userDto);
        }
        
        public async Task<User?> AuthentificateUserAsync(string username, string password)
        {
            return await _userRepository.AuthentificateUserAsync(username, password);
        }

        // Получение всех пользователей
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        // Обновление информации о пользователе
        public async Task UpdateUserAsync(UserDTO userDto, int id)
        {
            await _userRepository.UpdateUserAsync(userDto, id);
        }

        // Удаление пользователя
        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        // Проверка существования пользователя
        public async Task<bool> UserExistsAsync(int id)
        {
            return await _userRepository.UserExistsAsync(id);
        }

        // Отправка сообщения
        public async Task<Message> SendMessageAsync(MessageDTO messageDto)
        {
            return await _userRepository.AddMessageAsync(messageDto);
        }

        // Получение всех сообщений пользователя
        public async Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId)
        {
            return await _userRepository.GetMessagesByUserIdAsync(userId);
        }

        // Получение сообщения по ID
        public async Task<Message> GetMessageByIdAsync(int id)
        {
            return await _userRepository.GetMessageByIdAsync(id);
        }

        // Создание поста
        public async Task<Post> CreatePostAsync(Post post)
        {
            return await _userRepository.AddPostAsync(post);
        }

        // Получение всех постов
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _userRepository.GetAllPostsAsync();
        }
    }
}