namespace HeyDEAN_API.DTOs
{
    public class CreateTaskDto
    {
        public string? Title { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
    }
}