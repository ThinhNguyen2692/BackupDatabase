using ModelProject.ViewModels.ViewModelSeverConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusConfig 
    {
        public ServerConnectionViewModel SaveConnection(ServerConnectionViewModel serverConnectionViewModel);
    }
}
