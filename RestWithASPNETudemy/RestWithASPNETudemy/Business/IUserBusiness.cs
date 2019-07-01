using RestWithASPNETudemy.Models;

namespace RestWithASPNETudemy.Business
{
    public interface IUserBusiness
    {
        object FindByLogin(User user);
    }
}
