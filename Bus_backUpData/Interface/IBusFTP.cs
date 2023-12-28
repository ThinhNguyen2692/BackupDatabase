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
        public bool DeleteFTP(Guid JobId);
        public bool SaveFileFTP(Guid JobId, string JobName, string FileName);
        public List<HistoryFTP> GetHistoryFTPS(string JobName);
        public void JobTaskPushFTp(Guid JobId);
        public bool CheckFtpConnection(string ftpServer, string ftpUsername, string ftpPassword);
    }
}
