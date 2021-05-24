using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RestWithASPNETUdemy.Configurations;

namespace RestWithASPNETUdemy.Security.Implementation
{
    public class Token : IToken
    {
        private readonly TokenConfiguration _tokenConfig;

        public Token(TokenConfiguration tokenConfig)
        {
            _tokenConfig = tokenConfig;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _tokenConfig.Issuer,
                audience: _tokenConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_tokenConfig.Minutes),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public string GenerateRefreshToken()
        {
            throw new System.NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}