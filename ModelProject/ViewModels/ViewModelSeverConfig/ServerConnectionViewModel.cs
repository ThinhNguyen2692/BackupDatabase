using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels.ViewModelSeverConfig
{
    public class ServerConnectionViewModel
    {
        public ServerConnectionViewModel() { }  

        public Guid Id { get; set; }
        public string ServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
    }
}
