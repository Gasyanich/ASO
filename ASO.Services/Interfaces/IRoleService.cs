using System.Collections.Generic;
using System.Threading.Tasks;
using ASO.Models.DTO;

namespace ASO.Services.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<RoleDto> GetAvailableRoles(string role);

        IEnumerable<long> GetAvailableRoleIds(string role);

        RoleDto GetRoleById(long userRoleId);
    }
}