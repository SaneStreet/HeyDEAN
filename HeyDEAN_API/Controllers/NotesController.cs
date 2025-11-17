using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeyDEAN_API.Data;

namespace HeyDEAN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }

        // GET : api/notes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Note>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<Note>>> GetAllNotes()
        {
            var notes = await _context.Notes.ToListAsync();
            if (notes == null || notes.Count == 0)
                return NoContent();
            return Ok(new { message = "📝 Notes found! ", data = notes });
        }

        // GET: api/notes/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Note>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Note>> GetNoteById(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
                return NotFound($"❌ Note {id} not found ❌");

            return Ok(new { message = $"🗒️ Note found.", data = note });
        }
    }
}