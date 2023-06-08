using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BongOliver.Models;
using Microsoft.IdentityModel.Tokens;

namespace BongOliver.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.username),
                new Claim("id", user.id.ToString()),
                new Claim("fullname", user.name),
                new Claim("avatar", user.avatar),
                new Claim("email", user.email == null? "" : user.email),
                new Claim("phone", user.phone),
                new Claim("walet", user.Walet.money.ToString()),
                //new Claim(JwtRegisteredClaimNames.Email, $"{username}@bongoliver.app")
            };


            if (user.roleId == 1)
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            else
            if (user.roleId == 3)
                claims.Add(new Claim(ClaimTypes.Role, "user"));
            else
                claims.Add(new Claim(ClaimTypes.Role, "stylist"));

            var symmetricKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["TokenKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    symmetricKey, SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}