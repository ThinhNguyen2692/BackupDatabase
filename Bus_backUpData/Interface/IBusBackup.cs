using ModelProject.Models;
using ModelProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusBackup
    {
        public bool SaveSetting(ConfigurationBackUpViewModel configurationBackUpViewModel);
        public MessageBusViewModel CreateJob(ConfigurationBackUpViewModel configurationBackUpViewModel);
        public MessageBusViewModel DeleteJob(string JobName);
        public MessageBusViewModel StartJobNow(string jobname);
    }
}
