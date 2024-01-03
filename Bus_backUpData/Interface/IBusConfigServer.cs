using ModelProject.ViewModels.ViewModelConnect;
using ModelProject.ViewModels.ViewModelSeverConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusConfigServer 
    {
        public Task<ServerConnectionViewModel> SaveConnectionAsync(ServerConnectionViewModel serverConnectionViewModel);
        public List<ServerConnectViewModel> GetServerConnectViewModel(string ServerName = null);
    }
}
