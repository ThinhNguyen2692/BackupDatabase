using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels
{
    public enum BackUpType
    {
        Full = 0,
        Differential = 1,
        Log = 2,
    }
    public class BackUpSettingViewModel
    {
        [DisplayName("Database")]
        public string Name { get; set; }
        [DisplayName("Backup Type")]
        public BackUpType BackUpType { get; set; }
        [DisplayName("Path")]
        public string Path { get; set; }
    }
}
