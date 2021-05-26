using System.Collections.Generic;
using System.Linq;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Models;

namespace RestWithASPNETUdemy.Data.Converter
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
                Address = origin.Address,
                Genre = origin.Genre,
                Enabled = origin.Enabled
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
                Address = origin.Address,
                Genre = origin.Genre,
                Enabled = origin.Enabled
            };
        }

        public List<Person> ParseList(List<PersonVO> originList)
        {
            if (originList == null) return new List<Person>();

            return originList.Select(o => Parse(o)).ToList();
        }

        public List<PersonVO> ParseList(List<Person> originList)
        {
            if (originList == null) return new List<PersonVO>();

            return originList.Select(o => Parse(o)).ToList();
        }
    }
}