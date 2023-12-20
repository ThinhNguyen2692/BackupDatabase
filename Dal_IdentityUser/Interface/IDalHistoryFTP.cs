using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalBackup.Interface
{
	public interface IDalHistoryFTP
	{

		public HistoryFTP Add(HistoryFTP model);
		public HistoryFTP Update(HistoryFTP model);
		public HistoryFTP? FirstOrDefault(Guid id);
		public List<HistoryFTP> GetHistoryFTPs();
		public HistoryFTP? FirstOrDefault(string jobName);
	}
}
