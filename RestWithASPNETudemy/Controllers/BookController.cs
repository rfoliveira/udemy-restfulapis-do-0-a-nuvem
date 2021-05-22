using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookBusiness _business;

        public BookController(ILogger<BookController> logger, IBookBusiness business)
        {
            _logger = logger;
            _business = business;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var books = _business.FindAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var book = _business.FindById(id);

            if (book?.Id == null)
                return NotFound($"Book with id {id} not found");

            return Ok(book);            
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null)
                return BadRequest("Invalid book");

            return new CreatedResult("", _business.Create(book));
        }

        [HttpPut]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null)
                return BadRequest("Invalid book");

            var bookOld = _business.FindById(book.Id.Value);

            if (bookOld.Id == null)
                return NotFound($"Book with id {book.Id} not found");

            return new ObjectResult(_business.Update(book));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id <= 0)
                return BadRequest("Invalid book id");

            var bookOld = _business.FindById(id);

            if (bookOld.Id == null)
                return NotFound($"Book with id {id} not found");

            _business.Delete(id);
            return NoContent();
        }
    }
}