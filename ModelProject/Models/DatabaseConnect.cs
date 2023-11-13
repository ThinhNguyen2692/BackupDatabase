using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models
{
    public class DatabaseConnect : Entity
    {
        public string DatabaseName { get; set; }
        public Guid ServerConnectId { get; set; }
        [ForeignKey("ServerConnectId")]
        public virtual ServerConnect ServerConnects { get; set; }
        public virtual ICollection<ConfigurationBackUp> ConfigurationBackUps { get; set; }
    }
}
