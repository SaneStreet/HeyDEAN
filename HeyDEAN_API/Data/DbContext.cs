using Microsoft.EntityFrameworkCore;

namespace HeyDEAN_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users {get; set;}
        public DbSet<Note> Notes {get; set;}
        public DbSet<Task> Tasks {get; set;}
        public DbSet<Event> Events {get; set;}
        public DbSet<VoiceLog> VoiceLogs {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}