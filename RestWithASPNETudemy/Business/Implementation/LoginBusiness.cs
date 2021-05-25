using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RestWithASPNETUdemy.Configurations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Security;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class LoginBusiness : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _tokenConfig;
        private IUserRepository _repo;
        private readonly IToken _tokenService;

        public LoginBusiness(TokenConfiguration tokenConfig, IUserRepository repo, IToken tokenService)
        {
            _tokenConfig = tokenConfig;
            _repo = repo;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            // Validando se o usuário existe
            var user = _repo.ValidateCredentials(userCredentials);

            if (user == null)
                return null;

            // Definindo as claims
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, userCredentials.Username));

            // Definição do access token e refresh token
            var accessToken = _tokenService.GenerateAccessToken(claims);

            // Quando o access token expirar, este será usado
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_tokenConfig.DaysToExpire);
            
            _repo.RefreshUserInfo(user);

            var createDate = DateTime.Now;
            var expirationDate = createDate.AddMinutes(_tokenConfig.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token.AccessToken);
            var userName = principal.Identity.Name;
            var user = _repo.ValidateCredentials(userName);

            if (user == null 
                || user.RefreshToken != token.RefreshToken 
                || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return null;                
            }

            token.AccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            token.RefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = token.RefreshToken;

            _repo.RefreshUserInfo(user);

            var createDate = DateTime.Now;
            var expirationDate = createDate.AddMinutes(_tokenConfig.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                token.AccessToken,
                token.RefreshToken
            );
        }

        public bool RevokeToken(string username)
        {
            return _repo.RevokeToken(username);
        }
    }
}