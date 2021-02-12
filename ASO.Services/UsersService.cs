using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASO.DataAccess;
using ASO.DataAccess.Entities;
using ASO.Models.DTO.Users;
using ASO.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASO.Services
{
    public class UsersService : IUsersService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public UsersService(
            IMapper mapper,
            IRoleService roleService,
            DataContext dataContext)
        {
            _mapper = mapper;
            _roleService = roleService;
            _dataContext = dataContext;
        }

        #region CRUD

        public async Task<UserDto> GetUserAsync(long userId)
        {
            var user = await FindUserById(userId);

            return GetUserWithRole(user);
        }

        public async Task<UserDto> UpdateUserAsync(long id, UserUpdateDto userDto)
        {
            var user = await FindUserById(id);

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Patronymic = userDto.Patronymic;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Sex = userDto.Sex;

            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();

            return GetUserWithRole(user);
        }

        public async Task DeleteUserAsync(long id)
        {
            var userToDelete = await FindUserById(id);

            _dataContext.Remove(userToDelete);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetUsersByRoleIdsAsync(IEnumerable<long> roleIds)
        {
            var users = await _dataContext.Users
                .Include(user => user.Role)
                .Where(user => roleIds.Contains(user.RoleId))
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<bool> UserExistAsync(long id)
        {
            return await FindUserById(id) != null;
        }

        #endregion

        #region Private methods

        private UserDto GetUserWithRole(User user)
        {
            var userDto = _mapper.Map<UserDto>(user);

            var role = _roleService.GetRoleById(user.RoleId);
            userDto.Role = role;

            return _mapper.Map<UserDto>(userDto);
        }

        /// <summary>
        ///     Ищет пользователя в базе/контексте
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns></returns>
        private async Task<User> FindUserById(long id)
        {
            return await _dataContext.Users.FindAsync(id);
        }

        #endregion
    }
}