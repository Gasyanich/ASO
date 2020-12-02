using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASO.Models.DTO;
using ASO.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASO.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly IMapper _mapper;

        public const string Director = "Director";
        public const string Manager = "Manager";
        public const string Student = "Student";
        public const string Admin = "Admin";
        public const string Teacher = "Teacher";

        public RoleService(RoleManager<IdentityRole<long>> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public IEnumerable<string> GetAvailableRoles(string role)
        {
            switch (role)
            {
                case Director:
                    return new[] {Manager, Student, Teacher};
                case Manager:
                    return new[] {Student};
                default:
                    return Array.Empty<string>();
            }
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<bool> RoleExistAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}