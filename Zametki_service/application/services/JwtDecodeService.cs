using System.IdentityModel.Tokens.Jwt;

namespace Zametki_service.application.services
{
    public class JwtDecodeService
    {
        public static JwtSecurityToken Decode(string EncodedJwt)
        {
            var token = "[encoded jwt]";
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            return jwtSecurityToken;
        }
        public static int GetJwtId(string EncodedJwt)
        {
            JwtSecurityToken token = JwtDecodeService.Decode(EncodedJwt);
            int.TryParse(token.Claims.First(p => p.Type == "ID").Value, out int userId);
            return userId;
        }

    }
}
