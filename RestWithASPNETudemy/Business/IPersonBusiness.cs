using System.Collections.Generic;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Utils;

namespace RestWithASPNETUdemy.Business
{
    public interface IPersonBusiness
    {
         PersonVO Create(PersonVO person);
         PersonVO FindById(int id);
         IEnumerable<PersonVO> FindAll();
         PersonVO Update(PersonVO person);
         void Delete(int id);
         bool Exists(int id);
         PersonVO Disable(int id);
         IEnumerable<PersonVO> FindByName(string name);

         PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string direction, int pagesize, int page);
    }
}