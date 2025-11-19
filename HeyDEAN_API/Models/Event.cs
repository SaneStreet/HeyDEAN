namespace HeyDEAN_API.Models
{
    public class Event
    {
        public int EventId {get; set;}
        public Guid UserId { get; set; }

        public string? Title { get; set; }
        public DateTime? Date {get; set;}
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        // Tilføj UserId FK funktionalitet
        public User? User { get; set; }
    }
}