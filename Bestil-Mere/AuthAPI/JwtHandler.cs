using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthAPI
{
    public class JwtHandler
    {
        private readonly DateTime _accessTokenExpireDate = DateTime.UtcNow.AddDays(3);  
        
        public (string, int) GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim("role", user.Role),
                new Claim("id", user.Id.ToString()),
            };

            foreach (var scope in user.Scopes)
            {
                var claim = new Claim("scope", scope);
                claims.Add(claim);
            }
            
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    JwtSecurityKey.Key,
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null,
                    null,
                    claims.ToArray(),
                    DateTime.UtcNow,
                    _accessTokenExpireDate)
                ); 
            
            return (new JwtSecurityTokenHandler().WriteToken(token), (int)TimeSpan.FromDays(3).TotalSeconds);
        }
    }
}