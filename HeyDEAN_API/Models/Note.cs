using System.ComponentModel.DataAnnotations.Schema;

public class Note
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    // Tilføj UserId FK funktionalitet
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
}