using System.Threading.Tasks;
using ASO.Models.DTO.Login;

namespace ASO.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResultDto> LoginAsync(LoginReqDto request);

        Task<bool> ConfirmEmailAsync(long userId, string token);
    }
}