using System.Collections.Generic;
using System.Linq;
using RestWithASPNETUdemy.Models;
using RestWithASPNETUdemy.Models.Context;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Repository.Implementation
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(MySQLContext context) : base(context)
        {
        }

        public List<Book> FindByAuthor(string author) => 
            context.Books.Where(b => b.Author.Contains(author)).ToList();
    }
}