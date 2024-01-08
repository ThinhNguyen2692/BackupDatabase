using AutoMapper;
using Bus_backUpData.Interface;
using DalBackup.Interface;
using DalBackup.Services;
using Elfie.Serialization;
using HRM.SC.Core.Security;
using Microsoft.Extensions.Logging;
using ModelProject.Func;
using ModelProject.Models;
using ModelProject.ViewModels;
using ModelProject.ViewModels.ViewModelConnect;
using ModelProject.ViewModels.ViewModelSeverConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bus_backUpData.Services.AutoModelMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bus_backUpData.Services
{
    public class BusConfigServer : IBusConfigServer
    {
        private readonly IBusStoredProcedureServices _busStoredProcedureServices;
        private readonly IDalServerConnect _dalServerConnect;
        private readonly IDalDatabaseConnect _dalDatabaseConnect;
        private readonly IMapper _mapper;
        public BusConfigServer(IBusStoredProcedureServices busStoredProcedureServices, IDalServerConnect dalServerConnect, IMapper mapper,
            IDalDatabaseConnect dalDatabaseConnect)
        {
            _busStoredProcedureServices = busStoredProcedureServices;
            _dalServerConnect = dalServerConnect;
            _mapper = mapper;
            _dalDatabaseConnect = dalDatabaseConnect;
        }
        public async Task<ServerConnectionViewModel> SaveConnectionAsync(ServerConnectionViewModel serverConnectionViewModel)
        {
            WriteLogFile.WriteLog(string.Format("{0}{1}", "SaveConnectionAsync", DateTime.Now.ToString("ddMMyyyy")),
                        "SaveConnectionAsync_Start--------------" + serverConnectionViewModel.ServerName + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderBackUp);
            var serverConnectionViewModelJson = System.Text.Json.JsonSerializer.Serialize(serverConnectionViewModel);
            WriteLogFile.WriteLog(string.Format("{0}{1}", "SaveConnectionAsync", DateTime.Now.ToString("ddMMyyyy")),
            serverConnectionViewModelJson, Setting.FoderBackUp);
            try
            {
                var result = await _busStoredProcedureServices.CheckConnectionAsync(serverConnectionViewModel);
                if (result == false)
                {
                    serverConnectionViewModel.Id = Guid.Empty;
                    return serverConnectionViewModel;
                }
                var dataCheck = _dalServerConnect.FirstOrDefault(serverConnectionViewModel.ServerName);             
                if (dataCheck == null)
                {
                    var NewDatabaseConnect = new DatabaseConnect();
                    var mapper = MapperConfig<ServerConnectionViewModel, ServerConnect>.InitializeAutomapper();
                    var data = mapper.Map<ServerConnectionViewModel, ServerConnect>(serverConnectionViewModel);
                    NewDatabaseConnect.DatabaseName = serverConnectionViewModel.DatabaseName;
                    var ServerConnect = _dalServerConnect.Add(data);
                    NewDatabaseConnect.ServerConnectId = ServerConnect.Id;
                    NewDatabaseConnect = _dalDatabaseConnect.Add(NewDatabaseConnect);
                    serverConnectionViewModel.Id = ServerConnect.Id;
                    serverConnectionViewModel.DatabaseId = NewDatabaseConnect.Id;
                }
                else
                {
                    dataCheck.ServerName = serverConnectionViewModel.ServerName;
                    dataCheck.UserName = serverConnectionViewModel.UserName;
                    dataCheck.PassWord = serverConnectionViewModel.Password;
                    var repo = _dalServerConnect.Update(dataCheck);
                    var DatabaseConnect = _dalDatabaseConnect.FirstOrDefault(dataCheck.ServerName, serverConnectionViewModel.DatabaseName);
                    if (DatabaseConnect == null) {
                        DatabaseConnect = new DatabaseConnect();
                    }
                    DatabaseConnect.DatabaseName = serverConnectionViewModel.DatabaseName;
                    DatabaseConnect.ServerConnectId = dataCheck.Id;
                    DatabaseConnect = _dalDatabaseConnect.AddOrUpdate(DatabaseConnect);

                    serverConnectionViewModel.Id = dataCheck.Id;
                    serverConnectionViewModel.DatabaseId = DatabaseConnect.Id;
                }
                if (serverConnectionViewModel.Id != Guid.Empty)
                {
                    var MessageBusViewModel = _busStoredProcedureServices.CreateSettingDatabase(serverConnectionViewModel);
                    serverConnectionViewModel.MessageBusViewModel = MessageBusViewModel;

                }
            }
            catch (Exception ex)
            {

                WriteLogFile.WriteLog(string.Format("{0}{1}", "SaveConnectionAsync", DateTime.Now.ToString("ddMMyyyy")),
                        "SaveConnectionAsync_Error: " + ex.Message, Setting.FoderBackUp);
            }
            WriteLogFile.WriteLog(string.Format("{0}{1}", "SaveConnectionAsync", DateTime.Now.ToString("ddMMyyyy")),
                       "SaveConnectionAsync_End--------------" + serverConnectionViewModel.ServerName + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderBackUp);
            return serverConnectionViewModel;
        }

        public List<ServerConnectViewModel> GetServerConnectViewModel(string ServerName = null)
        {
            var data = _dalServerConnect.GetServerConnects();
            var viewModel = _mapper.Map<List<ServerConnectViewModel>>(data);
            if (!string.IsNullOrEmpty(ServerName))
            {
                viewModel = viewModel.Where(x => x.ServerName == ServerName).ToList();
            }
            return viewModel;
        }

    }
}
