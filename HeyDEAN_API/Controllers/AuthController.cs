using HeyDEAN_API.DTOs;
using HeyDEAN_API.Models;
using HeyDEAN_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/*
    Authentication controller for all Auth endpoints:
        * POST register      - Using UserDto to async-ly create new users
        * POST login         - Using UserDto to async-ly log in users with correct creds from DB
        * POST refresh-token - Using TokenResponseDto to async-ly refresh Tokens if authorized
        * AUTH "/"           - Authentication endpoint for testing Auth usability
        * admin-only         - Role "Admin" needed to access. Used for testing user/admin accessability
*/

namespace HeyDEAN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if(user is null)
                return BadRequest("Username already exists.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var result = await authService.LoginAsync(request);
            if(result is null)
                return BadRequest("Invalid username or password.");

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if(result is null || result.Token is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");
                
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok("You are Admin!");
        }

    }
}