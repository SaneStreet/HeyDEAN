using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HeyDEAN_API.DTOs;

namespace HeyDEAN_API.Controllers
{
    [Route("api/dean")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        [HttpPost("ask")]
        public IActionResult AskDean([FromBody] DeanRequestDto req)
        {
            if (string.IsNullOrWhiteSpace(req.Prompt))
                return BadRequest("Prompt can't be empty.");
            
            var prompt = req.Prompt.ToLower();

            // simple mock AI rules
            if (prompt.Contains("note"))
                return Ok(new { reply = "SHOW_NOTES" });
            
            if (prompt.Contains("task"))
                return Ok(new { reply = "SHOW_TASKS" });
            
            if (prompt.Contains("event"))
                return Ok(new { reply = "SHOW_EVENTS" });

            // return standard AI-esque reply
            return Ok(new { reply = $"DEAN: I received: `{req.Prompt}`"});
        }
    }

}