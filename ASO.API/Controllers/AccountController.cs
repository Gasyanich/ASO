using System.Threading.Tasks;
using ASO.Models.DTO;
using ASO.Models.Requests;
using ASO.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDto = _mapper.Map<UserRegisterDto>(request);

            var registerResult = await _accountService.RegisterUserAsync(userDto);

            if (registerResult.Succeeded)
                return Ok();

            return BadRequest(registerResult.Errors);
        }
    }
}