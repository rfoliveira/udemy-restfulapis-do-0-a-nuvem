using System.Collections.Generic;
using RestWithASPNETUdemy.Models.Base;
using System.Linq.Expressions;
using System;

namespace RestWithASPNETUdemy.Repository.Generic
{
    public interface IBaseRepository<T> where T: BaseEntity
    {
         T Create(T entity);
         T FindById(long id);
         List<T> FindAll();
         IEnumerable<T> Find(Expression<Func<T, bool>> filter = null);
         T Update(T entity);
         void Delete(long id);
         bool Exists(long? id);

         // Paginação
         List<T> FindWithPagedSearch(string query);
         int GetCount(string query);
    }
}