using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProfessionalCommunicationService;

namespace CommunicationService;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", builder => builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

        services.AddAuthentication("MyCookieAuthentication").AddCookie("MyCookieAuthentication", options =>
        {
            options.LoginPath = "/api/users/signin"; // Путь к вашему методу аутентификации
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Время жизни cookie
            options.SlidingExpiration = true; // Обновление времени жизни cookie при активности
        });
        
        // Добавление Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Communication Service API", Version = "v1" });
        });
        
        // Настройка контекста базы данных с использованием строки подключения
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        // Регистрация репозиториев
        services.AddScoped<UserRepository>();
        services.AddScoped<MessageRepository>();
        services.AddScoped<TopicRepository>();
        services.AddScoped<PostRepository>();
        services.AddScoped<CommentRepository>();

        // Регистрация сервисов
        services.AddScoped<UserService>();
        services.AddScoped<MessageService>();
        services.AddScoped<TopicService>();
        services.AddScoped<PostService>();
        services.AddScoped<CommentService>();

        // Добавление контроллеров
        services.AddRouting();
        services.AddControllers(); // Раскомментируйте, если используете контроллеры
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage(); // Отображение страницы ошибок в режиме разработки
        }
        else
        {
            app.UseExceptionHandler("/Home/Error"); // Обработка ошибок в продакшене
            app.UseHsts(); // Использование HSTS
        }

        app.UseHttpsRedirection(); // Перенаправление HTTP на HTTPS
        app.UseStaticFiles(); // Использование статических файлов

        app.UseRouting(); // Включение маршрутизации

        app.UseCors("AllowOrigin"); // Используйте CORS-политику
        // app.UseCors("Access-Control-Allow-Origin"); // Используйте CORS-политику
        // app.UseCors("Access-Control-Allow-Methods"); // Используйте CORS-политику

        app.UseAuthorization(); // Включение авторизации
        app.UseAuthentication(); // Включение аутентификации

        // Включение Swagger
        app.UseSwagger();

        // Включение Swagger UI
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Communication Service API V1");
            c.RoutePrefix = "swagger"; // Чтобы Swagger UI был доступен по корневому адресу
        });
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Маршрутизация контроллеров
            // Можно добавить другие маршруты, если нужно
        });
        
    }
}