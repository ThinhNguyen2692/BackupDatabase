using DalBackup.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DalBackup.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private AdminLayout_VuexyContext _context;

        public Repository(AdminLayout_VuexyContext _context)
        {
            this._context = _context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);

        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            var entity = _context.Set<T>().FirstOrDefault(predicate);
            if (entity == null) return null;
            return entity;
        }

        public IEnumerable<T> List()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public IQueryable<T> Where(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            return includes.Aggregate(query, (q, w) => q.Include(w));
        }
        public IQueryable<T> Where(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            var query = _context.Set<T>().AsQueryable();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public void Detached(T entity)
        {

            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
