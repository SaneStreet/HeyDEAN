using HeyDEAN_API.Data;
using HeyDEAN_API.Models;
using Microsoft.AspNetCore.Identity;
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
                Username = "peter",
                PasswordHash = "parker-but-not-hashed",
                CreatedAt = DateTime.UtcNow
            };

            var note = new Note
            {
                UserId = user.UserId,
                Content = "Remember to update my calendar.",
                CreatedAt = DateTime.UtcNow
            };

            var task = new TaskItem
            {
                UserId = user.UserId,
                Title = "Make a frontend for the new project.",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(2)
            };

            var voicelog = new VoiceLog
            {
                UserId = user.UserId,
                Transcript = "Hey Dean, what are my tasks for today?",
                CreatedAt = DateTime.UtcNow
            };

            var calendarEvent = new Event
            {
                UserId = user.UserId,
                Title = "Presentation for project #10",
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
