namespace ASO.Models.DTO.Users
{
    public record UserDto
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public RoleDto Role { get; set; }
    }
}