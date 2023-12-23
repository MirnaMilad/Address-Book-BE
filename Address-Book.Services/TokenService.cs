using Address_Book.Core.Entities;
using Address_Book.Core.services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Address_Book.Services
{
    public class TokenService : ITokenService
    {
        public async Task<string> CreateTokenAsync(AppUser User)
        {
            //Payload
            // 1 . Private Claims [User - Defined]
            var AuthClaim = new List<Claim>()
            {
                new Claim (ClaimTypes.GivenName , User.UserName),
                new Claim (ClaimTypes.Email , User.Email)
            };
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("STRONGAUTHENTICATIONKEY"));
            var Token = new JwtSecurityToken(
                issuer: "https://localhost:7251/",
                audience: "MySecurekey",
                expires: DateTime.Now.AddDays(double.Parse("2")),
                claims: AuthClaim,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)

                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
