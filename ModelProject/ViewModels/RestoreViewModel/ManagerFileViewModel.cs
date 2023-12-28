using ModelProject.ViewModels.ViewModelConnect;
using ModelProject.ViewModels.ViewModelIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels.RestoreViewModel
{
	public class ManagerFileViewModel
	{
		public ManagerFileViewModel()
		{
			FileInfomationViewModels = new List<FileInfomationViewModel>();
			DatabaseConnectViewModel = new DatabaseConnectViewModel();
			MessageBusViewModel = new MessageBusViewModel();
		}
		public List<FileInfomationViewModel> FileInfomationViewModels { get; set; }
		public DatabaseConnectViewModel DatabaseConnectViewModel { get; set; }
		public string PathLocation { get; set; }
		public MessageBusViewModel MessageBusViewModel { get; set; }
		public BackUpType BackUpType { get; set; }
		public bool IsRecovery { get; set; }
	}

	public class FileInfomationViewModel
	{
		public string Name { get; set; }
		public string FullName { get; set; }
		public string Size { get; set; }
		public string Extension { get; set; }
		public DateTime LastUpdateTime { get; set; }
	}
	public enum RestoreWith
	{
		NORECOVERY = 0,
		RECOVERY = 1,
		REPLACE = 2,
	}

}
