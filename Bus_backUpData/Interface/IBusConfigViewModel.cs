using ModelProject.Models;
using ModelProject.ViewModels;
using ModelProject.ViewModels.RestoreViewModel;
using ModelProject.ViewModels.ViewModelConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusConfigViewModel
    {
        public List<ConfigurationBackUpViewModel> GetConfigurationBackUpViewModel(string DatabaseName);
        public List<ConfigurationBackUpViewModel> GetConfigurationBackUpViewModel();
        public ConfigurationBackUpViewModel GetConfigurationBackUpViewModelByJobName(string DatabaseName, string JobName);
        public ConfigurationBackUpViewModel GetConfigurationBackUpViewModelByJobId(Guid ConfigId);
        public List<JobHistoryViewModel> GetJobHistoryViewModels(ConfigurationBackUpViewModel  configurationBackUpViewModel);
        public DatabaseConnectViewModel GetDatabaseNameConnectViewModel(string DatabaseName);
        public ManagerFolderViewModel GetBackUpTypeFolderInformation(string DatabaseName);
        public ManagerFileViewModel GetBackUpTypeFileInformation(BackUpType backUpType, string DatabaseName);
        public ConfigRestoreViewModel GetConfigRestoreViewModel(ConfigRestoreViewModel configRestoreViewModel);


    }
}
