using ModelProject.Models;
using ModelProject.ViewModels;
using ModelProject.ViewModels.ModelRequest;
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
        public List<ConfigurationBackUpViewModel> GetConfigurationBackUpViewModel(string ServerName,string DatabaseName);
        public List<ConfigurationBackUpViewModel> GetConfigurationBackUpViewModel(Guid id);
        public List<ConfigurationBackUpViewModel> GetConfigurationBackUpViewModel();
        public List<ConfigurationBackUpViewModel> MapConfigurationBackUpViewModel(List<ConfigurationBackUp> configurationBackUps);
        public ConfigurationBackUpViewModel GetConfigurationBackUpViewModelView(string ServerName, string DatabaseName, string JobName);
        public ConfigurationBackUpViewModel GetConfigurationBackUpViewModelView(Guid Id);
        public ConfigurationBackUpViewModel GetConfigurationBackUpViewModelView(JobViewModel jobModel);
        public ConfigurationBackUpViewModel GetConfigurationBackUpViewModelByJobId(Guid ConfigId);
        public List<JobHistoryViewModel> GetJobHistoryViewModels(ConfigurationBackUpViewModel  configurationBackUpViewModel);
        public DatabaseConnectViewModel GetDatabaseConnectViewModel(Guid Id);
        public DatabaseConnectViewModel GetDatabaseConnectViewModel(string ServerName, string DatabaseName);
        public DatabaseConnectViewModel GetDatabaseConnectViewModel(DatabaseConnect databaseConnect);
        public ManagerFolderViewModel GetBackUpTypeFolderInformationByDatabase(string serverName, string DatabaseName);
        public ManagerFileViewModel GetBackUpTypeFileInformation(BackUpType backUpType, string serverName, string DatabaseName);
        public ConfigRestoreViewModel GetConfigRestoreViewModel(ConfigRestoreViewModel configRestoreViewModel);


    }
}
