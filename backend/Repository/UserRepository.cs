using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task AddUserAsync(User user)
        {
            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
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