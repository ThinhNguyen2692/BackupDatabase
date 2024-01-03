using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels.ViewModelConnect
{
    public class DatabaseConnectViewModel
    {
        public Guid Id { get; set; }
        public string DatabaseName { get; set; }
        public string ServerName { get; set; }
        public string PassWord { get; set; }
        public string UserName { get; set; }
    }
}
