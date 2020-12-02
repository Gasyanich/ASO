using System.Net.Http;
using System.Threading.Tasks;
using ASO.DataAccess.Entities;
using ASO.Models.DTO;
using ASO.Services.Helpers;
using ASO.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;

namespace ASO.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserLoginDto> LoginAsync(UserLoginDto userLogin)
        {
            using var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "spa.aso.react",
                UserName = userLogin.Email,
                Password = userLogin.Password
            });

            if (tokenResponse.IsError)
                return userLogin with {IsSuccess = false};

            var accessToken = tokenResponse.AccessToken;
            var role = accessToken.GetIdentityRole();

            return userLogin with {IsSuccess = true, AccessToken = accessToken, Role = role};
        }

        public async Task<bool> ConfirmEmailAsync(long userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded;
        }
    }
}