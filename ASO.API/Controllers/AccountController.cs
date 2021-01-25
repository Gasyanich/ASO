using System.Threading.Tasks;
using ASO.API.Common.Constants;
using ASO.Models.DTO.Login;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUsersService _usersService;

        public AccountController(IAccountService accountService, IUsersService usersService)
        {
            _accountService = accountService;
            _usersService = usersService;
        }

        [HttpGet("me")]
        [Authorize(Roles = AuthorizeConstants.MeRoles)]
        public async Task<IActionResult> MeAsync()
        {
            return Ok(await _usersService.GetMeAsync());
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginReqDto reqDto)
        {
            var loginResult = await _accountService.LoginAsync(reqDto);

            if (!loginResult.IsSuccess)
                return Unauthorized(loginResult.ErrorMessage);

            return Ok(loginResult.Token);
        }

        // TODO: вернуть после разработки
        //[HttpGet]
        //public async Task<IActionResult> ConfirmEmail(long usid, string tkn)
        //{
        //    var result = await _accountService.ConfirmEmailAsync(usid, tkn);

        //    return result ? Ok("Email подтвержден.") : BadRequest();
        //}
    }
}