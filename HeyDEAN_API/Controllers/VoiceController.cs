using HeyDEAN_API.DTOs;
using HeyDEAN_API.Repositories.Interfaces;
using HeyDEAN_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeyDEAN_API.Controllers
{
    [ApiController]
    [Route("api/voice")]
    public class VoiceController : ControllerBase
    {
        private readonly IVoiceService _voiceService;

        public VoiceController(IVoiceService voiceService)
        {
            _voiceService = voiceService;
        }

        [HttpPost("execute")]
        public async Task<IActionResult> Execute([FromBody] VoiceExecuteRequest req)
        {
            var res = await _voiceService.ExecuteAsync(req);
            return res.Success ? Ok(res) : BadRequest(res);
        }

        [HttpGet("logs")]
        public async Task<IActionResult> Logs([FromServices] IGenericRepository<HeyDEAN_API.Models.VoiceLog> voiceRepo)
        {
            var logs = await voiceRepo.GetAllAsync();
            return Ok(logs);
        }
    }
}
