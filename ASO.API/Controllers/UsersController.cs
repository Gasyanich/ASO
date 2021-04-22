using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASO.API.Common.Attributes;
using ASO.API.Common.Constants;
using ASO.Models.DTO.Users;
using ASO.Services.Helpers;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers
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
        public virtual async Task<IActionResult> PostUserAsync(
            [FromQuery] [Required] string role,
            UserRegisterDto userRegisterDto)
        {
            var result = await _registerService.RegisterUserAsync(userRegisterDto, role);

            return result.IsSuccess ? Ok(result.UserDto) : BadResultRequest(result);
        }

        [HttpGet("{id}")]
        [RolePermission]
        public virtual async Task<IActionResult> GetUserAsync([FromQuery] [Required] string role, long id)
        {
            if (!await _usersService.UserExistAsync(id))
                return BadRequestWrongId(id);

            var user = await _usersService.GetUserAsync(id);

            return Ok(user);
        }

        [HttpPut]
        [RolePermission]
        public virtual async Task<IActionResult> PutUserAsync(
            [FromQuery] [Required] string role,
            long id,
            UserUpdateDto userUpdateDto)
        {
            if (!await _usersService.UserExistAsync(id))
                return BadRequestWrongId(id);

            var user = await _usersService.UpdateUserAsync(id, userUpdateDto);

            return Ok(user);
        }

        [HttpDelete]
        [RolePermission]
        public virtual async Task<IActionResult> DeleteUserAsync([FromQuery] [Required] string role, long id)
        {
            if (!await _usersService.UserExistAsync(id))
                return BadRequestWrongId(id);

            await _usersService.DeleteUserAsync(id);

            return NoContent();
        }


        [HttpGet]
        [RolePermission(IsMultipleRoles = true)]
        public async Task<IActionResult> GetUsersByRoleAsync([FromQuery] [Required] string roles)
        {
            var roleIds = roles.GetRoleNames().Select(roleName => _roleService.GetRoleId(roleName));
            var usersByRoles = await _usersService.GetUsersByRoleIdsAsync(roleIds);

            return Ok(usersByRoles);
        }
    }
}