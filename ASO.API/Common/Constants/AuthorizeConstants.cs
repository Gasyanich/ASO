using ASO.Models.Constants;

namespace ASO.API.Common.Constants
{
    public class AuthorizeConstants
    {
        public const string UsersControllerRoles = RolesConstants.Director + "," +
                                                   RolesConstants.Admin + "," +
                                                   RolesConstants.Teacher + ",";
    }
}