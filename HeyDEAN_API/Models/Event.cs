namespace HeyDEAN_API.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Event
    {
        public int EventId {get; set;}
        public string? Title { get; set; }
        public DateTime? Date {get; set;}
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedAt {get; set;}

        // Tilføj UserId FK funktionalitet
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}