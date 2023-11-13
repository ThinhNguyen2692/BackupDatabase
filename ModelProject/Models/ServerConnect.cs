using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models
{
    public class ServerConnect : Entity
    {
        public string SeverName { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public virtual ICollection<DatabaseConnect> DatabaseConnects { get; set; }
    }
}
