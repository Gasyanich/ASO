using System.Threading.Tasks;
using ASO.Models.DTO;
using ASO.Models.Requests;
using ASO.Services;
using ASO.Services.Helpers;
using ASO.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService usersService, IRoleService roleService, IMapper mapper)
        {
            _usersService = usersService;
            _roleService = roleService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Director")]
        [HttpPost("anyuser")]
        public async Task<IActionResult> RegisterUserAsync([FromForm] RegisterRequest request)
        {
            return await RegisterAnyUserAsync(request);
        }

        [Authorize(Roles = "Director,Manager")]
        [HttpPost("student")]
        public async Task<IActionResult> RegisterStudentAsync([FromForm] RegisterRequest request)
        {
            return await RegisterAnyUserAsync(request, true);
        }

        [Authorize(Roles = "Director,Manager")]
        [HttpGet("roles")]
        public async Task<IActionResult> GetAvailableRoles()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var role = accessToken.GetIdentityRole();

            return Ok(_roleService.GetAvailableRoles(role));
        }

        private async Task<IActionResult> RegisterAnyUserAsync(RegisterRequest request, bool isStudent = false)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDto = _mapper.Map<UserRegisterDto>(request);

            if (isStudent)
                userDto = userDto with{ Role = RoleService.Student};

            var registerResult = await _usersService.RegisterUserAsync(userDto);

            if (registerResult.Succeeded)
                return Ok();

            return BadRequest(registerResult.Errors);
        }
    }
}