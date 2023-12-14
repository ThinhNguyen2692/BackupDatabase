using DalBackup.Interface;
using DalBackup.Repository;
using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalBackup.Services
{
    public class DalDatabaseConnect : IDalDatabaseConnect
    {
        private IRepository<DatabaseConnect> repository;
        private IUnitOfWork _uniOfWork;
        public DalDatabaseConnect(IUnitOfWork uniOfWork)
        {
            _uniOfWork = uniOfWork;
            repository = _uniOfWork.Repository<DatabaseConnect>();
        }

        public DatabaseConnect Add(DatabaseConnect model)
        {
            var data = FirstOrDefault(model.DatabaseName);
            if (data == null)
            {
                repository.Add(model);
                _uniOfWork.SaveChanges();
            }
            return model;
        }

        public DatabaseConnect FirstOrDefault(Guid id)
        {
            var data = repository.FirstOrDefaultAsNoTracking(x => x.Id == id);
            return data;
        }
        public DatabaseConnect FirstOrDefault(string DatabaseName)
        {
            var data = repository.FirstOrDefaultAsNoTracking(x => x.DatabaseName == DatabaseName);
            return data;
        }

    }
}
