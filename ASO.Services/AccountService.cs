using System.Net.Http;
using System.Threading.Tasks;
using ASO.DataAccess.Entities;
using ASO.Models.DTO;
using ASO.Services.Interfaces;
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
            // TODO
            using var client = new HttpClient();


            return userLogin with {IsSuccess = true};
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