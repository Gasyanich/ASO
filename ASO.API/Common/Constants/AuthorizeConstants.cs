using ASO.Models.Constants;

namespace ASO.API.Common.Constants
{
    public class AuthorizeConstants
    {
        public const string UsersControllerRoles = RolesConstants.Director + "," +
                                                   RolesConstants.Admin + "," +
                                                   RolesConstants.Manager;

        public const string StudentsControllerRoles = RolesConstants.Manager + "," +
                                                      RolesConstants.Director;

        public const string TeachersControllerRoles = RolesConstants.Director;

        public const string ManagersControllerRoles = RolesConstants.Director;


        public const string MeRoles = RolesConstants.Director + "," +
                                      RolesConstants.Admin + "," +
                                      RolesConstants.Teacher + "," +
                                      RolesConstants.Student + "," +
                                      RolesConstants.Manager;
    }
}