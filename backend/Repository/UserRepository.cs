using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommunicationService.DTO;

namespace ProfessionalCommunicationService
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.users.ToListAsync();
        }

        // Получение пользователя по ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.users.FindAsync(id);
        }

        // Получение пользователя по имени пользователя
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.username == username);
        }

        // Добавление нового пользователя
        public async Task AddUserAsync(UserDTO userDto)
        {
            using (var hmac = new HMACSHA256())
            {
                userDto.created_at = DateTime.Now;
                userDto.updated_at = DateTime.Now;
                var hashedPassword = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.password)));
                var salt = hmac.Key;
                var user = new User
                {
                    username = userDto.username,
                    email = userDto.email,
                    password_hash = hashedPassword,
                    created_at = userDto.created_at,
                    updated_at = userDto.updated_at,
                    salt = salt,
                };
                await _context.users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AuthentificateUserAsync(UserDTO userDto)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.username == userDto.username);
            if (user != null)
            {
                return VerifyPassword(userDto.password, user.password_hash, user.salt);
            }
            return false;
        }


        private static bool VerifyPassword(string password, string storedHash, byte[] salt)
        {
            using (var hmac = new HMACSHA256(salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return CompareByteArrays(computedHash, Convert.FromBase64String(storedHash));
            }
        }
        
        private static bool CompareByteArrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }

        // Обновление информации о пользователе
        public async Task UpdateUserAsync(User user)
        {
            _context.users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Удаление пользователя
        public async Task DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                _context.users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Проверка существования пользователя
        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.users.AnyAsync(u => u.id == id);
        }

        // Отправка сообщения
        public async Task<Message> AddMessageAsync(Message message)
        {
            await _context.messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return message;
        }

        // Получение всех сообщений пользователя
        public async Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId)
        {
            return await _context.messages.Where(m => m.sender_id == userId || m.receiver_id == userId).ToListAsync();
        }

        // Получение сообщения по ID
        public async Task<Message> GetMessageByIdAsync(int id)
        {
            return await _context.messages.FindAsync(id);
        }

        // Создание поста
        public async Task<Post> AddPostAsync(Post post)
        {
            await _context.posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        // Получение всех постов
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.posts.ToListAsync();
        }

        // Получение поста по ID
        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.posts.FindAsync(id);
        }

        // Добавление комментария к посту
        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            await _context.comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        // Получение комментариев к посту
        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.comments.Where(c => c.post_id == postId).ToListAsync();
        }

        // Получение комментария по ID
        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _context.comments.FindAsync(id);
        }
    }
}