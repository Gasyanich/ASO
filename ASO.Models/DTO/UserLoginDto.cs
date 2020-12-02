namespace ASO.Models.DTO
{
    public record UserLoginDto
    {
        public bool IsSuccess { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string AccessToken { get; set; }
        public string Role { get; init; }
    }
}