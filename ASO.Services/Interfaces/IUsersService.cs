using System.Collections.Generic;
using System.Threading.Tasks;
using ASO.Models.DTO.Users;

namespace ASO.Services.Interfaces
{
    public interface IUsersService
    {
        Task<UserDto> RegisterUserAsync(UserRegisterDto userRegisterDto, string role);

        Task<UserDto> GetUserAsync(long userId);

        Task<UserDto> UpdateUserAsync(long id, UserUpdateDto userDto);

        Task DeleteUserAsync(long id);

        Task<IEnumerable<UserDto>> GetUsersByRolesAsync(IEnumerable<string> roleNames);

        Task<bool> UserExistAsync(long id);
    }
}