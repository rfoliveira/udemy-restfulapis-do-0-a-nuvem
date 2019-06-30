using RestWithASPNETudemy.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RestWithASPNETudemy.Repository.Generic
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
    }
}
