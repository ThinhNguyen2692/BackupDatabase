using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusHistoryFTP
    {
        public List<HistoryFTP> LoadJsonFileFTP();
        public bool DeleteJsonFTP(string jobname);
        public void DeleteJsonFTPRange(List<HistoryFTP> historyFTPs);
        public void AddJsonFTP(HistoryFTP HistoryFTP);
    }
}
