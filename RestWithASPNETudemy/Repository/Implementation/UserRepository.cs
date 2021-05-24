using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Data.VO;
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

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == pass);
        }

        private string ComputeHash(string password, SHA256CryptoServiceProvider algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id == user.Id))
                return null;

            var userEntity = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            try
            {
                _context.Entry(userEntity).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) 
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (Exception)
            {
                /*
                    Para remover o warning na versão aspnet core 5.0, que diz que 
                    os exceptions devem ser tratados explicitadamente e não repassados no catch.
                    warning CA2200 = Gerar novamente uma exceção detectada altera as informações de pilha
                throw ex;
                */
                throw;
            }

            return userEntity;
        }
    }
}