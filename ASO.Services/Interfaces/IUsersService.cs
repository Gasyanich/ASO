using System.Threading.Tasks;
using ASO.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace ASO.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IdentityResult> RegisterUserAsync(UserRegisterDto userRegister);
    }
}