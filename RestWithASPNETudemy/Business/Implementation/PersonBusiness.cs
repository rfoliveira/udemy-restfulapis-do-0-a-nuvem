using System;
using System.Collections.Generic;
using RestWithASPNETUdemy.Data.Converter;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Models;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class PersonBusiness : IPersonBusiness
    {
        // private readonly IBaseRepository<Person> _repo;
        private readonly IPersonRepository _repo;
        private readonly PersonConverter _converter;

        public PersonBusiness()
        {
        }

        public PersonBusiness(IPersonRepository repo)
        {
            _repo = repo;
            _converter = new PersonConverter();
        }
        
        public PersonVO Create(PersonVO person)
        {
            try
            {
                // Só pra fins de teste apenas, 
                // em um mundo real a validação consideraria os atributos do objeto
                if (string.IsNullOrEmpty(person.Firstname))
                    throw new System.Exception("Firstname is required");
                else if (string.IsNullOrEmpty(person.Lastname))
                    throw new System.Exception("Lastname is required");
                else if (string.IsNullOrEmpty(person.Address))
                    throw new System.Exception("Address is required");

                //if (_repo.FindByFirstname(person.Firstname) != null)
                //    throw new System.Exception("Firstname already exists");

                var personEntity = _converter.Parse(person);
                personEntity = _repo.Create(personEntity);

                return _converter.Parse(personEntity);
            }
            catch (Exception)
            {
                 throw;
            }
        }

        public void Delete(long id) => _repo.Delete(id);

        public PersonVO Disable(long id) => _converter.Parse(_repo.Disable(id));

        public bool Exists(long id) => _repo.Exists(id);
        
        public IEnumerable<PersonVO> FindAll() => _converter.ParseList(_repo.FindAll());
        
        public PersonVO FindById(long id) => _converter.Parse(_repo.FindById(id));

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repo.Update(personEntity);

            return _converter.Parse(personEntity);
        }
    }
}