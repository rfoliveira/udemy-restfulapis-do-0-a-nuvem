using System.Collections.Generic;
using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Business
{
    public interface IPersonBusiness
    {
         PersonVO Create(PersonVO person);
         PersonVO FindById(long id);
         IEnumerable<PersonVO> FindAll();
         PersonVO Update(PersonVO person);
         void Delete(long id);
         bool Exists(long id);
    }
}