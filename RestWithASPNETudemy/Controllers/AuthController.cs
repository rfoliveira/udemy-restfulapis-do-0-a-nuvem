using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Controllers
{
    /*
        Links relevantes sobre refresh token e cancellation / revoke token
        -------------------------------------------------------------------
        
        .NET 5 + ASP.NET Core + JWT + Refresh Tokens: exemplo de implementação - Renato Groffe - 24/05/2021
        https://renatogroffe.medium.com/net-5-asp-net-core-jwt-refresh-tokens-exemplo-de-implementa%C3%A7%C3%A3o-fe885ecbaa4e

        Canceling JWT tokens in .NET Core - Piotr Gankiewicz - 25/04/2018
        https://piotrgankiewicz.com/2018/04/25/canceling-jwt-tokens-in-net-core/
    */
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _business;

        public AuthController(ILoginBusiness business)
        {
            _business = business;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null)
                return BadRequest("Invalid client request");

            var token = _business.ValidateCredentials(user);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenVO token)
        {
            if (token == null)
                return BadRequest("Invalid client request");
            
            var newToken = _business.ValidateCredentials(token);

            if (newToken == null)
                return BadRequest("Invalid client request");

            return Ok(newToken);
        }

        [HttpGet]
        [Route("revoke")]
        // Precisa estar autenticado para que ele consiga identificar qual token que ele vai revogar
        [Authorize("Bearer")]   
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var result = _business.RevokeToken(username);

            if (!result)
                return BadRequest("Invalid client request");

            return NoContent();
        }
    }
}