using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETudemy.Business;
using RestWithASPNETudemy.Models;

namespace RestWithASPNETudemy.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserBusiness _business;

        public UsersController(IUserBusiness business)
        {
            _business = business;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignIn([FromBody]User user)
        {
            if (user == null) return BadRequest();

            var usrLoggedIn = _business.FindByLogin(user);
            return Ok(usrLoggedIn);
        }
    }
}
