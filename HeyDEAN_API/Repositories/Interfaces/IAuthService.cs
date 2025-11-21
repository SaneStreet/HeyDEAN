using HeyDEAN_API.DTOs;
using HeyDEAN_API.Models;

namespace HeyDEAN_API.Repositories.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}