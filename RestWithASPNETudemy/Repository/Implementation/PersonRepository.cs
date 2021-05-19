using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public IEnumerable<Person> FindByFirstName(string firstname)
        {
            Expression<Func<Person, bool>> filterByName = p => p.Firstname.Contains(firstname);
            return base.Find(filterByName);
        }
    }
}