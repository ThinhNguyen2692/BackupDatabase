using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusConfigurationBackUp
    {
        public List<ConfigurationBackUp> LoadJsonBackUp();
        public bool DeleteJsonBackUp(string jobname);
        public void AddJsonBackUp(ConfigurationBackUp configurationBackUp);
    }
}
