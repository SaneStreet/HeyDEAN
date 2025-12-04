using HeyDEAN_API.DTOs;
using HeyDEAN_API.Models;
using HeyDEAN_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HeyDEAN_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {

        /*
            Controls all endpoints of TaskItem ("api/tasks")
            TasksController:
                Connects to the GenericRepo
                GetAllTasks()   -> Fetches all Tasks
                GetTaskById     -> Finds Task on specific TaskId
                CreateTask      -> Makes a new Task using Post
                CompleteTask    -> Refreshes the IsComplete to true
                DeleteTask      -> Removes Task on specific TaskId

        */
        private readonly IGenericRepository<TaskItem> _repo;

        public TasksController(IGenericRepository<TaskItem> repo)
        {
            _repo = repo;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var t = await _repo.GetAllAsync();
            if (t == null)
                return NotFound();
            return Ok(new { message = "📝 Tasks found! ", data = t });
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTaskById(int id)
        {
            var t = await _repo.GetAsync(id);
            if (t == null)
                return NotFound($"❌ Task {id} not found ❌");

            return Ok(new { message = $"🗒️ Task found.", data = t });
        }

        // POST: api/tasks/
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            var t = new TaskItem { Title = dto.Title };
            var created = await _repo.AddAsync(t);
            return CreatedAtAction(nameof(GetTaskById), new { id = created.TaskId }, created);
        }

        // PUT: /api/tasks/5/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var t = await _repo.GetAsync(id);
            if (t == null) 
                return NotFound();
            
            t.IsCompleted = true;
            await _repo.UpdateAsync(t);
            return NoContent();
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var t = await _repo.GetAsync(id);
            if (t == null)
                return NotFound();
            await _repo.DeleteAsync(t);
            return NoContent();
        }
    }
}