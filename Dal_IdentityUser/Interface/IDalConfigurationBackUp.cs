using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalBackup.Interface
{
    public interface IDalConfigurationBackUp
    {
        public ConfigurationBackUp Add(ConfigurationBackUp model);
        public ConfigurationBackUp FirstOrDefault(string JobName, string DatabaseName, string ServerName);
        public ConfigurationBackUp? FirstOrDefault(Guid Id);
        public List<ConfigurationBackUp> GetData();
        public List<ConfigurationBackUp> GetData(string SeverName,string DatabaseName);
        public List<ConfigurationBackUp> GetData(Guid id);
        public ConfigurationBackUp Update(ConfigurationBackUp model);
        public ConfigurationBackUp Delete(ConfigurationBackUp model);
        public ConfigurationBackUp AddOrUpdate(ConfigurationBackUp model);

	}
}
