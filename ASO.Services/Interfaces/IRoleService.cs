using System.Collections.Generic;
using System.Threading.Tasks;
using ASO.Models.DTO;

namespace ASO.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetRolesAsync();
        Task<bool> RoleExistAsync(string roleName);
        IEnumerable<string> GetAvailableRoles(string role);
    }
}