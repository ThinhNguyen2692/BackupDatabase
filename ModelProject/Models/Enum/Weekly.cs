using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models.Enum
{
    public class Weekly : Entity
    {
        //Monday = 2,
        //Tuesday = 4,
        //Wednesday = 8,
        //Thursday = 16,
        //Friday = 32,
        //Saturday = 64,
        //Sunday = 1,

        public string Name { get; set; }
        public int Value { get; set; }
    }
}
