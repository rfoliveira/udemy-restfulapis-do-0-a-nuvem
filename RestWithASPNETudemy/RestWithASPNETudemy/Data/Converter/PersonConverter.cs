using RestWithASPNETudemy.Data.VO;
using RestWithASPNETudemy.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETudemy.Data.Converter
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if (origin == null) return new Person();

            return new Person
            {
                Id = origin.Id,
                Firstname = origin.Firstname,
                Lastname = origin.Lastname,
                Genre = origin.Genre
            };
        }

        public PersonVO Parse(Person origin)
        {
            if (origin == null) return new PersonVO();

            return new PersonVO
            {
                Id = origin.Id,
                Firstname = origin.Firstname,
                Lastname = origin.Lastname,
                Genre = origin.Genre
            };
        }

        public List<Person> ParseList(List<PersonVO> origin)
        {
            if (origin == null) return new List<Person>();

            return origin.Select(item => Parse(item)).ToList();
        }

        public List<PersonVO> ParseList(List<Person> origin)
        {
            if (origin == null) return new List<PersonVO>();

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
