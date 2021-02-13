using System.Threading.Tasks;
using ASO.Models.DTO.Results;
using ASO.Models.DTO.Users;

namespace ASO.Services.Interfaces
{
    public interface IRoleService
    {
        Task<BaseResultDto> CheckUserCanAccessRoles(params string[] roles);

        RoleDto GetRoleById(long userRoleId);

        long GetRoleId(string roleName);
    }
}