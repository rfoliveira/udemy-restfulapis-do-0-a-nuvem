using RestWithASPNETudemy.Data.VO;
using RestWithASPNETudemy.Models;
using System.Collections.Generic;

namespace RestWithASPNETudemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        void Delete(long id);

        bool Exists(long id);
    }
}
