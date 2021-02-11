using System.Collections.Generic;
using System.Threading.Tasks;
using ASO.Models.DTO.Results;
using ASO.Models.DTO.Users;

namespace ASO.Services.Interfaces
{
    public interface IRoleService
    {
        Task<BaseResultDto> CheckUserCanAccessRoles(List<string> roles);

        RoleDto GetRoleById(long userRoleId);
    }
}