using DalBackup.Repository;
using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DalBackup.Interface;

namespace DalBackup.Services
{
    public class DalConfigurationBackUp : IDalConfigurationBackUp
    {
        private IRepository<ConfigurationBackUp> repository;
        private IUnitOfWork _uniOfWork;
        public DalConfigurationBackUp(IUnitOfWork uniOfWork)
        {
            _uniOfWork = uniOfWork;
            this.repository = _uniOfWork.Repository<ConfigurationBackUp>();
        }
        public ConfigurationBackUp Add(ConfigurationBackUp model)
        {
            var data = FirstOrDefaultByJobName(model.BackupName);
            if (data.Id == Guid.Empty)
            {
                repository.Add(model);
                _uniOfWork.SaveChanges();
            }
            return model;
        }
        public ConfigurationBackUp FirstOrDefaultByJobName(string JobName)
        {
            // var data = context.ProductBrands.Where(c => c.BrandId.Contains(id)).FirstOrDefault();
            var data = repository.Where(predicate: x => x.IsDeleted != true
                 && x.BackupName == JobName,
                 include: x => x.Include(x => x.BackUpSetting)
                .Include(x => x.ScheduleBackup)
                .Include(x => x.EmailConfirmation)
                .Include(x => x.DatabaseConnect),
                 disableTracking: true)              
                .FirstOrDefault();
            if (data == null) return new ConfigurationBackUp();
            return data;
        }
        public List<ConfigurationBackUp> GetData()
        {
            var data = repository
                .Where(predicate: x => x.IsDeleted != true,
                 include: x => x.Include(x => x.BackUpSetting)
                .Include(x => x.ScheduleBackup)
                .Include(x => x.EmailConfirmation)
                .Include(x => x.DatabaseConnect),
                 disableTracking: true).ToList();
            return data;
        }

        public ConfigurationBackUp Update(ConfigurationBackUp model)
        {
            repository.Update(model);         
            return model;
        }

        public ConfigurationBackUp Delete(ConfigurationBackUp model)
        {
            repository.Delete(model);
            return model;
        }

    }
}
