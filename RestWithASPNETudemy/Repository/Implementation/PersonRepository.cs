using System.Linq;
using RestWithASPNETUdemy.Models;
using RestWithASPNETUdemy.Models.Context;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Repository.Implementation
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context)
        {
        }

        public Person Disable(long id)
        {
            var personEntity = context.Persons.FirstOrDefault(p => p.Id == id);

            if (personEntity == null) return null;

            try
            {
                personEntity.Enabled = false;
                base.Update(personEntity);    
            }
            catch (System.Exception)
            {
                 throw;
            }

            return personEntity;
        }
    }
}