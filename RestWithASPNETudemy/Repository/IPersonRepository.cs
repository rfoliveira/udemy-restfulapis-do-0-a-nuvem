using System.Collections.Generic;
using RestWithASPNETUdemy.Models;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Repository
{
    public interface IPersonRepository: IBaseRepository<Person>
    {
         IEnumerable<Person> FindByFirstName(string firstname);
    }
}