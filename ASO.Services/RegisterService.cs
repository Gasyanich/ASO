using System.Threading.Tasks;
using ASO.DataAccess.Entities;
using ASO.Models.DTO.Results;
using ASO.Models.DTO.Users;
using ASO.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ASO.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly UserManager<User> _userManager;

        public RegisterService(IMapper mapper, IRoleService roleService, UserManager<User> userManager)
        {
            _mapper = mapper;
            _roleService = roleService;
            _userManager = userManager;
        }

        public async Task<RegisterUserResult> RegisterUserAsync(UserRegisterDto registerDto, string role)
        {
            var roleId = _roleService.GetRoleId(role);

            var registerResult = new RegisterUserResult();

            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
                return registerResult with {ErrorMessage = "Пользователь с таким именем уже существует"};

            var user = _mapper.Map<User>(registerDto with {RoleId = roleId});
            var password = "Simple123";

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Role = _roleService.GetRoleById(roleId);

                return registerResult with {IsSuccess = true, UserDto = userDto};
            }

            return registerResult with {ErrorMessage = "Возникла ошибка при регистрации. Повторите попытку позже"};
        }
    }
}