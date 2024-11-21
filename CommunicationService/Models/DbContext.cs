using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ProfessionalCommunicationService;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Message> messages { get; set; }
    public DbSet<Topic> topics { get; set; }
    public DbSet<Post> posts { get; set; }
    public DbSet<Comment> comments { get; set; }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     // optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
    //     optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    // }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}