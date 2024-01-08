using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels.ModelRequest
{
    public class JobViewModel
    {
        public Guid Id { get; set; }
        public string? DatabaseName { get; set; }
        public string? ServerName { get; set; }
        public string? JobName { get; set; }
    }
}
