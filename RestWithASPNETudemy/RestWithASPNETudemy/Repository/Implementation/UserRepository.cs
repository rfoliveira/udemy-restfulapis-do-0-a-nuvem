using RestWithASPNETudemy.Models;
using RestWithASPNETudemy.Models.Context;
using System.Linq;

namespace RestWithASPNETudemy.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User FindByLogin(string login)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login);
        }
    }
}
