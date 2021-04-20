using System.Collections.Generic;
using ASO.Models.Constants;

namespace ASO.Services.Helpers
{
    public static class RolePermissionHelper
    {
        #region Role permissions

        private static readonly Dictionary<string, List<string>> RoleToRolePermission = new()
        {
            {
                RolesConstants.Director, new List<string>
                {
                    RolesConstants.Manager, RolesConstants.Student, RolesConstants.Teacher
                }
            },
            {
                RolesConstants.Manager, new List<string>
                {
                    RolesConstants.Student
                }
            },
            {
                RolesConstants.Admin, new List<string>
                {
                    RolesConstants.Director
                }
            },
        };

        #endregion

        public static bool HasPermissionToRole(string currentUserRole, string role)
        {
            return RoleToRolePermission.TryGetValue(currentUserRole, out var roles) && roles.Contains(role);
        }
    }
}