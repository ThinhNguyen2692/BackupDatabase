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
        public bool SaveSetting(ConfigurationBackUp ConfigurationBackUp);
        public MessageBusViewModel CreateJob(ConfigurationBackUpViewModel configurationBackUpViewModel);
        public MessageBusViewModel DeleteJob(JobModel jobModel);
        public MessageBusViewModel StartJobNow(JobModel jobModel);
        public MessageBusViewModel RestoreBackUpNow(string DatabaseName, string Path, string FileName);
        public MessageBusViewModel ExecuteRecoveryDatabase(string DatabaseName);

	}
}
