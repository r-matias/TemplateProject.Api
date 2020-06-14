using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TemplateProject.Domain.Entities.Model;

namespace TemplateProject.Service.Services
{
    public static class TokenService
    {
        private static string _secretToken;

        public static void SetSecretToken(string secretToken)
        {
            _secretToken = secretToken;
        }

        public static string GenerateToken(User user)
        {
            if (string.IsNullOrEmpty(_secretToken))
                throw new ArgumentException("Secret token not found. [Verify appsettings CustomLogin:ClientSecret]");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretToken);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
