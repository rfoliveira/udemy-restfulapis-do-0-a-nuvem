using RestWithASPNETudemy.Data.Converter;
using RestWithASPNETudemy.Data.VO;
using RestWithASPNETudemy.Models;
using RestWithASPNETudemy.Repository.Generic;
using System.Collections.Generic;

namespace RestWithASPNETudemy.Business.Implementation
{
    public class BookBusiness: IBookBusiness
    {
        private IBaseRepository<Book> _repo;
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

        public void Delete(long id)
        {
            _repo.Delete(id);
        }

        public bool Exists(long id)
        {
            return _repo.Exists(id);
        }

        public List<BookVO> FindAll()
        {
            var books = _repo.FindAll();
            return _converter.ParseList(books);
        }

        public BookVO FindById(long id)
        {
            var bookEntity = _repo.FindById(id);
            return _converter.Parse(bookEntity);
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repo.Update(bookEntity);

            return _converter.Parse(bookEntity);
        }
    }
}
