using System.Threading.Tasks;
using ASO.Models.DTO;

namespace ASO.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserLoginDto> LoginAsync(UserLoginDto request);

        Task<bool> ConfirmEmailAsync(long userId, string token);
    }
}