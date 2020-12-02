namespace ASO.Models.Responses
{
    public record LoginResponse
    {
        public string AccessToken { get; init; }
        public string Email { get; init; }
        public string Role { get; init; }
    }
}