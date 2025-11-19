using System.Collections.Generic;

namespace HeyDEAN_API.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<VoiceLog> VoiceLogs { get; set; } = new List<VoiceLog>();
    }
}
