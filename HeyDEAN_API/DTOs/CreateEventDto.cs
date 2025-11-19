namespace HeyDEAN_API.DTOs
{
    public class CreateEventDto
    {
        public string Title { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
    }
}