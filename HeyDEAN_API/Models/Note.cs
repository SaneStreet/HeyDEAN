namespace HeyDEAN_API.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Note
    {
        public int NoteId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Tilføj UserId FK funktionalitet
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}