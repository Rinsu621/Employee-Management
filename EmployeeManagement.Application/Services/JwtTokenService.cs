using EmployeeManagement.Application.Interface;
using EmployeeManagement.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Application.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly string _jwtSecret;
        private readonly int _jwtLifespan;

        public JwtTokenService(string jwtSecret, int jwtLifespan)
        {
            _jwtSecret = jwtSecret;
            _jwtLifespan = jwtLifespan;
        }

        public string GenerateToken(Employee employee)
        {
            //Claims are piece of information stored inside the tokens
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, employee.Email),
                new Claim("id", employee.Id.ToString()),
                new Claim(ClaimTypes.Role, employee.Role)
            };
            //Symmetric key from the secret, HMAC SHA-256 algorithm
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "EmployeeManagement.Api",
                audience: "EmployeeManagement.Api",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtLifespan),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
