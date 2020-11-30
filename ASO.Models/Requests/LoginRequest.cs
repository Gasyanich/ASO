using System.ComponentModel.DataAnnotations;

namespace ASO.Models.Requests
{
    public record LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }
    }
}