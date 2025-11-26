namespace HeyDEAN_API.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } // bruges ikke
        public string? Meta { get; set; } // fx "12 Jan · 10:00 - 12:00"
    }
}
