public class VoiceLog
{
    public int VoiceLogId { get; set; }
    public string? Transcript { get; set; }
    public string? Intent { get; set; }
    public string? Response { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    // Tilføj UserId FK funktionalitet
}