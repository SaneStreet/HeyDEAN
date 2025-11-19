using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeyDEAN_API.Data;
using HeyDEAN_API.Models;
using HeyDEAN_API.Repositories;
using HeyDEAN_API.Repositories.Interfaces;

namespace HeyDEAN_API.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IGenericRepository<Note> _repo;

        public NotesController(IGenericRepository<Note> repo)
        {
            _repo = repo;
        }

        // GET : api/notes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Note>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _repo.GetAllAsync();
            if (notes == null)
                return NoContent();
            return Ok(new { message = "📝 Notes found! ", data = notes });
        }

        // GET: api/notes/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Note>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetNoteById(int id)
        {
            var note = await _repo.GetAsync(id);
            if (note == null)
                return NotFound($"❌ Note {id} not found ❌");

            return Ok(new { message = $"🗒️ Note found.", data = note });
        }


    }
}