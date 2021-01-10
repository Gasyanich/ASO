namespace ASO.Models.DTO.Login
{
    public record LoginResultDto(bool IsSuccess, string ErrorMessage, string Token);
}