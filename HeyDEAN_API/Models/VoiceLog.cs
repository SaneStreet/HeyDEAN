namespace HeyDEAN_API.Models
{
    public class VoiceLog
    {
        public int VoiceLogId { get; set; }
        public Guid UserId { get; set; }

        public string? Transcript { get; set; } = string.Empty;
        public string? Intent { get; set; }
        public string? Response { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        // Tilføj UserId FK funktionalitet
        public User? User { get; set; }
}   
}