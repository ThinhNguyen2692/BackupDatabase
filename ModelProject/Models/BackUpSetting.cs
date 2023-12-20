using ModelProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models
{
    public class BackUpSetting : Entity
    {
        public string Name { get; set; }
        public BackUpType BackUpType { get; set; }
        public string Path { get; set; }
        public Guid ConfigurationBackUpId { get; set; }
       
    }

}
