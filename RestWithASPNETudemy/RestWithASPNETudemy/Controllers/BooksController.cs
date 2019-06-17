using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNETudemy.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        
    }
}