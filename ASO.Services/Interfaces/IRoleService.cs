using System.Collections.Generic;
using System.Threading.Tasks;
using ASO.Models.DTO;

namespace ASO.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAvailableRolesAsync(string role);
    }
}