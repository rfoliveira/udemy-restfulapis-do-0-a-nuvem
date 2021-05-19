using System.Linq;
using RestWithASPNETUdemy.Models;
using RestWithASPNETUdemy.Models.Context;

namespace RestWithASPNETUdemy.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

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