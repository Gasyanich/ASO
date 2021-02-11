using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASO.Models.Constants;
using ASO.Models.DTO.Results;
using ASO.Models.DTO.Users;
using ASO.Services.Helpers;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ASO.Services
{
    public class RoleService : IRoleService
    {
        private readonly ActionContext _actionContext;

        private readonly Dictionary<long, RoleDto> _roleToRoleId = new()
        {
            {
                RolesConstants.DirectorId, new RoleDto
                {
                    Id = RolesConstants.DirectorId,
                    DisplayName = "Директор",
                    Name = RolesConstants.Director
                }
            },
            {
                RolesConstants.AdminId, new RoleDto
                {
                    Id = RolesConstants.AdminId,
                    DisplayName = "Администратор",
                    Name = RolesConstants.Admin
                }
            },
            {
                RolesConstants.TeacherId, new RoleDto
                {
                    Id = RolesConstants.TeacherId,
                    DisplayName = "Преподаватель",
                    Name = RolesConstants.Teacher
                }
            },
            {
                RolesConstants.ManagerId, new RoleDto
                {
                    Id = RolesConstants.ManagerId,
                    DisplayName = "Менеджер",
                    Name = RolesConstants.Manager
                }
            },
            {
                RolesConstants.StudentId, new RoleDto
                {
                    Id = RolesConstants.StudentId,
                    DisplayName = "Обучающийся",
                    Name = RolesConstants.Student
                }
            }
        };

        public RoleService(IActionContextAccessor actionContextAccessor)
        {
            _actionContext = actionContextAccessor.ActionContext;
        }

        public async Task<BaseResultDto> CheckUserCanAccessRoles(List<string> roles)
        {
            var accessToken = await _actionContext.HttpContext.GetTokenAsync("access_token");
            var currentUserRole = accessToken.GetIdentityRole();

            var availableRolesIds = currentUserRole switch
            {
                RolesConstants.Director => new[]
                {
                    RolesConstants.ManagerId, RolesConstants.StudentId, RolesConstants.TeacherId
                },
                RolesConstants.Manager => new[]
                {
                    RolesConstants.StudentId
                },
                RolesConstants.Admin => new[]
                {
                    RolesConstants.DirectorId
                },
                _ => Array.Empty<long>()
            };

            var result = new BaseResultDto(true);

            var availableRolesNames = availableRolesIds
                .Select(roleId => _roleToRoleId[roleId]).Select(role => role.Name)
                .ToList();

            if (!availableRolesNames.Any())
                return result with
                {
                    IsSuccess = false, ErrorMessage = "Текущий пользователь не имеет доступа ни к одной роли"
                };

            var isUsersAccessToAllRoles = roles.All(role => availableRolesNames.Contains(role));

            if (isUsersAccessToAllRoles)
                return result;

            var nonAccessRoles = roles.Except(availableRolesNames);

            return result with
            {
                IsSuccess = false,
                ErrorMessage =
                $"Текущий пользователь не может получить доступ к следующим ролям:{string.Join(',', nonAccessRoles)}"
            };
        }

        public RoleDto GetRoleById(long roleId)
        {
            return _roleToRoleId[roleId];
        }
    }
}