using System.ComponentModel.DataAnnotations;

namespace ASO.Models.DTO.Login
{
    public record LoginReqDto
    {
        [Required] [EmailAddress] public string Email { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }
    }
}