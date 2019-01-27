using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SignRecognition.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SignRecognition.Authentication
{
    public class JwtFactory : IJwtFactory
    {
        private IConfiguration _config;

        public JwtFactory(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateEncodedToken(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Authentication error");
            }

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
               };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
