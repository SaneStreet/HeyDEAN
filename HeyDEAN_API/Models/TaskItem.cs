namespace HeyDEAN_API.Models
{
    public class TaskItem
    {
        public int TaskId {get;set;}
        public Guid UserId { get; set; }

        public string? Title {get; set;}
        public bool IsCompleted {get; set;}
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate {get; set;}

        // Tilføj UserId FK funktionalitet
        public User? User { get; set; }
    }
}