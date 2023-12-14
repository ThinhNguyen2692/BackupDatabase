using Bus_backUpData.Interface;
using DalBackup.Interface;
using DalBackup.Services;
using HRM.SC.Core.Security;
using Microsoft.Extensions.Logging;
using ModelProject.Models;
using ModelProject.ViewModels;
using ModelProject.ViewModels.ViewModelSeverConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bus_backUpData.Services.AutoModelMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bus_backUpData.Services
{
    public class BusConfig : IBusConfig
    {
        private readonly IBusStoredProcedureServices _busStoredProcedureServices;
        private readonly IDalServerConnect _dalServerConnect;
        public BusConfig(IBusStoredProcedureServices busStoredProcedureServices, IDalServerConnect dalServerConnect)
        {
            _busStoredProcedureServices = busStoredProcedureServices;
            _dalServerConnect = dalServerConnect;
        }
        public ServerConnectionViewModel SaveConnection(ServerConnectionViewModel serverConnectionViewModel)
        {
            var result = _busStoredProcedureServices.CheckConnection(serverConnectionViewModel);
            if (result == false)
            {
                serverConnectionViewModel.Id = Guid.Empty;
                return serverConnectionViewModel;
            }
            var dataCheck = _dalServerConnect.FirstOrDefault(serverConnectionViewModel.ServerName);
            if (dataCheck == null)
            {
                var DatabaseConnect = new DatabaseConnect();
                var mapper = MapperConfig<ServerConnectionViewModel, ServerConnect>.InitializeAutomapper();
                var data = mapper.Map<ServerConnectionViewModel, ServerConnect>(serverConnectionViewModel);
                DatabaseConnect.DatabaseName = serverConnectionViewModel.DatabaseName;
                List<DatabaseConnect> databaseConnects = new List<DatabaseConnect>();
                databaseConnects.Add(DatabaseConnect);
                data.DatabaseConnects = databaseConnects;
               var rest = _dalServerConnect.Add(data);
                serverConnectionViewModel.Id = rest.Id;
            }
            else
            {
                dataCheck.ServerName = serverConnectionViewModel.ServerName;
                dataCheck.UserName = serverConnectionViewModel.ServerName;
                dataCheck.PassWord = serverConnectionViewModel.Password;
                var DatabaseConnect = new DatabaseConnect();
                DatabaseConnect.DatabaseName = serverConnectionViewModel.DatabaseName;
                dataCheck.DatabaseConnects.Add(DatabaseConnect);
                var repo = _dalServerConnect.Update(dataCheck);
                serverConnectionViewModel.Id = repo.Id;

            }
            
            return serverConnectionViewModel;
        }
    }
}
