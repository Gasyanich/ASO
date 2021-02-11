using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ASO.DataAccess;
using ASO.DataAccess.Entities;
using ASO.Models.DTO.Login;
using ASO.Models.DTO.Users;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ASO.Services
{
    public class AccountService : IAccountService
    {
        private readonly ActionContext _actionContext;
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;
        private readonly IRoleService _roleService;
        private readonly UserManager<User> _userManager;
        private readonly IUsersService _usersService;


        public AccountService(UserManager<User> userManager,
            IConfiguration configuration,
            DataContext dataContext,
            IRoleService roleService,
            IActionContextAccessor actionContextAccessor,
            IUsersService usersService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _dataContext = dataContext;
            _roleService = roleService;
            _usersService = usersService;
            _actionContext = actionContextAccessor.ActionContext;
        }

        public async Task<LoginResultDto> LoginAsync(LoginReqDto loginReq)
        {
            var user = await _userManager.FindByEmailAsync(loginReq.Email);

            var result = new LoginResultDto(string.Empty, false, "Неверный логин или пароль");

            if (user == null)
                return result;

            var checkPassword = await _userManager.CheckPasswordAsync(user, loginReq.Password);

            if (!checkPassword)
                return result;

            var userRole = await _dataContext.UserRoles.FirstAsync(role => role.UserId == user.Id);
            var userRoleName = _roleService.GetRoleById(userRole.RoleId).Name;

            var token = GenerateJwtToken(user, userRoleName);

            return result with {IsSuccess = true, Token = token};
        }

        public async Task<bool> ConfirmEmailAsync(long userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded;
        }

        public async Task<UserDto> GetMeAsync()
        {
            var userClaims = _actionContext.HttpContext.User;
            var email = userClaims.FindFirst(ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            return await _usersService.GetUserAsync(user.Id);
        }

        private string GenerateJwtToken(User user, string roleName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Expires = DateTime.UtcNow.Add(TimeSpan.FromSeconds(int.MaxValue)).ToUniversalTime(),
                Expires = DateTime.UtcNow.AddDays(365),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, roleName)
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}