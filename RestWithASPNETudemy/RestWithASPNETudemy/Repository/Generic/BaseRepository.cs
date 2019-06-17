using Microsoft.EntityFrameworkCore;
using RestWithASPNETudemy.Models.Base;
using RestWithASPNETudemy.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETudemy.Repository.Generic
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly MySQLContext _context;
        private DbSet<T> _entity;

        public BaseRepository(MySQLContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public T Create(T entity)
        {
            try
            {
                _entity.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entity;
        }

        public void Delete(long id)
        {            
            try
            {
                var oldEntity = _entity.FirstOrDefault(e => e.Id == id);

                if (oldEntity != null)
                {
                    _entity.Remove(oldEntity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(long? id)
        {
            return _entity.Any(e => e.Id.Equals(id));
        }

        public List<T> FindAll()
        {
            return _entity.ToList();
        }

        public T FindById(long id)
        {
            return _entity.FirstOrDefault(e => e.Id == id);
        }

        public T Update(T entity)
        {
            var newEntity = _entity.FirstOrDefault(e => e.Id == entity.Id);

            try
            {
                if (newEntity != null)
                {
                    _context.Entry(newEntity).CurrentValues.SetValues(entity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newEntity;
        }
    }
}
