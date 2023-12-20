using Bus_backUpData.Interface;
using Microsoft.AspNetCore.Authorization;
using ModelProject.ViewModels.ViewModelSeverConfig;
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminLayout_Vuexy.Controllers
{
    [ApiController]
    public class BackupController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusStoredProcedureServices _busStoredProcedureServices;
        private readonly IBusConfigServer _busConfigServer;
        public BackupController(ILogger<HomeController> logger, IBusStoredProcedureServices busStoredProcedureServices, IBusConfigServer busConfigServer)
        {
            _logger = logger;
            _busStoredProcedureServices = busStoredProcedureServices;
            _busConfigServer = busConfigServer;
        }

        [Route("/api/CheckConntion")]
        [HttpPost]
        public IActionResult CheckConntion([FromBody]ServerConnectionViewModel serverConnectionViewModel)
        {
            var result = _busStoredProcedureServices.CheckConnection(serverConnectionViewModel);
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
        public IActionResult SaveConnection([FromBody] ServerConnectionViewModel serverConnectionViewModel)
        {
            var result = _busConfigServer.SaveConnection(serverConnectionViewModel);
            return Ok(result);
        }
    }
}
