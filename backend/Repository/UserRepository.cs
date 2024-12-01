using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommunicationService;
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
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.email == email);
        }

        // Добавление нового пользователя
        public async Task<RegistrationStatus> AddUserAsync(UserDTO userDto)
        {
            try
            {
                // Проверка на уникальность username
                var existingUserByUsername = await _context.users
                    .AnyAsync(u => u.username == userDto.username);
                if (existingUserByUsername)
                {
                    return RegistrationStatus.UsernameExists; 
                }

                // Проверка на уникальность email
                var existingUserByEmail = await _context.users
                    .AnyAsync(u => u.email == userDto.email);
                if (existingUserByEmail)
                {
                    return RegistrationStatus.EmailExists;
                }

                using (var hmac = new HMACSHA256())
                {
                    userDto.created_at = DateTime.Now.ToUniversalTime();
                    userDto.updated_at = DateTime.Now.ToUniversalTime();
                    var hashedPassword = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.password)));
                    var salt = hmac.Key;
                    var user = new User
                    {
                        username = userDto.username,
                        email = userDto.email!,
                        password_hash = hashedPassword,
                        created_at = userDto.created_at,
                        updated_at = userDto.updated_at,
                        salt = salt,
                    };
                    await _context.users.AddAsync(user);
                    await _context.SaveChangesAsync();
                }
                return RegistrationStatus.Success;
            }
            catch
            {
                return RegistrationStatus.Error;
            }
        }

        public async Task<User?> AuthentificateUserAsync(string username, string password)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.username == username);
            if (user != null)
            {
                if (VerifyPassword(password, user.password_hash, user.salt))
                {
                    return user;
                }
                return null;
            }
            return null;
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
        public async Task UpdateUserAsync(UserDTO userDto, int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
                return;
            if (userDto.email != null)
                user.email = userDto.email!;
            if (userDto.password != null)
            {
                using(var hmac = new HMACSHA256())
                {
                    user.password_hash =
                        Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.password)));
                    user.salt = hmac.Key;
                }
            }

            user.created_at = user.created_at.ToUniversalTime();
            user.updated_at = DateTime.Now.ToUniversalTime();

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
        public async Task<Message> AddMessageAsync(MessageDTO messageDto)
        {
            var message = new Message
            {
                sender_id = messageDto.sender_id,
                receiver_id = messageDto.receiver_id,
                content = messageDto.content,
                sent_at = DateTime.Now.ToUniversalTime(),
            };
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
    }
}