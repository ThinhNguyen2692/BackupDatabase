using ModelProject.Func;
using Bus_backUpData.Interface;
using ModelProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Services
{
    public  class BusConfigurationBackUp : IBusConfigurationBackUp
    {

        public BusConfigurationBackUp() { }
        public List<ConfigurationBackUp> LoadJsonBackUp()
        {
            try
            {
                string pathLocation = Path.GetFullPath("Config\\" + Setting.TypeConfigbackup);
                using (StreamReader r = new StreamReader(pathLocation))
                {
                    string json = r.ReadToEnd();
                    List<ConfigurationBackUp> items = JsonConvert.DeserializeObject<List<ConfigurationBackUp>>(json);
                    if (items == null) items = new List<ConfigurationBackUp>();
                    return items;
                }
            }
            catch (Exception)
            {
                return new List<ConfigurationBackUp>();
            }

        }

        public bool DeleteJsonBackUp(string jobname)
        {
            var data = LoadJsonBackUp();
            var ConfigurationBackUpDelete = data.FirstOrDefault(x => x.BackupName == jobname);
            if (ConfigurationBackUpDelete != null)
            {
                data.Remove(ConfigurationBackUpDelete);
                LibrarySettingFileConfig.SaveConfig(data, Setting.TypeConfigbackup);
                return true;
            }
            return false;
        }
        public  void AddJsonBackUp(ConfigurationBackUp configurationBackUp)
        {
            var data = LoadJsonBackUp();
            if (configurationBackUp.Id == Guid.Empty) configurationBackUp.Id = Guid.NewGuid();
            data.Add(configurationBackUp);
            LibrarySettingFileConfig.SaveConfig(data, Setting.TypeConfigbackup);
        }
    }
}
