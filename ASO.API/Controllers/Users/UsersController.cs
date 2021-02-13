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
        private readonly IRoleService _roleService;
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService, IRoleService roleService)
        {
            _usersService = usersService;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersByRoleAsync([FromQuery(Name = "roles")] string roles)
        {
            var rolesArray = roles.Split(',').Select(role => role.ToUpper()).ToArray();

            if (!rolesArray.Any())
                return BadRequestError("Не выбрано ни одной роли");

            var isUserAccessRoles = await _roleService.CheckUserCanAccessRoles(rolesArray);

            if (!isUserAccessRoles.IsSuccess)
                return BadResultRequest(isUserAccessRoles);


            var roleIds = rolesArray.Select(roleName => _roleService.GetRoleId(roleName));
            var usersByRoles = await _usersService.GetUsersByRoleIdsAsync(roleIds);

            return Ok(usersByRoles);
        }
    }
}