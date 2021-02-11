using ASO.Models.DTO.Results;

namespace ASO.Models.DTO.Login
{
    public record LoginResultDto : BaseResultDto
    {
        public LoginResultDto(string token, bool isSuccess, string errorMessage = "") : base(isSuccess, errorMessage)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}