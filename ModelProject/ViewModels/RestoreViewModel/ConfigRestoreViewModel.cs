using ModelProject.ViewModels.ViewModelConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels.RestoreViewModel
{
    public class ConfigRestoreViewModel
    {
        public BackUpType BackUpTypeName { get; set; }
        public string DatabaseName { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public DatabaseConnectViewModel DatabaseConnectViewModel { get; set; } = new DatabaseConnectViewModel();
    }
    
}
