using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusConfigurationInformation
    {
        public bool DeleteJsonBackUp(string jobname);
		public string GetConnectStringByDatabaseName(string DatabaseName);
		public string GetConnectStringByJobName(string JobName);
		public string GetServerNameByDatabaseName(string DatabaseName);
		public string GetServerNameByJobName(string JobName);
		public string GetServerName(string DatabaseName);
		public bool IsJob(string JobName);
	}
}
