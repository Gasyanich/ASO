using System.Collections.Generic;
using System.Threading.Tasks;
using ASO.Models.DTO;
using ASO.Models.DTO.Users;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers.Users
{
    [ApiController]
    public abstract class BaseUsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        protected BaseUsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        protected abstract string Role { get; }

        [HttpPost]
        public virtual async Task<IActionResult> PostUserAsync(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _usersService.RegisterUserAsync(userRegisterDto, Role);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetUserAsync(long id)
        {
            if (!await _usersService.UserExistAsync(id))
                return BadRequestWrongId(id);

            var user = await _usersService.GetUserAsync(id);

            return Ok(user);
        }
        [HttpGet("Roles")]
        public virtual async Task<IActionResult> GetUsersByRoleAsync()
        {
            var usersByRoles = await _usersService.GetUsersByRolesAsync(new []{Role});

            return Ok(usersByRoles);
        }

        [HttpPut]
        public virtual async Task<IActionResult> PutUserAsync(long id, UserUpdateDto userUpdateDto)
        {
            if (!await _usersService.UserExistAsync(id))
                return BadRequestWrongId(id);

            var user = await _usersService.UpdateUserAsync(id, userUpdateDto);

            return Ok(user);
        }

        [HttpDelete]
        public virtual async Task<IActionResult> DeleteUserAsync(long id)
        {
            if (!await _usersService.UserExistAsync(id))
                return BadRequestWrongId(id);

            await _usersService.DeleteUserAsync(id);

            return NoContent();
        }

        private IActionResult BadRequestWrongId(long id)
        {
            return BadRequest($"User with id {id} not found");
        }
    }
}