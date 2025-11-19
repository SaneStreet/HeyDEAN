namespace HeyDEAN_API.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class TaskItem
    {
        public int TaskId {get;set;}
        public string? Title {get; set;}
        public bool IsCompleted {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime? DueDate {get; set;}

        // Tilføj UserId FK funktionalitet
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}