using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Models;

namespace RestWithASPNETUdemy.Repository
{
    public interface IUserRepository
    {
         User ValidateCredentials(UserVO user);
    }
}