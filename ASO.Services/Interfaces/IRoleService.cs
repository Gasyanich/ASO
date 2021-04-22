using ASO.Models.DTO.Users;

namespace ASO.Services.Interfaces
{
    public interface IRoleService
    {
        bool CheckUserHasPermissionsToRoles(string roles);

        RoleDto GetRoleById(long userRoleId);

        long GetRoleId(string roleName);
    }
}