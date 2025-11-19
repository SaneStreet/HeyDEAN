namespace HeyDEAN_API.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class VoiceLog
    {
        public int VoiceLogId { get; set; }
        public string? Transcript { get; set; } = string.Empty;
        public string? Intent { get; set; }
        public string? Response { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Tilføj UserId FK funktionalitet
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
}   
}