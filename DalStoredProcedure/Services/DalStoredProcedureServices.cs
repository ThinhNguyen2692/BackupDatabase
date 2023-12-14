using Bus_backUpData.Data;
using DalStoredProcedure.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DalStoredProcedure.Services
{
    public class DalStoredProcedureServices: IDalStoredProcedureServices
    {
        public DalStoredProcedureServices() { }

        public void ExecuteSqlRaw(string Connection, string Sql, List<SqlParameter> SqlParameters)
        {
            using (Context context = new Context(Connection))
            {
                context.Database.ExecuteSqlRaw(Sql, SqlParameters);
            }
        }
        public List<string> SqlQueryRaw(string Connection, string Sql)
        {
            using (Context context = new Context(Connection))
            {
                var result = context.Database.SqlQueryRaw<string>(Sql);
                return result.ToList();
            }
        }

        public bool CheckConnection(string Connection)
        {
            using (Context context = new Context(Connection))
            {
                var result = context.Database.CanConnect();
                return result;
            }
        }
    }
}
