using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ModelsApi.Utilities
{
    public static class JWTUtilities
    {
        public static string GenerateToken(string email, long modelId, bool isManager)
        {
            Claim roleClaim;
            if (isManager)
                roleClaim = new Claim("Role", "Manager");
            else
                roleClaim = new Claim("Role", "Model");

            var claims = new Claim[]
            {
                new Claim("Email", email),
                roleClaim,
                new Claim("ModelId", modelId.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                 new JwtHeader(new SigningCredentials(
                      new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256")),
                                               SecurityAlgorithms.HmacSha256)),
                      new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
