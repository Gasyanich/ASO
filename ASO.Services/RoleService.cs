using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASO.DataAccess;
using ASO.DataAccess.Entities;
using ASO.Models.Constants;
using ASO.Models.DTO;
using ASO.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASO.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<UserRole> _roleManager;

        public RoleService(RoleManager<UserRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAvailableRolesAsync(string role)
        {
            var availableRoleString = role switch
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

            var availableRoles = await _roleManager.Roles
                .Where(userRole => availableRoleString.Contains(userRole.Name))
                .ToListAsync();

            return _mapper.Map<IEnumerable<RoleDto>>(availableRoles);
        }
    }
}