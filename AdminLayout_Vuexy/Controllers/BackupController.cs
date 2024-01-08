using Bus_backUpData.Interface;
using Microsoft.AspNetCore.Authorization;
using ModelProject.ViewModels.ViewModelSeverConfig;
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelProject.ViewModels.ModelRequest;
using Bus_backUpData.Services;
using System.Data.Entity.Infrastructure;

namespace AdminLayout_Vuexy.Controllers
{
    [ApiController]
    public class BackupController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusStoredProcedureServices _busStoredProcedureServices;
        private readonly IBusConfigServer _busConfigServer;
        private readonly IBusBackup _BusBackup;
        public BackupController(ILogger<HomeController> logger, IBusStoredProcedureServices busStoredProcedureServices, IBusConfigServer busConfigServer, IBusBackup busBackup)
        {
            _logger = logger;
            _busStoredProcedureServices = busStoredProcedureServices;
            _busConfigServer = busConfigServer;
            _BusBackup = busBackup;
        }

        [Route("/api/CheckConntion")]
        [HttpPost]
        public async Task<IActionResult> CheckConntion([FromBody]ServerConnectionViewModel serverConnectionViewModel)
        {
            var result = await _busStoredProcedureServices.CheckConnectionAsync(serverConnectionViewModel);
            return Ok(result);
        }
        [Route("/api/GetListDatabaseNameServer")]
        [HttpPost]
        public IActionResult GetListDatabaseNameServer([FromBody]ServerConnectionViewModel serverConnectionViewModel)
        {
            var result = _busStoredProcedureServices.GetListDatabaseNameServer(serverConnectionViewModel);
            return Ok(result);
        }

        [Route("/api/SaveConnection")]
        [HttpPost]
        public async Task<IActionResult> SaveConnection([FromBody] ServerConnectionViewModel serverConnectionViewModel)
        {
            var result = await _busConfigServer.SaveConnectionAsync(serverConnectionViewModel);
            return Ok(result);
        }

        [Route("/api/DeleteJob")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteJob([FromBody] JobViewModel jobModel)
        {
            var MessageBusViewModel = _BusBackup.DeleteJob(jobModel);
            return Ok(MessageBusViewModel);
        }
    }
}
