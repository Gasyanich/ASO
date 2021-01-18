using System.Collections.Generic;
using System.Threading.Tasks;
using ASO.Models.DTO;
using ASO.Models.DTO.Users;

namespace ASO.Services.Interfaces
{
    public interface IUsersService
    {
        Task<UserDto> RegisterUserAsync(UserRegisterDto userRegisterDto, string role);

        Task<UserDto> GetUserAsync(long userId);

        Task<IEnumerable<UserDto>> GetAvailableUsersAsync();

        Task<UserDto> UpdateUserAsync(long id, UserUpdateDto userDto);

        Task DeleteUserAsync(long id);

        Task<bool> UserExistAsync(long id);
        Task<IEnumerable<UserDto>> GetUsersByRolesAsync(IEnumerable<string> roleNames);
    }
}