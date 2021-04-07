﻿using System.Threading.Tasks;
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

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("me")]
        [Authorize(Roles = AuthorizeConstants.MeRoles)]
        public async Task<IActionResult> MeAsync()
        {
            return Ok(await _accountService.GetMeAsync());
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginReqDto reqDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginResult = await _accountService.LoginAsync(reqDto);

            if (!loginResult.IsSuccess)
                return Unauthorized(loginResult.ErrorMessage);

            return Ok(loginResult.Token);
        }
    }
}