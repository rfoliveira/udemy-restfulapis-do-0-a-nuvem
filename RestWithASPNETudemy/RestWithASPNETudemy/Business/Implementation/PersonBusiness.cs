using RestWithASPNETudemy.Models;
using RestWithASPNETudemy.Repository;
using System.Collections.Generic;

namespace RestWithASPNETudemy.Business.Implementation
{
    public class PersonBusiness: IPersonBusiness
    {
        private IPersonRepository _repo;

        public PersonBusiness(IPersonRepository repo)
        {
            _repo = repo;
        }

        public Person Create(Person person)
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

                if (_repo.FindByFirstname(person.Firstname) != null)
                    throw new System.Exception("Firstname already exists");

                return _repo.Create(person);
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

        public List<Person> FindAll()
        {
            return _repo.FindAll();
        }

        public Person FindById(long id)
        {
            return _repo.FindById(id);
        }

        public Person Update(Person person)
        {
            return _repo.Update(person);
        }
    }
}
