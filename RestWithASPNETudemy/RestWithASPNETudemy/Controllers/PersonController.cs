using Microsoft.AspNetCore.Mvc;
using RestWithASPNETudemy.Models;
using RestWithASPNETudemy.Services;
using System.Collections.Generic;

namespace RestWithASPNETudemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var persons = _personService.GetAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindById(id);

            if (person == null)
                return NotFound($"Person not found with id {id}");

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null)
                return BadRequest("Invalid person");

            return new CreatedResult("", _personService.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null)
                return BadRequest("Invalid person");

            return new ObjectResult(_personService.Update(person));
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var person = _personService.FindById(id);

            if (person == null)
                return NotFound();

            _personService.Delete(id);

            return NoContent();
        }
    }
}
