using System.Collections.Generic;
using System.Threading.Tasks;
using ASO.Models.DTO.Users;

namespace ASO.Services.Interfaces
{
    public interface IUsersService
    {
        Task<UserDto> GetUserAsync(long userId);

        Task<UserDto> UpdateUserAsync(long id, UserUpdateDto userDto);

        Task DeleteUserAsync(long id);

        Task<IEnumerable<UserDto>> GetUsersByRoleIdsAsync(IEnumerable<long> roleIds);

        Task<bool> UserExistAsync(long id);
    }
}