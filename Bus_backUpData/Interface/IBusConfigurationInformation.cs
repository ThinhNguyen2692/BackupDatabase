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
		public bool DeleteJsonBackUp(string jobname, string dataBaseName);
		public bool DeleteJsonBackUpByID(Guid id);
        public bool DeleteBackUpBy(ConfigurationBackUp? configurationBackUp);

        public string GetConnectStringByDatabaseName(string DatabaseName);
        public string GetConnectString(DatabaseConnect? databaseConnect);
        public string GetConnectStringByJobName(string JobName, string DatbaseName);
        public string GetConnectStringByJobId(Guid Id);
        public string GetServerNameByDatabaseName(string DatabaseName);
        public string GetServerNameByJobName(string JobName, string DatabaseName);
        public string GetServerNameByJobId(Guid Id);
        public string GetServerName(string Connectstring);
        public bool IsJob(string JobName, string DatbaseName);
        public bool IsJob(Guid Id);
        public bool IsRecovery(string DatabaseName);
        public string? StateDescDB(string DatabaseName);

        public JobModel GetFullJobModel(JobModel jobModel);
    }
}
