using RestWithASPNETudemy.Models;
using RestWithASPNETudemy.Repository.Generic;

namespace RestWithASPNETudemy.Repository
{
    public interface IUserRepository
    {
        User FindByLogin(string login);
    }
}
