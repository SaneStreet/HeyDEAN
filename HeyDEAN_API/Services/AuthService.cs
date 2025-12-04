using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HeyDEAN_API.Data;
using HeyDEAN_API.DTOs;
using HeyDEAN_API.Models;
using HeyDEAN_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HeyDEAN_API.Services
{
    public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
    {
        /*
            Async login using UserDto req, to return null if succeeded
        */
        public async Task<TokenResponseDto?> LoginAsync(UserDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if(user is null)
                return null;
            
            if(new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }


            return await CreateTokenResponse(user);
        }

        /*
            Async creates a response with token provided for User object
            Content: token, refreshToken, userID, userName
        */
        private async Task<TokenResponseDto> CreateTokenResponse (User? user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            return new TokenResponseDto
            {
                Token = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),
                UserId = user.UserId,
                UserName = user.Username,
            };
        }

        /*
            Async registration of Users using UserDto request
            Checks for existing users
            Hashes password, sets standard Role and Username 
        */
        public async Task<User?> RegisterAsync(UserDto request)
        {
            if(await context.Users.AnyAsync(u => u.Username == request.Username))
                return null;
            
            var user = new User();
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);
            
            user.Username = request.Username;
            user.PasswordHash = hashedPassword;
            user.Role = "User";

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
            
        }

        /*
            Async creates RefreshToken for User object
        */
        public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if(user is null)
                return null;
            
            return await CreateTokenResponse(user);
        }

        /*
            Validates the refresh token when requested for the User object
        */
        private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await context.Users.FindAsync(userId);
            if(user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiration <= DateTime.UtcNow)
                return null;
            
            return user;
        }

        /*
            Generates a new refreshToken when needed
        */
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /*
            Async makes and saves a new refreshToken, which expires after 7 days
        */
        public async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshToken;
        }
        
        /*
            Creates a new token for User object
            Sets the Claim lists needed
            Adds a symmetric security key, signed credentials and a JWT descriptor 
        */
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role ?? string.Empty),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));
            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}