using Bus_backUpData.Interface;
using DalStoredProcedure.Interface;
using ModelProject.Func;
using ModelProject.Models;
using ModelProject.ViewModels.ViewModelSeverConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Services
{
    public class BusStoredProcedureServices : IBusStoredProcedureServices
    {
        public readonly IDalStoredProcedureServices _dalStoredProcedureServices;
        public BusStoredProcedureServices(IDalStoredProcedureServices dalStoredProcedureServices) {
            _dalStoredProcedureServices = dalStoredProcedureServices;
        }

        public bool CheckConnection(ServerConnectionViewModel serverConnectionViewModel)
        {
            var connectionstring = SettingConnection.GetConnection(serverConnectionViewModel.ServerName, serverConnectionViewModel.DatabaseName, serverConnectionViewModel.UserName, serverConnectionViewModel.Password);
            var result = _dalStoredProcedureServices.CheckConnection(connectionstring);
            return result;
        }

        public List<string> GetListDatabaseNameServer(ServerConnectionViewModel serverConnectionViewModel)
        {
            var connectionstring = SettingConnection.GetConnection(serverConnectionViewModel.ServerName, serverConnectionViewModel.DatabaseName, serverConnectionViewModel.UserName, serverConnectionViewModel.Password);
            var result = _dalStoredProcedureServices.SqlQueryRaw(connectionstring, StringSql.SQlsysdatabases).ToList();
            return result.ToList();
        }
       
    }
}
        