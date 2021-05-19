using System.Collections.Generic;
using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);
        BookVO FindById(long id);
        IEnumerable<BookVO> FindAll();
        BookVO Update(BookVO book);
        void Delete(long id);
        bool Exists(long id);
    }
}