using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels.ViewModelConnect
{
    public class ServerConnectViewModel
    {
        public string ServerName { get; set; }
        public List<DatabaseConnectViewModel> DatabaseConnectViewModels { get; set;}
    }
}
