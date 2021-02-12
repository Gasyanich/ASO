using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using ASO.Models.DTO;
using ASO.Models.DTO.Users;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers.Users
{
    [ApiController]
    public abstract class BaseUsersController : AsoBaseController
    {
        private readonly IUsersService _usersService;
        private readonly IRegisterService _registerService;

        protected BaseUsersController(IUsersService usersService, IRegisterService registerService)
        {
            _usersService = usersService;
            _registerService = registerService;
        }

        protected abstract long RoleId { get; }

        [HttpPost]
        public virtual async Task<IActionResult> PostUserAsync(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _registerService.RegisterUserAsync(userRegisterDto, RoleId);

            if (result.IsSuccess)
                return Ok(result.UserDto);

            return BadResultRequest(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetUserAsync(long id)
        {
            if (!await _usersService.UserExistAsync(id))
                return BadRequestWrongId(id);

            var user = await _usersService.GetUserAsync(id);

            return Ok(user);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetUsersByRoleAsync()
        {
            var usersByRoles = await _usersService.GetUsersByRolesAsync(new []{"RoleId"});

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