using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASO.API.Common.Constants;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = AuthorizeConstants.UsersControllerRoles)]
    public class UsersController : AsoBaseController
    {
        private readonly IUsersService _usersService;
        private readonly IRoleService _roleService;

        public UsersController(IUsersService usersService, IRoleService roleService)
        {
            _usersService = usersService;
            _roleService = roleService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsersByRoleAsync([FromQuery(Name = "roles")] List<string> roles)
        {
            var isUserAccessRoles = await _roleService.CheckUserCanAccessRoles(roles);

            if (!isUserAccessRoles.IsSuccess)
                return BadResultRequest(isUserAccessRoles);

            var usersByRoles = await _usersService.GetUsersByRolesAsync(roles);

            return Ok(usersByRoles);
        }
    }
}