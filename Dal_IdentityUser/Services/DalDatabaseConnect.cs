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

        public DatabaseConnect AddOrUpdate(DatabaseConnect model)
        {
            var data = FirstOrDefault(model.ServerConnects.ServerName, model.DatabaseName);
            if (data == null)
            {
                Add(model);
            }
            else
            {
                Update(model);
            }
            return model;
        }

        public DatabaseConnect Add(DatabaseConnect model)
        {
            var data = FirstOrDefault(model.ServerConnects.ServerName, model.DatabaseName);
            if (data == null)
            {
                repository.Add(model);
                _uniOfWork.SaveChanges();
            }
            return model;
        }

        public bool Update(DatabaseConnect model)
        {
            try
            {
                var data = FirstOrDefault(model.Id);
                if (data != null)
                {
                    repository.Update(model);
                    _uniOfWork.SaveChanges();
                    return true;
                } else
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
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
        public DatabaseConnect? FirstOrDefault(string ServerName, string DatabaseName)
        {
            var data = repository
                .Where(x => x.DatabaseName == DatabaseName && x.ServerConnects.ServerName == ServerName, 
                include: x => x.Include(p => p.ServerConnects))
                .FirstOrDefault();
			if (data != null)
			{
				data.ServerConnects.PassWord = EncryptionSecurity.DecryptV2(data.ServerConnects.PassWord);
			}
			return data;
        }

        public List<Guid> GetIdByServerId(Guid ServerId)
        {
            var data = repository
               .Where(predicate: x => x.IsDeleted != true && x.ServerConnectId == ServerId,
                disableTracking: true).Select(x => x.Id).ToList();
            return data ?? new List<Guid>();
        }

    }
}
