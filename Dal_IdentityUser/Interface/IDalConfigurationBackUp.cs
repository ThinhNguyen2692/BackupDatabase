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
        public ConfigurationBackUp FirstOrDefaultByJobName(string JobName);
        public List<ConfigurationBackUp> GetData();
        public ConfigurationBackUp Update(ConfigurationBackUp model);
        public ConfigurationBackUp Delete(ConfigurationBackUp model);
        public ConfigurationBackUp AddOrUpdate(ConfigurationBackUp model);

	}
}
