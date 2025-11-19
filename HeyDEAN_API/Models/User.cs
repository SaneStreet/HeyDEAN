namespace HeyDEAN_API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Username { get; set; } = string.Empty;
        public string? PasswordHashed { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Interface collections
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<VoiceLog> VoiceLogs { get; set; } = new List<VoiceLog>();

    }
}