using System.Threading.Tasks;
using ASO.Models.DTO;
using ASO.Models.Requests;
using ASO.Models.Responses;
using Microsoft.AspNetCore.Identity;

namespace ASO.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUserAsync(UserRegisterDto userRegister);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}