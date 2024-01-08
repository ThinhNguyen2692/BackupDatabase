using ModelProject.Models;
using ModelProject.ViewModels.ModelRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusConfigurationInformation
    {
		public bool DeleteBackUp(string ServerName, string DatabaseName, string JobName);
		public bool DeleteBackUpByID(Guid id);
        public bool DeleteBackUpBy(ConfigurationBackUp? configurationBackUp);
        public string GetConnectStringByDatabase(string ServerName, string DatabaseName);
        public string GetConnectStringByDatabase(Guid Id);
        public string GetConnectString(DatabaseConnect? databaseConnect);
        public string GetConnectStringByJob(string ServerName, string DatbaseName, string JobName);
        public string GetConnectStringByJob(Guid Id);
        public string GetConnectStringByJob(ConfigurationBackUp? configurationBackUp);
        public string GetServerNameByDatabase(string ServerName, string DatabaseName);
        public string GetServerNameByDatabase(Guid Id);
        public string GetServerNameByDatabase(DatabaseConnect databaseConnect);
        public string GetServerNameByJob(string ServerName, string DatabaseName, string JobName);
        public string GetServerNameByJob(Guid Id);
        public string GetServerName(string Connectstring);
        public bool IsJob(string ServerName, string DatabaseName, string JobName);
        public bool IsJob(Guid Id);
        public bool IsRecovery(string ServerName, string DatabaseName);
        public string? StateDescDB(string ServerName, string DatabaseName);
        public JobViewModel GetFullJobModel(JobViewModel jobModel);
        public string GetBackUpSettingPath(string ServerName, string DatabaseName);
    }
}
