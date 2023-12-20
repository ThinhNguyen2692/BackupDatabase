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
        public List<HistoryFTP> LoadFileFTP();
        public bool DeleteFTP(string jobname);
        public void DeleteFTPRange(List<HistoryFTP> historyFTPs);
        public HistoryFTP AddHistoryFTP(HistoryFTP HistoryFTP);
    }
}
