using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASO.DataAccess;
using ASO.DataAccess.Entities;
using ASO.Models.Constants;
using ASO.Models.DTO;
using ASO.Models.DTO.Users;
using ASO.Services.Helpers;
using ASO.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;

namespace ASO.Services
{
    public class UsersService : IUsersService
    {
        private readonly ActionContext _actionContext;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IRoleService _roleService;
        private readonly DataContext _dataContext;

        public UsersService(
            IMapper mapper,
            IEmailService emailService,
            UserManager<User> userManager,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            RoleManager<UserRole> roleManager,
            IRoleService roleService,
            DataContext dataContext)
        {
            _actionContext = actionContextAccessor.ActionContext;

            _mapper = mapper;
            _emailService = emailService;
            _urlHelper = urlHelperFactory.GetUrlHelper(_actionContext);
            _userManager = userManager;
            _roleManager = roleManager;
            _roleService = roleService;
            _dataContext = dataContext;
        }

        public async Task<UserDto> RegisterUserAsync(UserRegisterDto userRegisterDto, string role)
        {
            var user = _mapper.Map<User>(userRegisterDto);
            var password = "Simple123";

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                await SendConfirmEmailAsync(user.Id, emailConfirmationToken, user.Email, password);

                await _userManager.AddToRoleAsync(user, role);
            }

            return await GetUserWithRole(user);
        }

        public async Task<UserDto> GetUserAsync(long userId)
        {
            var user = await FindUserById(userId);

            return await GetUserWithRole(user);
        }

        public async Task<IEnumerable<UserDto>> GetAvailableUsersAsync()
        {
            var accessToken = await _actionContext.HttpContext.GetTokenAsync("access_token");
            var role = accessToken.GetIdentityRole();

            var availableRoleIds = _roleService.GetAvailableRoleIds(role);

            var availableUsersWithRoleId = await
                (from userRole in _dataContext.UserRoles
                    join user in _dataContext.Users on userRole.UserId equals user.Id
                    where availableRoleIds.Contains(userRole.RoleId)
                    select new Tuple<User, long>(user, userRole.RoleId))
                .ToListAsync();

            return availableUsersWithRoleId.Select(userWithRoleId =>
            {
                var (user, userRoleId) = userWithRoleId;

                var userDto = _mapper.Map<UserDto>(user);
                userDto.Role = _roleService.GetRoleById(userRoleId);

                return userDto;
            });
        }

        public async Task<UserDto> UpdateUserAsync(long id, UserUpdateDto userDto)
        {
            var user = await FindUserById(id);

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Patronymic = userDto.Patronymic;
            user.PhoneNumber = userDto.PhoneNumber;


            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return await GetUserWithRole(user);

            return null;
        }

        public async Task DeleteUserAsync(long id)
        {
            var userToDelete = await FindUserById(id);

            await _userManager.DeleteAsync(userToDelete);
        }

        public async Task<bool> UserExistAsync(long id)
        {
            return await FindUserById(id) != null;
        }
        public async Task<IEnumerable<UserDto>> GetUsersByRolesAsync(IEnumerable<string> roleNames)
        {
            return await GetUsersByRoles(roleNames);
        }

        #region Private methods
        private async Task<IEnumerable<UserDto>> GetUsersByRoles(IEnumerable<string> roleNames)
        {
            var usersWithRoleId = await
                (from userRole in _dataContext.UserRoles
                    join user in _dataContext.Users on userRole.UserId equals user.Id
                    join roleName in _roleManager.Roles on userRole.RoleId equals roleName.Id
                    where roleNames.Contains(roleName.Name)
                    select new Tuple<User, long>(user, userRole.RoleId))
                .ToListAsync();

            return usersWithRoleId.Select(usersWithRoles =>
            {
                var (user, userRoleId) = usersWithRoles;

                var userDto = _mapper.Map<UserDto>(user);
                userDto.Role = _roleService.GetRoleById(userRoleId);

                return userDto;
            });
        }
        private async Task<UserDto> GetUserWithRole(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var userRole = await _roleManager.FindByNameAsync(userRoles.First());

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Role = _mapper.Map<RoleDto>(userRole);

            return userDto;
        }

        private async Task<User> FindUserById(long id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        private async Task SendConfirmEmailAsync(long userId, string token, string email, string userPassword)
        {
            var request = _actionContext.HttpContext.Request;
            var confirmationLink = _urlHelper.Action("ConfirmEmail", "Account",
                new {usid = userId, tkn = token}, request.Scheme, request.Host.ToString());

            var message = GetEmailBodyMessage(userPassword, confirmationLink);

            await _emailService.SendAsync(email, "Подтверждение регистрации на сайте Автошкола онлайн", message);
        }

        private string GetEmailBodyMessage(string userPassword, string confirmationLink)
        {
            return "Здравствуйте!" +
                   "\n\nВы зарегестрировались на сайте Автошкола онлайн." +
                   $"\n\nВаш пароль:{userPassword}" +
                   $"\nДля подтверждения регистрации пройдите по следующей ссылке - {confirmationLink}" +
                   "\nЭто письмо отправлено автоматически, не отвечайте на него. " +
                   "Если Вы считаете, что письмо пришло к вам по ошибке, просто удалите его.";
        }

        #endregion
    }
}