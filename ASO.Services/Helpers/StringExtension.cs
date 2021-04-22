using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace ASO.Services.Helpers
{
    public static class StringExtension
    {
        public static string GetIdentityRole(this string jwtString)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadToken(jwtString) as JwtSecurityToken;

            return decodedToken?.Claims.First(claim => claim.Type == "role").Value;
        }

        public static string[] GetRoleNames(this string roles)
        {
            var rolesArray = roles.Split(',').Select(role => role.ToUpper()).ToArray();

            return rolesArray;
        }
    }
}