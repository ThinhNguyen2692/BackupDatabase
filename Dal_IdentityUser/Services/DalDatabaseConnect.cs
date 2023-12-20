using DalBackup.Interface;
using DalBackup.Repository;
using Microsoft.EntityFrameworkCore;
using ModelProject.Func;
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

        public DatabaseConnect? FirstOrDefault(Guid id)
        {
            var data = repository.Where(x => x.Id == id, include: x => x.Include(p => p.ServerConnects)).FirstOrDefault();
			if (data != null)
			{
				data.ServerConnects.PassWord = EncryptionSecurity.DecryptV2(data.ServerConnects.PassWord);
			}
			return data;
        }
        public DatabaseConnect? FirstOrDefault(string DatabaseName)
        {
            var data = repository.Where(x => x.DatabaseName == DatabaseName, include: x => x.Include(p => p.ServerConnects)).FirstOrDefault();
			if (data != null)
			{
				data.ServerConnects.PassWord = EncryptionSecurity.DecryptV2(data.ServerConnects.PassWord);
			}
			return data;
        }

    }
}
