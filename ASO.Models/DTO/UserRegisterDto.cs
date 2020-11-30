namespace ASO.Models.DTO
{
    public record UserRegisterDto
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        private string Patronymic { get; init; }

        public string Email { get; init; }
        public string UserName { get; init; }
        public string Role { get; init; }
    }
}