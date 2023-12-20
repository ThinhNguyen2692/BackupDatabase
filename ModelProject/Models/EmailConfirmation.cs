using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models
{
    public class EmailConfirmation : Entity
    {
        public string EmailSuccess { get; set; }
        public string EmailFailure { get; set; }
        public Guid ConfigurationBackUpId { get; set; }
    }
}
