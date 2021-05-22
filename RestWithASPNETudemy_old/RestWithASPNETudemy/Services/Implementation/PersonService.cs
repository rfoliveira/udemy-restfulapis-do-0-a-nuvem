using RestWithASPNETudemy.Models;
using RestWithASPNETudemy.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETudemy.Services.Implementation
{
    public class PersonService : IPersonService
    {
        private MySQLContext _context;

        public PersonService(MySQLContext context)
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

        public Person FindById(long id)
        {
            var person = _context.Persons.Where(p => p.Id == id).FirstOrDefault();

            return person;
        }

        public List<Person> GetAll()
        {
            return _context.Persons.ToList();
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
