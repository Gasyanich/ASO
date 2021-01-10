using System;
using System.Collections.Generic;
using ASO.Models.Constants;
using ASO.Models.DTO;
using ASO.Services.Interfaces;

namespace ASO.Services
{
    public class RoleService : IRoleService
    {
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

        public IEnumerable<RoleDto> GetAvailableRoles(string role)
        {
            var availableRoleIds = GetAvailableRoleIds(role);

            foreach (var availableRoleId in availableRoleIds) yield return _roleToRoleId[availableRoleId];
        }

        public IEnumerable<long> GetAvailableRoleIds(string role) => role switch
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


        public RoleDto GetRoleById(long roleId)
        {
            return _roleToRoleId[roleId];
        }
    }
}