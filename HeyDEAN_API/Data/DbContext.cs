using Microsoft.EntityFrameworkCore;
using HeyDEAN_API.Models;

namespace HeyDEAN_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users {get; set;}
        public DbSet<Note> Notes {get; set;}
        public DbSet<TaskItem> Tasks {get; set;}
        public DbSet<Event> Events {get; set;}
        public DbSet<VoiceLog> VoiceLogs {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.UserId);
                b.HasIndex(u => u.Username).IsUnique();
                b.Property(u => u.CreatedAt).HasColumnType("DATETIME(6)");
            });

            modelBuilder.Entity<TaskItem>(b =>
            {
                b.HasKey(t => t.TaskId);
                b.HasOne(t => t.User)
                    .WithMany(u => u.Tasks)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                b.Property(t => t.CreatedAt).HasColumnType("DATETIME(6)");
            });

            modelBuilder.Entity<Note>(b =>
            {
                b.HasKey(n => n.NoteId);
                b.HasOne(n => n.User)
                    .WithMany(u => u.Notes)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                b.Property(n => n.CreatedAt).HasColumnType("DATETIME(6)");
            });

            modelBuilder.Entity<Event>(b =>
            {
                b.HasKey(e => e.EventId);
                b.HasOne(e => e.User)
                    .WithMany(u => u.Events)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                b.Property(e => e.CreatedAt).HasColumnType("DATETIME(6)");
            });

            modelBuilder.Entity<VoiceLog>(b =>
            {
                b.HasKey(v => v.VoiceLogId);
                b.HasOne(v => v.User)
                    .WithMany(u => u.VoiceLogs)
                    .HasForeignKey(v => v.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
                b.Property(v => v.CreatedAt).HasColumnType("DATETIME(6)");
            });
        }
    }
}