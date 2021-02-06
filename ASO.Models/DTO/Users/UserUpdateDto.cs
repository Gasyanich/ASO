using System.ComponentModel.DataAnnotations;
using ASO.DataAccess.Entities;

namespace ASO.Models.DTO.Users
{
    public record UserUpdateDto
    {
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] public string Patronymic { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Required] [Phone] public string PhoneNumber { get; set; }

        [Required] public Sex Sex { get; set; }
    }
}