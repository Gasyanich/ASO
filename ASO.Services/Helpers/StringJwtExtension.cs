using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace ASO.Services.Helpers
{
    public static class StringJwtExtension
    {
        public static string GetIdentityRole(this string jwtString)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadToken(jwtString) as JwtSecurityToken;

            return decodedToken?.Claims.First(claim => claim.Type == "role").Value;
        }
    }
}