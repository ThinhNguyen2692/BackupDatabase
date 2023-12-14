using ModelProject.ViewModels.ViewModelSeverConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusStoredProcedureServices
    {
        public bool CheckConnection(ServerConnectionViewModel serverConnectionViewModel);
        public List<string> GetListDatabaseNameServer(ServerConnectionViewModel serverConnectionViewModel);
    }
}
