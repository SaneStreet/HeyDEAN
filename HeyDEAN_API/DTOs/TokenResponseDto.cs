namespace HeyDEAN_API.DTOs
{
    /*
        Token Response Data Transfer Object:
        Needed for the async token generation/manipulation funcs
    */
    public class TokenResponseDto
    {
        public required string Token { get; set;}
        public required string RefreshToken { get; set;}
        public Guid UserId { get; set;}
        public string? UserName {get; set;}
    }
}