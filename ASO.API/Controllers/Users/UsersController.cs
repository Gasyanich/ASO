using System.Threading.Tasks;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}