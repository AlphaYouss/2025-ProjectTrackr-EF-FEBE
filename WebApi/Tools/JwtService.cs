using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models;
using WebApi.Tools.Interface;

namespace WebApi.Tools
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration config;

        public JwtService(IConfiguration config)
        {
            this.config = config;
        }

        public string GenerateToken(User user)
        {
            IConfigurationSection jwt = config.GetSection("Jwt");
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(jwt["Key"]));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            Claim[] claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                new Claim(ClaimTypes.Name, user.username)
            ];

            JwtSecurityToken token = new(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(jwt["ExpiresInHours"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}