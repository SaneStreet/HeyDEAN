using HeyDEAN_API.DTOs;
using HeyDEAN_API.Models;
using HeyDEAN_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HeyDEAN_API.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly IGenericRepository<Event> _repo;

        public EventsController(IGenericRepository<Event> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventDto dto)
        {
            var e = new Event
            {
                Title = dto.Title,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };
            var created = await _repo.AddAsync(e);
            return CreatedAtAction(nameof(GetAll), new { id = created.EventId }, created);
        }
    }
}
