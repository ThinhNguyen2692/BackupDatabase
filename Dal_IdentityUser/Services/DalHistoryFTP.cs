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
	public class DalHistoryFTP : IDalHistoryFTP
	{
		private readonly IRepository<HistoryFTP> repository;
		private readonly IUnitOfWork _uniOfWork;
		public DalHistoryFTP(IUnitOfWork uniOfWork)
		{
			_uniOfWork = uniOfWork;
			repository = _uniOfWork.Repository<HistoryFTP>();
		}
		
		public HistoryFTP Add(HistoryFTP model)
		{
			var data = FirstOrDefault(model.Id);
			if (data == null)
			{
				repository.Add(model);
				return model;
			}
			return model;
		}

		public HistoryFTP Update(HistoryFTP model)
		{
			var data = FirstOrDefault(model.Id);
			if (data != null)
			{
				repository.Update(model);
				return model;
			}
			return model;
		}
		
		public HistoryFTP? FirstOrDefault(Guid id) {
			var data = repository.FirstOrDefaultAsNoTracking(x => x.Id == id);
			return data;
		}

		public HistoryFTP? FirstOrDefault(string jobName)
		{
			var data = repository.FirstOrDefaultAsNoTracking(x => x.JobName == jobName);
			return data;
		}

		public List<HistoryFTP> GetHistoryFTPs()
		{
			var data = repository.Where(predicate: x => !x.IsDeleted, disableTracking: true).ToList();
			return data;
		}
	}
}
