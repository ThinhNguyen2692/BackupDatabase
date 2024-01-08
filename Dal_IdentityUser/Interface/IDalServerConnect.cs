using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalBackup.Interface
{
    public interface IDalServerConnect
    {
        public ServerConnect Add(ServerConnect model);
        public ServerConnect FirstOrDefault(Guid id);
        public ServerConnect FirstOrDefault(string Servername);
        public bool Update(ServerConnect model);
        public List<ServerConnect> GetServerConnects();
    }
}
