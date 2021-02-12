using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASO.DataAccess;
using ASO.DataAccess.Entities;
using ASO.Models.DTO;
using ASO.Models.DTO.Users;
using ASO.Services.Interfaces;
using AutoMapper;
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
        private readonly DataContext _dataContext;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IRoleService _roleService;
        private readonly IUrlHelper _urlHelper;
        private readonly UserManager<User> _userManager;

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

        #region CRUD

        public async Task<UserDto> GetUserAsync(long userId)
        {
            var user = await FindUserById(userId);

            return await GetUserWithRole(user);
        }

        public async Task<UserDto> UpdateUserAsync(long id, UserUpdateDto userDto)
        {
            var user = await FindUserById(id);

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Patronymic = userDto.Patronymic;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Sex = userDto.Sex;

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

        public async Task<IEnumerable<UserDto>> GetUsersByRolesAsync(IEnumerable<string> roleNames)
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
            }).OrderBy(userDto => userDto.Role.DisplayName);
        }

        public async Task<bool> UserExistAsync(long id)
        {
            return await FindUserById(id) != null;
        }

        #endregion

        #region Private methods

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

        #endregion
    }
}