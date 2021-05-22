using RestWithASPNETudemy.Models;
using System;
using System.Collections.Generic;

namespace RestWithASPNETudemy.Services.Implementation
{
    public class PersonMockService : IPersonService
    {
        private static List<Person> _personsMock = new List<Person>()
        {
            new Person
            {
                Id = 1,
                Firstname = "Fulano",
                Lastname = "da Silva",
                Address = "Algum lugar por aí, 111",
                Gender = "Male"
            },
            new Person
            {
                Id = 2,
                Firstname = "Beltrano",
                Lastname = "da Silva",
                Address = "Algum lugar por aí, 222",
                Gender = "Male"
            },
            new Person
            {
                Id = 3,
                Firstname = "Ciclana",
                Lastname = "da Silva",
                Address = "Algum lugar por aí, 333",
                Gender = "Female"
            }
        };

        public Person Create(Person person)
        {
            person.Id = _personsMock.Count + 1;

            _personsMock.Add(person);
            return person;
        }

        public void Delete(long id)
        {
            var person = _personsMock.Find(p => p.Id == id);

            if (person != null)
                _personsMock.Remove(person);
        }

        public Person FindById(long id)
        {
            return _personsMock.Find(p => p.Id == id);
        }

        public List<Person> GetAll()
        {
            return _personsMock;
        }

        public Person Update(Person person)
        {
            var personOri = _personsMock.Find(p => p.Id == person.Id);

            if (personOri != null)
            {
                personOri.Firstname = person.Firstname;
                personOri.Lastname = person.Lastname;
                personOri.Address = person.Address;
                personOri.Gender = person.Gender;
            }

            return person;
        }
    }
}
