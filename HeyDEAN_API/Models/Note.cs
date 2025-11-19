namespace HeyDEAN_API.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public Guid UserId { get; set; }

        public string Content { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        // Tilføj UserId FK funktionalitet
        public User? User { get; set; }
    }
}