using AutoMapper;
using Bus_backUpData.Interface;
using DalBackup.Interface;
using DalBackup.Services;
using Elfie.Serialization;
using HRM.SC.Core.Security;
using Microsoft.Extensions.Logging;
using ModelProject.Models;
using ModelProject.ViewModels;
using ModelProject.ViewModels.ViewModelConnect;
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
    public class BusConfigServer : IBusConfigServer
    {
        private readonly IBusStoredProcedureServices _busStoredProcedureServices;
        private readonly IDalServerConnect _dalServerConnect;
        private readonly IMapper _mapper;
        public BusConfigServer(IBusStoredProcedureServices busStoredProcedureServices, IDalServerConnect dalServerConnect, IMapper mapper)
        {
            _busStoredProcedureServices = busStoredProcedureServices;
            _dalServerConnect = dalServerConnect;
            _mapper = mapper;

        }
        public async Task<ServerConnectionViewModel> SaveConnectionAsync(ServerConnectionViewModel serverConnectionViewModel)
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
                dataCheck.UserName = serverConnectionViewModel.UserName;
                dataCheck.PassWord = serverConnectionViewModel.Password;
                var DatabaseConnect = new DatabaseConnect();
                DatabaseConnect.DatabaseName = serverConnectionViewModel.DatabaseName;
                DatabaseConnect.ServerConnectId = dataCheck.Id;
                dataCheck.DatabaseConnects.Add(DatabaseConnect);
                var repo = _dalServerConnect.Update(dataCheck);
                serverConnectionViewModel.Id = repo.Id;

            }
            if (serverConnectionViewModel.Id != Guid.Empty)
            {
               var MessageBusViewModel = _busStoredProcedureServices.CreateSettingDatabase(serverConnectionViewModel);
                serverConnectionViewModel.MessageBusViewModel = MessageBusViewModel;

            }

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
