using HeyDEAN_API.Data;
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
            // Migrér DB hvis nødvendigt
            _context.Database.Migrate();

            if (_context.Users.Any())
                return; // Allerede seeded

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = "sanestreet",
                PasswordHash = "fake-hash",
                CreatedAt = DateTime.UtcNow
            };

            var note = new Note
            {
                UserId = user.UserId,
                Content = "This is a test note",
                CreatedAt = DateTime.UtcNow
            };

            var task = new TaskItem
            {
                UserId = user.UserId,
                Title = "Test Task #1",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            var voicelog = new VoiceLog
            {
                UserId = user.UserId,
                Transcript = "This is a voicelog placeholder",
                CreatedAt = DateTime.UtcNow
            };

            var calendarEvent = new Event
            {
                UserId = user.UserId,
                Title = "Test Event #1",
                Date = DateOnly.FromDateTime(DateTime.Now),
                StartTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(1)),
                EndTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(2))
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
