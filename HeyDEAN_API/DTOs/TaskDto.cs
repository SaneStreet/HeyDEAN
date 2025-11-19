namespace HeyDEAN_API.DTOs
{
    public class TaskDto
    {
        public int TaskId {get; set;}
        public string? Title {get; set;}
        public bool IsCompleted {get; set;}
        public DateTime? DueDate {get; set;}
    }
}