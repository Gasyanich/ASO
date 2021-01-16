using System.Collections.Generic;
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
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var availableUsers = await _usersService.GetAvailableUsersAsync();

            return Ok(availableUsers);
        }
        [HttpGet("Roles")]
        public async Task<IActionResult> GetUsersByRoleAsync([FromQuery] IEnumerable<string> roleNames)
        {
            var usersByRoles = await _usersService.GetUsersByRolesAsync(roleNames);

            return Ok(usersByRoles);
        }
    }
}