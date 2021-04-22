using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public bool CheckUserHasPermissionsToRoles(string roles)
        {
            var roleNames = roles.GetRoleNames();

            if (!roleNames.Any())
                return false;

            var currentUserRole = _actionContext.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            var availableRoleNames = currentUserRole switch
            {
                RolesConstants.Director => new[]
                {
                    RolesConstants.Manager, RolesConstants.Student, RolesConstants.Teacher
                },
                RolesConstants.Manager => new[]
                {
                    RolesConstants.Student
                },
                RolesConstants.Admin => new[]
                {
                    RolesConstants.Director
                },
                _ => Array.Empty<string>()
            };

            if (!availableRoleNames.Any())
                return false;

            var isUsersAccessToAllRoles = roleNames.All(role => availableRoleNames.Contains(role.ToUpper()));

            return isUsersAccessToAllRoles;
        }

        public RoleDto GetRoleById(long roleId)
        {
            return _roleToRoleId[roleId];
        }

        public long GetRoleId(string roleName)
        {
            var roleId = _roleToRoleId.Values
                .FirstOrDefault(roleDto => roleDto.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase))?.Id;

            return roleId ?? 0;
        }
    }
}