using ModelProject.Models;
using ModelProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusFTP
    {
        public bool PushFPT(ConfigurationBackUp ConfigurationBackUp, ConfigurationBackUpViewModel configurationBackUpViewModel);
        public bool DeleteFTP(string JobName);
        public bool SaveFileFTP(string JobName, string FileName);
        public List<HistoryFTP> GetHistoryFTPS(string JobName);
        public void JobTaskPushFTp(string JobName);
    }
}
