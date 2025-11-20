using HeyDEAN_API.Models;

namespace HeyDEAN_API.Repositories.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<string?> LoginAsync(UserDto request);
    }
}