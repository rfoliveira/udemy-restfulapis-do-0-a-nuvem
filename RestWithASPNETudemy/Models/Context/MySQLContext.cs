using Microsoft.EntityFrameworkCore;

namespace RestWithASPNETUdemy.Models.Context
{
    public class MySQLContext: DbContext
    {
        public MySQLContext() { }
        public MySQLContext(DbContextOptions options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}