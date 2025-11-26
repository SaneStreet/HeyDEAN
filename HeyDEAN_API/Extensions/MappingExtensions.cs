using HeyDEAN_API.DTOs;
using HeyDEAN_API.Models;

namespace HeyDEAN_API.Extensions
{
    public static class MappingExtensions
    {
        public static NoteDto ToDto(this Note n)
        {
            return new NoteDto
            {
                Id = n.NoteId,
                Title = null,
                Description = n.Content,
                Meta = n.CreatedAt?.ToString("yyyy-MM-dd HH:mm")
            };
        }
        
        public static TaskDto ToDto(this TaskItem t)
        {
            return new TaskDto
            {
                Id = t.TaskId,
                Title = t.Title,
                Description = null,
                Meta = t.DueDate?.ToString("yyyy-MM-dd")
            };
        }

        public static EventDto ToDto(this Event e)
        {
            var date = e.Date?.ToString("yyyy-MM-dd") ?? "";
            var time = $"{e.StartTime?.ToString("HH:mm")} - {e.EndTime?.ToString("HH:mm")}";

            return new EventDto
            {
                Id = e.EventId,
                Title = e.Title,
                Description = null,
                Meta = $"{date}  ·  {time}"
            };
        }
    }
}