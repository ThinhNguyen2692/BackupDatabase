using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DalBackup.Repository
{
    public interface IRepository<T> where T : class
    {
        T FirstOrDefault(Func<T, bool> predicate);
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void Attach(T entity);

        public IQueryable<T> Where(params Expression<Func<T, object>>[] includes);
        public IQueryable<T> Where(
           Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true, bool ignoreQueryFilters = false);
        public void Detached(T entity);
    }
}
