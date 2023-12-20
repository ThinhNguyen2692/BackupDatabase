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
using DalBackup.Interface;
using DalBackup.Services;

namespace Bus_backUpData.Services
{
    public class BusHistoryFTP : IBusHistoryFTP
    {
        private readonly IDalHistoryFTP _dalHistoryFTP;

        public BusHistoryFTP(IDalHistoryFTP dalHistoryFTP)
        {
            _dalHistoryFTP = dalHistoryFTP;
        }

		public List<HistoryFTP> LoadFileFTP()
        {
            var data = _dalHistoryFTP.GetHistoryFTPs();
            return data;

        }

        public bool DeleteFTP(string jobname)
        {
            var ConfigurationDelete = _dalHistoryFTP.FirstOrDefault(jobname);
            if (ConfigurationDelete != null)
            {
				ConfigurationDelete.IsDeleted = true;
                _dalHistoryFTP.Update(ConfigurationDelete);
                return true;
            }
            return false;
        }

        public void DeleteFTPRange(List<HistoryFTP> historyFTPs)
        {
            foreach (var item in historyFTPs)
            {
                DeleteFTP(item.JobName);
            }
        }

        public HistoryFTP AddHistoryFTP(HistoryFTP HistoryFTP)
        {
			HistoryFTP = _dalHistoryFTP.Add(HistoryFTP);
            return HistoryFTP;
        }

    }
}
