using System.Collections.Generic;
using System.Security.Claims;

namespace RestWithASPNETUdemy.Security
{
    public interface IToken
    {
         string GenerateAccessToken(IEnumerable<Claim> claims);
         string GenerateRefreshToken();
         ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}