using System.Collections.Generic;
using RestWithASPNETUdemy.Data.Converter;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Utils;
using RestWithASPNETUdemy.Repository;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IBookRepository _repo;
        private readonly BookConverter _converter;

        public BookBusiness(IBookRepository repo)
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

        public IEnumerable<BookVO> FindByAuthor(string author) => _converter.ParseList(_repo.FindByAuthor(author));

        public BookVO FindById(long id) => _converter.Parse(_repo.FindById(id));

        public PagedSearchVO<BookVO> FindWithPagedSearch(string title, string sortDirection, int pagesize, int page)
        {
            var offset = page > 0 ? page - 1 : 0;
            var sort = (string.IsNullOrEmpty(sortDirection) || sortDirection != "desc") ? "asc" : sortDirection;
            var size = (pagesize < 1) ? 1 : pagesize;

            // Camada de business não deve conter SQL, mas foi assim que o instrutor usou.
            // TODO: formatar para apenas passar os valores para o repositório
            var query = "select id, Author, LaunchDate, Price, Title " +
                        "from books " +
                        $"where title like '%{title}%' " + 
                        $"order by title {sort} " + 
                        $"limit {size} offset {offset}";

            var books = _repo.FindWithPagedSearch(query);

            var countQuery = "select count(*) from books";
            int totalResults = _repo.GetCount(countQuery);

            return new PagedSearchVO<BookVO> 
            {
                CurrentPage = offset,
                List = _converter.ParseList(books),
                PageSize = size,
                SortDirection = sortDirection,
                TotalResults = totalResults
            };
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repo.Update(bookEntity);

            return _converter.Parse(bookEntity);
        }
    }
}