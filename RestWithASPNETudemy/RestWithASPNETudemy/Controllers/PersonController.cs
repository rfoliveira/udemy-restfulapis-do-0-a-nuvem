using Microsoft.AspNetCore.Mvc;
using RestWithASPNETudemy.Business;
using RestWithASPNETudemy.Data.VO;
using RestWithASPNETudemy.Models;
using Tapioca.HATEOAS;

namespace RestWithASPNETudemy.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonController : ControllerBase
    {
        private IPersonBusiness _personBusiness;

        public PersonController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        [HttpGet]
        //[TypeFilter(typeof(HyperMediaFilter))]    //não funcionou...
        public IActionResult Get()
        {
            var persons = _personBusiness.FindAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        //[TypeFilter(typeof(HyperMediaFilter))]    //não funcionou...
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);

            if (person == null)
                return NotFound($"Person not found with id {id}");

            return Ok(person);
        }

        [HttpPost]
        //[TypeFilter(typeof(HyperMediaFilter))]    //não funcionou...
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null)
                return BadRequest("Invalid person");

            return new CreatedResult("", _personBusiness.Create(person));
        }

        [HttpPut]
        //[TypeFilter(typeof(HyperMediaFilter))]    //não funcionou...
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null)
                return BadRequest("Invalid person");

            return new ObjectResult(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        //[TypeFilter(typeof(HyperMediaFilter))]    //não funcionou...
        public IActionResult Delete(long id)
        {
            var person = _personBusiness.FindById(id);

            if (person == null)
                return NotFound();

            _personBusiness.Delete(id);

            return NoContent();
        }
    }
}
