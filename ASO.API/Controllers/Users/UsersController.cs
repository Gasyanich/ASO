using System.Linq;
using System.Threading.Tasks;
using ASO.API.Common.Constants;
using ASO.API.Permission;
using ASO.Models.DTO.Users;
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
        private readonly IRegisterService _registerService;

        public UsersController(IRoleService roleService, IUsersService usersService, IRegisterService registerService)
        {
            _roleService = roleService;
            _usersService = usersService;
            _registerService = registerService;
        }

        [HttpPost]
        [RolePermission]
        public virtual async Task<IActionResult> PostUserAsync([FromQuery] string role, UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _registerService.RegisterUserAsync(userRegisterDto, role);

            return result.IsSuccess ? Ok(result.UserDto) : BadResultRequest(result);
        }

        [HttpGet("{id}")]
        [RolePermission]
        public virtual async Task<IActionResult> GetUserAsync([FromQuery] string role, long id)
        {
            if (!await _usersService.UserExistAsync(id))
                return BadRequestWrongId(id);

            var user = await _usersService.GetUserAsync(id);

            return Ok(user);
        }

        [HttpPut]
        [RolePermission]
        public virtual async Task<IActionResult> PutUserAsync([FromQuery] string role, long id, UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _usersService.UserExistAsync(id))
                return BadRequestWrongId(id);

            var user = await _usersService.UpdateUserAsync(id, userUpdateDto);

            return Ok(user);
        }

        [HttpDelete]
        [RolePermission]
        public virtual async Task<IActionResult> DeleteUserAsync([FromQuery] string role, long id)
        {
            if (!await _usersService.UserExistAsync(id))
                return BadRequestWrongId(id);

            await _usersService.DeleteUserAsync(id);

            return NoContent();
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