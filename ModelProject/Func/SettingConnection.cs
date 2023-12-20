using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Func
{
    public static class SettingConnection
    {

        public static string GetConnection(string servername, string databaseName, string login, string password)
        {
            var connection = Setting.ConnectionDefaut;
            if (string.IsNullOrEmpty(databaseName))
            {
                databaseName = Setting.UsingMaster;
            }
            connection = string.Format(connection, servername, databaseName, login, password);
            return connection;
        }
		public static string GetConnection(DatabaseConnect databaseConnect)
		{
			var connection = Setting.ConnectionDefaut;
			connection = string.Format(connection, databaseConnect.ServerConnects.ServerName, databaseConnect.DatabaseName, databaseConnect.ServerConnects.UserName, databaseConnect.ServerConnects.PassWord);
			return connection;
		}
	}
}
