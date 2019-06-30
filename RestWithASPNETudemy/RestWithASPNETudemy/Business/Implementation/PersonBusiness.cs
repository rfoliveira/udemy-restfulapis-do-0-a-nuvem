using RestWithASPNETudemy.Data.Converter;
using RestWithASPNETudemy.Data.VO;
using RestWithASPNETudemy.Models;
using RestWithASPNETudemy.Repository;
using RestWithASPNETudemy.Repository.Generic;
using System.Collections.Generic;

namespace RestWithASPNETudemy.Business.Implementation
{
    public class PersonBusiness: IPersonBusiness
    {
        private IBaseRepository<Person> _repo;
        private readonly PersonConverter _converter;

        public PersonBusiness(IBaseRepository<Person> repo)
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
            catch (System.Exception ex)
            {
                throw ex;
            }            
        }

        public void Delete(long id)
        {
            _repo.Delete(id);
        }

        public bool Exists(long id)
        {
            return _repo.Exists(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repo.FindAll());
        }

        public PersonVO FindById(long id)
        {
            var personEntity = _repo.FindById(id);

            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repo.Update(personEntity);
            return _converter.Parse(personEntity);
        }
    }
}
