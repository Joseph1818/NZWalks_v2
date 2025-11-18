namespace NZWalks.API.Models.DTO
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
