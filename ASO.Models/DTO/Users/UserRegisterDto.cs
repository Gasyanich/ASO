using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ASO.DataAccess.Entities;

namespace ASO.Models.DTO.Users
{
    /// <summary>
    /// DTO на запрос регистрации пользователя
    /// </summary>
    public record UserRegisterDto
    {
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] public string Patronymic { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Required] [Phone] public string PhoneNumber { get; set; }

        [Required] public Sex Sex { get; set; }

        // не приходит с запросом
        [JsonIgnore] public long RoleId { get; set; }
    }
}