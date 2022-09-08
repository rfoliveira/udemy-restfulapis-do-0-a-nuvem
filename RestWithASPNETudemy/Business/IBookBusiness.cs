using System.Collections.Generic;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Utils;

namespace RestWithASPNETUdemy.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);
        BookVO FindById(int id);
        IEnumerable<BookVO> FindAll();
        BookVO Update(BookVO book);
        void Delete(int id);
        bool Exists(int id);

        PagedSearchVO<BookVO> FindWithPagedSearch(string title, string sortDirection, int pagesize, int page);
        IEnumerable<BookVO> FindByAuthor(string author);
    }
}