using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RestWithASPNETudemy.Data.Converter;
using RestWithASPNETudemy.Models;
using RestWithASPNETudemy.Repository;
using RestWithASPNETudemy.Security.Configuration;

namespace RestWithASPNETudemy.Business
{
    public class UserBusiness : IUserBusiness
    {
        private IUserRepository _repo;
        private readonly UserConverter _converter;
        private SigningConfiguration _signinConfiguration;
        private TokenConfiguration _tokenConfiguration;

        public UserBusiness(IUserRepository repo,
            SigningConfiguration signinConfiguration,
            TokenConfiguration tokenConfiguration)
        {
            _repo = repo;
            _converter = new UserConverter();
            _signinConfiguration = signinConfiguration;
            _tokenConfiguration = tokenConfiguration;
        }

        public object FindByLogin(User user)
        {
            bool credentialIsValid = false;

            if (user != null && !string.IsNullOrWhiteSpace(user.Login))
            {
                var baseUser = _repo.FindByLogin(user.Login);
                credentialIsValid = (baseUser != null 
                    && user.Login == baseUser.Login 
                    && user.AcceessKey == baseUser.AcceessKey);
            }

            if (credentialIsValid)
            {
                //ClaimsIdentity identity = new ClaimsIdentity(
                //    new GenericIdentity(user.Login, "Login", new[] {
                //        new Claim(JwtRegisteredClaimNames.Jti)
                //    }))
                return SuccessObject(DateTime.Now, DateTime.Now, "");   //info: só pra não dar erro de compilação por enquanto 
            }
            else
            {
                return ExceptionObject();
            }
        }

        private object ExceptionObject()
        {
            return new
            {
                Autenticated = false,
                Message = "A autenticação falhou"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                Autenticated = true,
                Created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Expires = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };
        }
    }
}
