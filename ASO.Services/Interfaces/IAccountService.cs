using System.Threading.Tasks;
using ASO.DataAccess.Entities;
using ASO.Models.DTO.Login;
using ASO.Models.DTO.Users;

namespace ASO.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResultDto> LoginAsync(LoginReqDto request);

        Task<UserDto> GetMeAsync();

        Task<User> GetCurrentUserAsync();

        Task<bool> ConfirmEmailAsync(long userId, string token);
    }
}