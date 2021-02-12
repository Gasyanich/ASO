using System.Threading.Tasks;
using ASO.Models.DTO.Results;
using ASO.Models.DTO.Users;

namespace ASO.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<RegisterUserResult> RegisterUserAsync(UserRegisterDto registerDto, long roleId);
    }
}