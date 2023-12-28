using ModelProject.ViewModels.ViewModelConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels.RestoreViewModel
{
	public class ManagerFolderViewModel
	{
		public ManagerFolderViewModel() {
			BackUpTypeFolder = new List<BackUpTypeViewModel>();
			DatabaseConnectViewModel = new DatabaseConnectViewModel();
			MessageBusViewModel = new MessageBusViewModel();
        }
		public List<BackUpTypeViewModel> BackUpTypeFolder { get; set; }
		public DatabaseConnectViewModel DatabaseConnectViewModel { get; set; }
		public MessageBusViewModel MessageBusViewModel { get; set; }
		public bool IsRecovery { get; set; }
		public MessageStatus MessageStatus { get; set; }

	}

	public class BackUpTypeViewModel
	{
		public string Name { get; set; }
		public DateTime? LastUpdateTime {  get; set; }
		public string FolderSize { get; set; }
	}
}
