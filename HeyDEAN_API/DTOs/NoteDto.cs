namespace HeyDEAN_API.DTOs
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string? Title { get; set; } // optional
        public string? Description { get; set; }
        public string? Meta { get; set; }
    }
}
