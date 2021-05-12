using ASO.API.Common.Constants;
using ASO.Models.DTO.Login;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ASO.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet("me")]
        [Authorize(Roles = AuthorizeConstants.MeRoles)]
        public async Task<IActionResult> MeAsync()
        {
            _logger.LogInformation("MeAsync is success");
            return Ok(await _accountService.GetMeAsync());
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginReqDto reqDto)
        {
            var loginResult = await _accountService.LoginAsync(reqDto);

            if (!loginResult.IsSuccess)
            {
                _logger.LogInformation("Log in is not successful");
                return Unauthorized(loginResult.ErrorMessage);
            }

            _logger.LogInformation("User logged in successfully");
            return Ok(loginResult.Token);
        }
    }
}