using System.Collections.Generic;
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
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        [TypeFilter(typeof(HypermediaFilter))]
        [ProducesResponseType(typeof(IEnumerable<PersonVO>), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            var persons = _personBusiness.FindAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HypermediaFilter))]
        [ProducesResponseType(typeof(PersonVO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);

            if (person?.Id == null)
                return NotFound($"Person not found with id {id}");

            return Ok(person);
        }

        [HttpPost]
        [TypeFilter(typeof(HypermediaFilter))]
        [ProducesResponseType(typeof(PersonVO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null)
                return BadRequest("Invalid person");

            return new CreatedResult("", _personBusiness.Create(person));
        }

        [HttpPut]
        [TypeFilter(typeof(HypermediaFilter))]
        [ProducesResponseType(typeof(PersonVO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null || person.Id <= 0)
                return BadRequest("Invalid person");
            
            var personEntity = _personBusiness.FindById(person.Id.Value); 

            if (personEntity == null)
                return NotFound($"Person with id {person.Id.Value} not found");

            return new ObjectResult(_personBusiness.Update(person));
        }

        // DELETE não precisa de filtro porque não retorna nada
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Delete(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null)
                return NotFound($"Person with id {id} not found");
            
            _personBusiness.Delete(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(PersonVO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Disable(long id)
        {
            var person = _personBusiness.Disable(id);

            if (person == null)
                return NotFound($"Person with id {id} not found");

            return Ok(person);
        }    
    }
}