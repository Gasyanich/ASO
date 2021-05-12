using ASO.API.Common.Attributes;
using ASO.API.Common.Constants;
using ASO.Models.DTO.Users;
using ASO.Services.Helpers;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly ILogger<UsersController> _logger;

        public UsersController(IRoleService roleService, IUsersService usersService, IRegisterService registerService, ILogger<UsersController> logger)
        {
            _roleService = roleService;
            _usersService = usersService;
            _registerService = registerService;
            _logger = logger;
        }

        [HttpPost]
        [RolePermission]
        public virtual async Task<IActionResult> PostUserAsync(
            [FromQuery][Required] string role,
            UserRegisterDto userRegisterDto)
        {
            var result = await _registerService.RegisterUserAsync(userRegisterDto, role);
            if (result.IsSuccess)
            {
                _logger.LogInformation("User posted successfully");
                return Ok(result.UserDto);
            }
            else
            {
                _logger.LogInformation("User is not posted");
                return BadResultRequest(result);
            }
        }

        [HttpGet("{id}")]
        [RolePermission]
        public virtual async Task<IActionResult> GetUserAsync([FromQuery][Required] string role, long id)
        {
            if (!await _usersService.UserExistAsync(id))
            {
                _logger.LogInformation("User does not exist");
                return BadRequestWrongId(id);
            }


            var user = await _usersService.GetUserAsync(id);
            _logger.LogInformation("User found");
            return Ok(user);
        }

        [HttpPut]
        [RolePermission]
        public virtual async Task<IActionResult> PutUserAsync(
            [FromQuery][Required] string role,
            long id,
            UserUpdateDto userUpdateDto)
        {
            if (!await _usersService.UserExistAsync(id))
            {
                _logger.LogInformation("User does not exist");
                return BadRequestWrongId(id);
            }


            var user = await _usersService.UpdateUserAsync(id, userUpdateDto);
            _logger.LogInformation("User profile updated successfully");
            return Ok(user);
        }

        [HttpDelete]
        [RolePermission]
        public virtual async Task<IActionResult> DeleteUserAsync([FromQuery][Required] string role, long id)
        {
            if (!await _usersService.UserExistAsync(id))
            {
                _logger.LogInformation("User does not exist");
                return BadRequestWrongId(id);
            }


            await _usersService.DeleteUserAsync(id);
            _logger.LogInformation("User profile deleted succsessfully");
            return NoContent();
        }


        [HttpGet]
        [RolePermission(IsMultipleRoles = true)]
        public async Task<IActionResult> GetUsersByRoleAsync([FromQuery][Required] string roles)
        {
            var roleIds = roles.GetRoleNames().Select(roleName => _roleService.GetRoleId(roleName));
            var usersByRoles = await _usersService.GetUsersByRoleIdsAsync(roleIds);
            _logger.LogInformation("GetUserByRole is success");
            return Ok(usersByRoles);
        }
    }
}