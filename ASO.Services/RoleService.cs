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
                1, new RoleDto
                {
                    Id = 1,
                    DisplayName = "Директор",
                    Name = RolesConstants.Director
                }
            },
            {
                2,
                new RoleDto
                {
                    Id = 2,
                    DisplayName = "Администратор",
                    Name = RolesConstants.Admin
                }
            },
            {
                3,
                new RoleDto
                {
                    Id = 3,
                    DisplayName = "Преподаватель",
                    Name = RolesConstants.Teacher
                }
            },
            {
                4, new RoleDto
                {
                    Id = 4,
                    DisplayName = "Менеджер",
                    Name = RolesConstants.Manager
                }
            },
            {
                5, new RoleDto
                {
                    Id = 5,
                    DisplayName = "Обучающийся",
                    Name = RolesConstants.Student
                }
            }
        };

        public IEnumerable<RoleDto> GetAvailableRoles(string role)
        {
            var availableRoleIds = role switch
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

            foreach (var availableRoleId in availableRoleIds) yield return _roleToRoleId[availableRoleId];
        }

        public RoleDto GetRoleById(long roleId)
        {
            return _roleToRoleId[roleId];
        }
    }
}