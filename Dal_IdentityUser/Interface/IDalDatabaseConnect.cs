using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalBackup.Interface
{
    public interface IDalDatabaseConnect
    {
        public DatabaseConnect Add(DatabaseConnect model);
        public DatabaseConnect FirstOrDefault(Guid id);
        public DatabaseConnect FirstOrDefault(string DatabaseName);
    }
}
