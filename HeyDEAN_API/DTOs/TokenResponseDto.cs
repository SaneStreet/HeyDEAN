namespace HeyDEAN_API.DTOs
{
    public class TokenResponseDto
    {
        public required string Token { get; set;}
        public required string RefreshToken { get; set;}
        public Guid UserId { get; set;}
    }
}