using DalBackup.Data;
using ModelProject.Func;
using ModelProject.Models;
using NuGet.Protocol.Core.Types;
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
            _context.Dispose();
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
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "SaveConnectionAsync", DateTime.Now.ToString("ddMMyyyy")),
              ex.Message, Setting.FoderBackUp);
                WriteLogFile.WriteLog(string.Format("{0}{1}", "SaveConnectionAsync", DateTime.Now.ToString("ddMMyyyy")),
                ex.InnerException?.Message, Setting.FoderBackUp);

              
                throw;
            }
            
        }
    }
}
