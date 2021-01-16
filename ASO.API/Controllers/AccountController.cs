using System.Threading.Tasks;
using ASO.Models.DTO;
using ASO.Models.DTO.Login;
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

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginReqDto reqDto)
        {
            var loginResult = await _accountService.LoginAsync(reqDto);

            if (!loginResult.IsSuccess)
                return Unauthorized(loginResult.ErrorMessage);

            return Ok(loginResult.Token);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(long usid, string tkn)
        {
            var result = await _accountService.ConfirmEmailAsync(usid, tkn);

            return result ? Ok("Email подтвержден.") : BadRequest();
        }
    }
}