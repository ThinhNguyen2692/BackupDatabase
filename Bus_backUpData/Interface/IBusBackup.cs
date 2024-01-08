using ModelProject.Models;
using ModelProject.ViewModels;
using ModelProject.ViewModels.ModelRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusBackup
    {
        public ConfigurationBackUp SaveSetting(ConfigurationBackUp ConfigurationBackUp);
        public ConfigurationBackUpViewModel CreateJob(ConfigurationBackUpViewModel configurationBackUpViewModel);
        public MessageBusViewModel DeleteJob(JobViewModel jobModel);
        public MessageBusViewModel StartJobNow(JobViewModel jobModel);
        public Task<MessageBusViewModel> RestoreBackUpNowAsync(string ServerName, string DatabaseName, string Path, string FileName);
        public Task<MessageBusViewModel> ExecuteRecoveryDatabaseAsync(string ServerName, string DatabaseName);
        public MessageBusViewModel RemoveDatabase(Guid Id);
        public MessageBusViewModel RemoveServer(Guid Id);

    }
}
