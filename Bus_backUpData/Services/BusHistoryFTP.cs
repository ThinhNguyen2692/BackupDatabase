using ModelProject.Func;
using Bus_backUpData.Interface;
using ModelProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bus_backUpData.Services
{
    public class BusHistoryFTP : IBusHistoryFTP
    {
        public List<HistoryFTP> LoadJsonFileFTP()
        {
            try
            {
                string pathLocation = Path.GetFullPath("Config\\" + Setting.TypeConfigFileFTP);
                using (StreamReader r = new StreamReader(pathLocation))
                {
                    string json = r.ReadToEnd();
                    if(string.IsNullOrEmpty(json)) return new List<HistoryFTP>();
                    List<HistoryFTP> items = JsonConvert.DeserializeObject<List<HistoryFTP>>(json);
                    if (items == null) items = new List<HistoryFTP>();
                    return items;
                }
            }
            catch (Exception)
            {
                return new List<HistoryFTP>();
            }

        }

        public bool DeleteJsonFTP(string jobname)
        {
            var data = LoadJsonFileFTP();
            var ConfigurationDelete = data.FirstOrDefault(x => x.JobName == jobname);
            if (ConfigurationDelete != null)
            {
                data.Remove(ConfigurationDelete);
                LibrarySettingFileConfig.SaveConfig(data, Setting.TypeConfigFileFTP);
                return true;
            }
            return false;
        }

        public void DeleteJsonFTPRange(List<HistoryFTP> historyFTPs)
        {
            var data = LoadJsonFileFTP();
            foreach (var item in historyFTPs)
            {
                DeleteJsonFTP(item.JobName);
            }
            LibrarySettingFileConfig.SaveConfig(data, Setting.TypeConfigFileFTP);
        }

        public void AddJsonFTP(HistoryFTP HistoryFTP)
        {
            var data = LoadJsonFileFTP();
            data.Add(HistoryFTP);
            LibrarySettingFileConfig.SaveConfig(data, Setting.TypeConfigFileFTP);
        }

    }
}
