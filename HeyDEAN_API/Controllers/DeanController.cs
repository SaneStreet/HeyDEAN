using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HeyDEAN_API.DTOs;

namespace HeyDEAN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeanController : ControllerBase
    {
        [HttpPost("ask")]
        public IActionResult AskDean([FromBody] DeanRequestDto req)
        {
            if (string.IsNullOrWhiteSpace(req.Prompt))
                return BadRequest("Prompt can't be empty.");
            
            var prompt = req.Prompt.ToLower();

            // simple mock AI rules
            if (prompt.Contains("note"))
                return Ok(new { reply = "Here is your notes:" });
            
            if (prompt.Contains("task"))
                return Ok(new { reply = "Here is your tasks:" });
            
            if (prompt.Contains("event"))
                return Ok(new { reply = "Here is your events:" });

            // return standard AI-esque reply
            return Ok(new { reply = $"I received: `{req.Prompt}`, is that correct?"});
        }
    }

}