using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Models.Base;
using RestWithASPNETUdemy.Models.Context;

namespace RestWithASPNETUdemy.Repository.Generic
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly MySQLContext _context;
        private readonly DbSet<T> _entity;
        
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
            catch (DbUpdateConcurrencyException) 
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (Exception)
            {
                /*
                    Para remover o warning na versão aspnet core 5.0, que diz que 
                    os exceptions devem ser tratados explicitadamente e não repassados no catch.
                    warning CA2200 = Gerar novamente uma exceção detectada altera as informações de pilha
                 throw ex;
                 */
                 throw;
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
            catch (DbUpdateConcurrencyException) 
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (Exception)
            {
                /*
                    Para remover o warning na versão aspnet core 5.0, que diz que 
                    os exceptions devem ser tratados explicitadamente e não repassados no catch.
                    warning CA2200 = Gerar novamente uma exceção detectada altera as informações de pilha
                 throw ex;
                 */
                 throw;
            }
        }

        public bool Exists(long? id)
        {
            return _entity.Any(e => e.Id == id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
                return _entity.Where(filter);

            return FindAll();
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
            catch (DbUpdateConcurrencyException) 
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (Exception)
            {
                /*
                    Para remover o warning na versão aspnet core 5.0, que diz que 
                    os exceptions devem ser tratados explicitadamente e não repassados no catch.
                    warning CA2200 = Gerar novamente uma exceção detectada altera as informações de pilha
                 throw ex;
                 */
                 throw;
            }

            return newEntity;
        }
    }
}