namespace HeyDEAN_API.Models
{
    public class Event
    {
        public int EventId {get; set;}
        public Guid UserId { get; set; }

        public string? Title { get; set; }
        public DateOnly? Date {get; set;}
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        // Tilføj UserId FK funktionalitet
        public User? User { get; set; }
    }
}