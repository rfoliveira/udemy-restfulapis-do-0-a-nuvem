using RestWithASPNETudemy.Models;
using RestWithASPNETudemy.Models.Context;
using RestWithASPNETudemy.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETudemy.Repository.Implementation
{
    public class PersonRepositoryNew : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepositoryNew(MySQLContext context) : base(context)
        {
        }

        public bool Exists(long id)
        {
            throw new NotImplementedException();
        }

        public Person FindByFirstname(string firstName)
        {
            throw new NotImplementedException();
        }

        public override void Delete(long id)
        {
            var person = FindById(id);

            try
            {
                if (person == null)
                    throw new Exception($"Pessoa com id {id} não encontrado");

                Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
