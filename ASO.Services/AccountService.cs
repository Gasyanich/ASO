using System;
using System.Threading.Tasks;
using ASO.DataAccess.Entities;
using ASO.Models.DTO;
using ASO.Models.Requests;
using ASO.Models.Responses;
using ASO.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ASO.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly IMapper _mapper;

        public AccountService(
            UserManager<User> userManager,
            RoleManager<IdentityRole<long>> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterUserAsync(UserRegisterDto userRegister)
        {
            var user = _mapper.Map<User>(userRegister);
            var password = "randomPassword";

            if (!await _roleManager.RoleExistsAsync(userRegister.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole<long>(userRegister.Role));
            }

            var result = await _userManager.CreateAsync(user, password);

            await _userManager.AddToRoleAsync(user, userRegister.Role);

            return result;
        }

        public Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}