using DalBackup.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalBackup.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AdminLayout_VuexyContext _context;
        private Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public UnitOfWork(AdminLayout_VuexyContext context)
        {
            _context = context;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Repository<T> Repository<T>() where T : class
        {
            IRepository<T> repository = null;
            if (repositories.ContainsKey(typeof(T)))
            {
                repository = repositories[typeof(T)] as IRepository<T>;
            }
            else
            {
                repository = new Repository<T>(_context);
                repositories.Add(typeof(T), repository);
            }

            return (Repository<T>)repository;

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
