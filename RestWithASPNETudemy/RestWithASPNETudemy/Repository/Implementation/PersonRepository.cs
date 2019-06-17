using RestWithASPNETudemy.Models;
using RestWithASPNETudemy.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETudemy.Repository.Implementation
{
    public class PersonRepository: IPersonRepository
    {
        private MySQLContext _context;

        public PersonRepository(MySQLContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return person;
        }

        public void Delete(long id)
        {
            var person = _context.Persons.Where(p => p.Id == id).FirstOrDefault();

            try
            {
                if (person == null)
                    throw new Exception($"Pessoa com id {id} não encontrado");

                _context.Persons.Remove(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id == id);
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindByFirstname(string firstName)
        {
            return _context.Persons.FirstOrDefault(p => p.Firstname == firstName);
        }

        public Person FindById(long id)
        {
            var person = _context.Persons.Where(p => p.Id == id).FirstOrDefault();

            return person;
        }

        public Person Update(Person person)
        {
            var personOri = _context.Persons.Where(p => p.Id == person.Id).FirstOrDefault();

            try
            {
                if (personOri == null)
                    throw new Exception($"Pessoa com id {person.Id} não encontrado");

                _context.Entry(personOri).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return personOri;
        }
    }
}
