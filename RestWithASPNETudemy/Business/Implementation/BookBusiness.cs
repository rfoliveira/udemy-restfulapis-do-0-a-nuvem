using System.Collections.Generic;
using RestWithASPNETUdemy.Data.Converter;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Models;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IBaseRepository<Book> _repo;
        private readonly BookConverter _converter;

        public BookBusiness(IBaseRepository<Book> repo)
        {
            _repo = repo;
            _converter = new BookConverter();
        }
        
        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repo.Create(bookEntity);

            return _converter.Parse(bookEntity);
        }

        public void Delete(long id) => _repo.Delete(id);

        public bool Exists(long id) => _repo.Exists(id);

        public IEnumerable<BookVO> FindAll() => _converter.ParseList(_repo.FindAll());

        public BookVO FindById(long id) => _converter.Parse(_repo.FindById(id));

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repo.Update(bookEntity);

            return _converter.Parse(bookEntity);
        }
    }
}