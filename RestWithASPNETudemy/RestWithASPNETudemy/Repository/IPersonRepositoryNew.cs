using RestWithASPNETudemy.Models;
using RestWithASPNETudemy.Repository.Generic;

namespace RestWithASPNETudemy.Repository
{
    public interface IPersonRepositoryNew: IBaseRepository<Person>
    {
        Person FindByFirstname(string firstName);
    }
}
