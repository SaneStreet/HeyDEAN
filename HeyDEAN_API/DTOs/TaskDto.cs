namespace HeyDEAN_API.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } // bruges ikke -> null
        public string? Meta { get; set; } // fx "Due 2025-01-11"
    }
}