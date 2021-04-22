using System.ComponentModel.DataAnnotations;
using static ASO.Models.Constants.ErrorMessageConstants;

namespace ASO.Models.DTO.Login
{
    public record LoginReqDto
    {
        [Required(ErrorMessage = RequiredField)]
        [EmailAddress(ErrorMessage = IncorrectEmail)]
        public string Email { get; init; }

        [Required(ErrorMessage = RequiredField)]
        [DataType(DataType.Password)]
        public string Password { get; init; }
    }
}