using Bus_backUpData.Models;
using Bus_backUpData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusConfigViewModel
    {
        public List<ConfigurationBackUpViewModel> GetConfigurationBackUpViewModel();
        public ConfigurationBackUpViewModel GetConfigurationBackUpViewModelByJobName(string JobName);
        public List<JobHistoryViewModel> GetJobHistoryViewModels(string jobname);
        public bool IsJob(string JobName);
    }
}
