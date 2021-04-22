using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ASO.DataAccess.Entities;
using static ASO.Models.Constants.ErrorMessageConstants;

namespace ASO.Models.DTO.Users
{
    /// <summary>
    ///     DTO на запрос регистрации пользователя
    /// </summary>
    public record UserRegisterDto
    {
        [Required(ErrorMessage = RequiredField)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = RequiredField)]
        public string LastName { get; set; }

        [Required(ErrorMessage = RequiredField)]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [EmailAddress(ErrorMessage = IncorrectEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [Phone]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некорректный номер телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = RequiredField)]
        public Sex Sex { get; set; }

        // не приходит с запросом
        [JsonIgnore] public long RoleId { get; set; }
    }
}