using System.ComponentModel.DataAnnotations;

namespace ASO.Models.Requests
{
    public record RegisterRequest(
        [Required] string FirstName,
        [Required] string LastName,
        [Required] string Patronymic,
        [Required] string Email,
        [Required] string Role);
}