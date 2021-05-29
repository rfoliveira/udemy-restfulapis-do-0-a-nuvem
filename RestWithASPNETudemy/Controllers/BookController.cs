using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Filters;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize("Bearer")]
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

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [TypeFilter(typeof(HypermediaFilter))]
        [ProducesResponseType(typeof(IEnumerable<BookVO>), (int)HttpStatusCode.OK)]
        public IActionResult Get(
            [FromQuery] string title,
            string sortDirection,
            int pageSize,
            int page
        )
        {
            var books = _business.FindWithPagedSearch(title, sortDirection, pageSize, page);
            return Ok(books);
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HypermediaFilter))]
        [ProducesResponseType(typeof(BookVO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Get(long id)
        {
            var book = _business.FindById(id);

            if (book?.Id == null)
                return NotFound($"Book with id {id} not found");

            return Ok(book);            
        }

        [HttpGet]
        [TypeFilter(typeof(HypermediaFilter))]
        [ProducesResponseType(typeof(IEnumerable<BookVO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Route("findbyauthor")]
        public IActionResult Get([FromQuery] string author)
        {
            var books = _business.FindByAuthor(author);

            if (books.Count() == 0)
                return NotFound($"Person with name {author} not found");

            return Ok(books);
        }

        [HttpPost]
        [TypeFilter(typeof(HypermediaFilter))]
        [ProducesResponseType(typeof(BookVO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null)
                return BadRequest("Invalid book");

            return new CreatedResult("", _business.Create(book));
        }

        [HttpPut]
        [TypeFilter(typeof(HypermediaFilter))]
        [ProducesResponseType(typeof(BookVO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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