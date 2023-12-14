using DalBackup.Interface;
using DalBackup.Repository;
using HRM.SC.Core.Security;
using Microsoft.EntityFrameworkCore;
using ModelProject.Models;
using ModelProject.ViewModels.ViewModelSeverConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DalBackup.Services
{
    public class DalServerConnect : IDalServerConnect
    {
        private IRepository<ServerConnect> repository;
        private IUnitOfWork _uniOfWork;
        public DalServerConnect(IUnitOfWork uniOfWork)
        {
            _uniOfWork = uniOfWork;
            repository = _uniOfWork.Repository<ServerConnect>();
        }
        public ServerConnect Add(ServerConnect model)
        {
            var data = FirstOrDefault(model.ServerName);
            if (data == null)
            {
                //mã hóa mật khẩu
                model.PassWord = EncryptV2(model.PassWord);
                repository.Add(model);
                _uniOfWork.SaveChanges();
            }
            return model;
        }

        public ServerConnect Update(ServerConnect model)
        {
            var data = FirstOrDefault(model.ServerName);
            if (data == null)
            {
                model.PassWord = DecryptV2(model.PassWord);
                repository.Update(model);
                _uniOfWork.SaveChanges();
            }
            return model;
        }

        public ServerConnect FirstOrDefault(Guid id)
        {
            var data = repository.FirstOrDefaultAsNoTracking(x => x.Id == id);
            if(data != null)
            {
                data.PassWord = DecryptV2(data.PassWord);
            }
            return data;
        }
        public ServerConnect? FirstOrDefault(string Servername)
        {
            var data = repository.Where(predicate: x => x.ServerName == Servername, disableTracking: true, include: x => x.Include(p => p.DatabaseConnects)).FirstOrDefault();
            if (data != null)
            {
                data.PassWord = DecryptV2(data.PassWord);
            }
            return data;
        }

       private string DecryptV2(string input)
        {
            var outPut = Encryption.DecryptV2(input);
            return outPut;
        }
        private string EncryptV2(string input)
        {
            var outPut = Encryption.EncryptV2(input);
            return outPut;
        }
    }
}
