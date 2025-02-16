using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using User_service.infrastructure;
using Auth_service.domain.entity;
using User_service.infrastructure.Dbcontext;

namespace User_service.api.controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        [HttpPost("/token")]
        public IActionResult Token(int userId, string password)
        {
            var identity = GetIdentity(userId, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
            issuer: PrivateInfoHolder.ISSUER,
                    audience: PrivateInfoHolder.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(PrivateInfoHolder.LIFETIME)),
                    signingCredentials: new SigningCredentials(PrivateInfoHolder.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(int userId, string password)
        {
            User? person;
            using (UserDbContext db = new UserDbContext())
            {
                person = db.Users.FirstOrDefault(u => u.id == userId && u.IsPasswordEqual(password));
            }
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("ID", person.id.ToString()),
                    new Claim("password", password)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
    

