using System.Threading.Tasks;
using ASO.Models.DTO;
using ASO.Models.DTO.Users;
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

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] UserLoginDto dto)
        {
            //var loginDto = _mapper.Map<UserLoginDto>(dto);

            //var loginResult = await _accountService.LoginAsync(loginDto);
            //if (loginResult.IsSuccess)
            //    return Ok(_mapper.Map<LoginResponse>(loginResult));

            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(long usid, string tkn)
        {
            var result = await _accountService.ConfirmEmailAsync(usid, tkn);

            return result ? Ok("Email подтвержден.") : BadRequest();
        }
    }
}