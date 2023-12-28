using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalStoredProcedure.Interface
{
    public interface IDalStoredProcedureServices
    {
        public int ExecuteSqlRaw(string Connection, string Sql, List<SqlParameter> SqlParameters);
        public int ExecuteSqlRaw(string Connection, string Sql);

		public List<string> SqlQueryRaw(string Connection, string Sql);
        public List<string> SqlQueryRaw(string Connection, string Sql, List<SqlParameter> SqlParameters);

		public bool CheckConnection(string Connection);

    }
}
