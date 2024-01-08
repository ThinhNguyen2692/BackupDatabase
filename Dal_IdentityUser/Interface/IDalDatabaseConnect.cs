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
        public bool Update(DatabaseConnect model);
        public DatabaseConnect? FirstOrDefault(Guid id);
        public DatabaseConnect? FirstOrDefault(string ServerName, string DatabaseName);
        public List<Guid> GetIdByServerId(Guid ServerId);
        public DatabaseConnect AddOrUpdate(DatabaseConnect model);
    }
}
