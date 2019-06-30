using RestWithASPNETudemy.Models;
using RestWithASPNETudemy.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETudemy.Repository
{
    public interface IPersonRepositoryNew: IBaseRepository<Person>
    {
        Person FindByFirstname(string firstName);
    }
}
