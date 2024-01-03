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
using ModelProject.Func;

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


		public ConfigurationBackUp AddOrUpdate(ConfigurationBackUp model)
		{
			try
			{
				var data = FirstOrDefault(model.Id);
				if (data == null)
				{
					Add(model);
				}
				else
				{
				
					Update(model);

                 var datas = repository
                .Where(predicate: x => x.IsDeleted != true && x.DatabaseConnect.DatabaseName == data.DatabaseConnect.DatabaseName &&
                x.DatabaseConnect.ServerConnects.ServerName == data.DatabaseConnect.ServerConnects.ServerName,
                include: x => x.Include(x => x.BackUpSetting)
                .Include(x => x.DatabaseConnect).ThenInclude(x => x.ServerConnects),
                disableTracking: false).ToList();
					datas.ForEach(x => {
						x.BackUpSetting.Path = model.BackUpSetting.Path;

					}) ;
                    _uniOfWork.SaveChanges();
                }
				return model;
			}
			catch (Exception ex)
			{

				throw ex;
			}

		}


		public ConfigurationBackUp Add(ConfigurationBackUp model)
		{
			try
			{
				var data = FirstOrDefault(model.Id);
				if (data == null)
				{
					if (model.FTPSetting != null)
					{
						model.FTPSetting.PassWord = EncryptionSecurity.EncryptV2(model.FTPSetting.PassWord);
					}

					repository.Add(model);
					model.BackUpSetting.ConfigurationBackUpId = model.Id;
					model.EmailConfirmation.ConfigurationBackUpId = model.Id;
					model.FTPSetting.ConfigurationBackUpId = model.Id;
					model.ScheduleBackup.ConfigurationBackUpId = model.Id;
					_uniOfWork.SaveChanges();
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
			return model;
		}
		public ConfigurationBackUp? FirstOrDefault(string JobName, string DatabaseName, string ServerName)
		{
			// var data = context.ProductBrands.Where(c => c.BrandId.Contains(id)).FirstOrDefault();
			var data = repository.Where(predicate: x => x.IsDeleted != true
				 && x.BackupName == JobName,
				 include: x => x.Include(x => x.BackUpSetting)
				.Include(x => x.ScheduleBackup)
				.Include(x => x.EmailConfirmation)
				.Include(x => x.FTPSetting)
				.Include(x => x.DatabaseConnect).ThenInclude(x => x.ServerConnects),
				 disableTracking: true)
				.FirstOrDefault(x => x.DatabaseConnect.DatabaseName == DatabaseName && x.DatabaseConnect.ServerConnects.ServerName == ServerName);
			if (data != null)
			{
				data.FTPSetting.PassWord = EncryptionSecurity.DecryptV2(data.FTPSetting.PassWord);
                if (data.DatabaseConnect != null && data.DatabaseConnect.ServerConnects != null)
                {
                    data.DatabaseConnect.ServerConnects.PassWord = EncryptionSecurity.DecryptV2(data.DatabaseConnect.ServerConnects.PassWord);
                }
            }
			return data;
		}
        public ConfigurationBackUp? FirstOrDefault(Guid Id)
        {
            // var data = context.ProductBrands.Where(c => c.BrandId.Contains(id)).FirstOrDefault();
            var data = repository.Where(predicate: x => x.IsDeleted != true
                 && x.Id == Id,
                 include: x => x.Include(x => x.BackUpSetting)
                .Include(x => x.ScheduleBackup)
                .Include(x => x.EmailConfirmation)
                .Include(x => x.FTPSetting)
                .Include(x => x.DatabaseConnect).ThenInclude(x => x.ServerConnects),
                 disableTracking: true)
                .FirstOrDefault();
            if (data != null)
            {
                data.FTPSetting.PassWord = EncryptionSecurity.DecryptV2(data.FTPSetting.PassWord);
                if(data.DatabaseConnect != null && data.DatabaseConnect.ServerConnects != null)
				{
                    data.DatabaseConnect.ServerConnects.PassWord = EncryptionSecurity.DecryptV2(data.DatabaseConnect.ServerConnects.PassWord);
                }
            }
            return data;
        }
        public List<ConfigurationBackUp> GetData()
		{
			var data = repository
				.Where(predicate: x => x.IsDeleted != true,
				 include: x => x.Include(x => x.BackUpSetting)
				.Include(x => x.ScheduleBackup)
				.Include(x => x.FTPSetting)
				.Include(x => x.EmailConfirmation)
				.Include(x => x.DatabaseConnect).ThenInclude(x => x.ServerConnects),
				 disableTracking: true).ToList();
			data.ForEach(x =>
			{
				x.FTPSetting.PassWord = EncryptionSecurity.DecryptV2(x.FTPSetting.PassWord);
                if (x.DatabaseConnect != null && x.DatabaseConnect.ServerConnects != null)
                {
                    x.DatabaseConnect.ServerConnects.PassWord = EncryptionSecurity.DecryptV2(x.DatabaseConnect.ServerConnects.PassWord);
                }
            });

			return data;
		}

        public List<ConfigurationBackUp> GetData(string SeverName, string DatabaseName)
        {
            var data = repository
                .Where(predicate: x => x.IsDeleted != true && x.DatabaseConnect.DatabaseName == DatabaseName &&
				x.DatabaseConnect.ServerConnects.ServerName == SeverName,
                 include: x => x.Include(x => x.BackUpSetting)
                .Include(x => x.ScheduleBackup)
                .Include(x => x.FTPSetting)
                .Include(x => x.EmailConfirmation)
                .Include(x => x.DatabaseConnect).ThenInclude(x => x.ServerConnects),
                 disableTracking: true).ToList();
            data.ForEach(x =>
            {
                x.FTPSetting.PassWord = EncryptionSecurity.DecryptV2(x.FTPSetting.PassWord);
                if (x.DatabaseConnect != null && x.DatabaseConnect.ServerConnects != null)
                {
                    x.DatabaseConnect.ServerConnects.PassWord = EncryptionSecurity.DecryptV2(x.DatabaseConnect.ServerConnects.PassWord);
                }
            });

            return data;
        }

        public List<ConfigurationBackUp> GetData(Guid id)
        {
            var data = repository
                .Where(predicate: x => x.IsDeleted != true && x.Id == id,
                 include: x => x.Include(x => x.BackUpSetting)
                .Include(x => x.ScheduleBackup)
                .Include(x => x.FTPSetting)
                .Include(x => x.EmailConfirmation)
                .Include(x => x.DatabaseConnect).ThenInclude(x => x.ServerConnects),
                 disableTracking: true).ToList();
            data.ForEach(x =>
            {
                x.FTPSetting.PassWord = EncryptionSecurity.DecryptV2(x.FTPSetting.PassWord);
                if (x.DatabaseConnect != null && x.DatabaseConnect.ServerConnects != null)
                {
                    x.DatabaseConnect.ServerConnects.PassWord = EncryptionSecurity.DecryptV2(x.DatabaseConnect.ServerConnects.PassWord);
                }
            });

            return data;
        }

        public ConfigurationBackUp Update(ConfigurationBackUp model)
		{
            if (model.FTPSetting != null)
            {
                model.FTPSetting.PassWord = EncryptionSecurity.EncryptV2(model.FTPSetting.PassWord);
            }
			if (model.DatabaseConnect != null && model.DatabaseConnect.ServerConnects != null)
			{
				model.DatabaseConnect.ServerConnects.PassWord = EncryptionSecurity.DecryptV2(model.DatabaseConnect.ServerConnects.PassWord);
			}
			repository.Update(model);
            _uniOfWork.SaveChanges();
            return model;
		}

		public ConfigurationBackUp Delete(ConfigurationBackUp model)
		{
			repository.Delete(model);
            _uniOfWork.SaveChanges();
            return model;
		}

	}
}
