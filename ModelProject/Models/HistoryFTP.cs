using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models
{
    public class HistoryFTP : Entity
    {
        public HistoryFTP() { }
        public Guid JobId { get; set; }
        public string JobName { get; set; }
        public string FullFilePathName { get; set; }
    }
}
