using System.ComponentModel.DataAnnotations;

namespace ASO.Models.DTO.Users
{
    public record UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }
    }
}