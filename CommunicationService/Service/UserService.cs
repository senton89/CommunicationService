using System.Collections.Generic;
using System.Threading.Tasks;

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

        // Регистрация пользователя
        public async Task RegisterUserAsync(User user)
        {
            // Здесь можно добавить логику для хеширования пароля
            await _userRepository.AddUserAsync(user);
        }

        // Получение всех пользователей
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        // Обновление информации о пользователе
        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
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
        public async Task<Message> SendMessageAsync(Message message)
        {
            return await _userRepository.AddMessageAsync(message);
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

        // Получение поста по ID
        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _userRepository.GetPostByIdAsync(id);
        }

        // Добавление комментария к посту
        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            return await _userRepository.AddCommentAsync(comment);
        }

        // Получение комментариев к посту
        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _userRepository.GetCommentsByPostIdAsync(postId);
        }

        // Получение комментария по ID
        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _userRepository.GetCommentByIdAsync(id);
        }
    }
}