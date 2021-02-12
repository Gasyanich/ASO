using ASO.Models.DTO.Users;

namespace ASO.Models.DTO.Results
{
    public record RegisterUserResult : BaseResultDto
    {
        public UserDto UserDto { get; set; }
    }
}