using HeyDEAN_API.Models;
using Microsoft.EntityFrameworkCore;

namespace HeyDEAN_API.Data
{
    public class Seeder
    {
        private readonly AppDbContext _context;

        public Seeder(AppDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Migrate if needed
            _context.Database.Migrate();

            if (_context.Users.Any())
                return; // If already seeded return

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = "tester",
                PasswordHash = "tester-pass-not-hashed",
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            var note = new Note
            {
                UserId = user.UserId,
                Content = "Just a test Note.",
                CreatedAt = DateTime.UtcNow
            };

            var task = new TaskItem
            {
                UserId = user.UserId,
                Title = "Just a test Task",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(2)
            };

            var voicelog = new VoiceLog
            {
                UserId = user.UserId,
                Transcript = "This is a Voicelog test",
                CreatedAt = DateTime.UtcNow
            };

            var calendarEvent = new Event
            {
                UserId = user.UserId,
                Title = "This is an Event test",
                Date = DateOnly.FromDateTime(DateTime.Now),
                StartTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(2)),
                EndTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(3))
            };

            _context.Users.Add(user);
            _context.Notes.Add(note);
            _context.Tasks.Add(task);
            _context.VoiceLogs.Add(voicelog);
            _context.Events.Add(calendarEvent);

            _context.SaveChanges();
        }
    }
}
