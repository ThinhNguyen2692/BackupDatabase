using ModelProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models
{
    public class BackUpSetting
    {
        public string Name { get; set; }
        public BackUpType BackUpType { get; set; }
        public string Path { get; set; }
    }

}
