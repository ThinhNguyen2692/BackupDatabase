using AdminLayout_Vuexy.Models;
using ModelProject.Func;
using Bus_backUpData.Interface;
using ModelProject.Models;
using ModelProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using ModelProject.ViewModels.ModelRequest;
using System.Data.Entity.Infrastructure;
using ModelProject.ViewModels.RestoreViewModel;
using Org.BouncyCastle.Tls;
using System.Security.Policy;

namespace AdminLayout_Vuexy.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IBusBackup _BusBackup;
		private readonly IBusConfigViewModel _busConfigViewModel;
		private readonly IBusConfigurationInformation _busConfigurationInformation;
		private static Dictionary<Guid, MessageBusViewModel> _messageBusViewModels = new Dictionary<Guid, MessageBusViewModel>();
		private readonly string _urlDefaut = "/";
		public HomeController(ILogger<HomeController> logger, IBusBackup busBackup, IBusConfigViewModel busConfigViewModel, IBusConfigurationInformation busConfigurationInformation)
		{
			_logger = logger;
			_BusBackup = busBackup;
			_busConfigViewModel = busConfigViewModel;
			_busConfigurationInformation = busConfigurationInformation;
		}

		[Route("/{ServerName}/{DatabaseName}/{JobName?}")]
		[HttpGet]
		public IActionResult Index(JobViewModel jobModel)
		{
			ConfigurationBackUpViewModel configurationBackUpViewModel = _busConfigViewModel.GetConfigurationBackUpViewModelView(jobModel);
            if (configurationBackUpViewModel.MessageBusViewModel.MessageStatus == MessageStatus.NotFound) {
                return View("PageNotFound");
            }
            configurationBackUpViewModel.MessageBusViewModel = GetMessageBusViewModel(configurationBackUpViewModel.Id) ?? configurationBackUpViewModel.MessageBusViewModel;
            return View("Index", configurationBackUpViewModel);
		}

		[HttpGet]
        [Route("/PageNotFound")]
        public IActionResult PageNotFound()
		{
			return View();
		}

       
        [Route("/ManagerFolder/{ServerName}/{DatabaseName}/{IdMess?}")]
		public IActionResult Privacy(JobViewModel jobModel, Guid? IdMess)
		{
			var viewModel = _busConfigViewModel.GetBackUpTypeFolderInformationByDatabase(jobModel.ServerName, jobModel.DatabaseName);
			viewModel.MessageBusViewModel = GetMessageBusViewModel(IdMess) ?? viewModel.MessageBusViewModel;
			return View(viewModel);
		}

		[Route("/ManagerFile/{BackUpTypeName}/{ServerName}/{DatabaseName}/{IdMess?}")]
		public IActionResult ManagerFile(BackUpType BackUpTypeName, JobViewModel jobModel, Guid? IdMess)
		{
			var viewModel = _busConfigViewModel.GetBackUpTypeFileInformation(BackUpTypeName, jobModel.ServerName, jobModel.DatabaseName);
            viewModel.MessageBusViewModel = GetMessageBusViewModel(IdMess) ?? viewModel.MessageBusViewModel;
            return View(viewModel);
		}
		//[Route("/ManagerFile")]
		//public IActionResult ManagerFile()
		//{

		//	return View();
		//}

		[HttpPost("Backup")]
        [ValidateAntiForgeryToken]
        public IActionResult Backup(ConfigurationBackUpViewModel BackUpViewModel)
		{
            BackUpViewModel = _BusBackup.CreateJob(BackUpViewModel);

			if (BackUpViewModel.MessageBusViewModel.MessageStatus == MessageStatus.Error)
			{
				BackUpViewModel.JobHistoryViewModels = new List<JobHistoryViewModel>();
				return View("Index", BackUpViewModel);
			}
			else
			{
                var idMess = BackUpViewModel.Id;
                _messageBusViewModels.Add(idMess, BackUpViewModel.MessageBusViewModel);
                var url = Url.Action("Index", "Home", 
					new { ServerName = BackUpViewModel.DatabaseConnectViewModel.ServerName, 
						DatabaseName = BackUpViewModel.DatabaseConnectViewModel.DatabaseName, 
						JobName = BackUpViewModel.BackupName }) ?? _urlDefaut;
                return Redirect(url);
			}
		}

		[Route("/DeleteJob/{ServerName}/{DatabaseName}/{JobName}/{Id}")]
		public IActionResult DeleteJob(JobViewModel jobModel)
		{
			var MessageBusViewModel = _BusBackup.DeleteJob(jobModel);
			if (MessageBusViewModel.MessageStatus == MessageStatus.Error)
			{
				var configurationBackUpViewModel = _busConfigViewModel.GetConfigurationBackUpViewModelView(jobModel.Id);
				configurationBackUpViewModel.MessageBusViewModel = MessageBusViewModel;
				return View("Index", configurationBackUpViewModel);
			}
           
            var url = Url.Action("Index", "Home", 
				new { ServerName = jobModel.ServerName, 
					DatabaseName = jobModel.DatabaseName, JobName = jobModel.JobName }) ?? _urlDefaut;
            return Redirect(url);
        }

		[Route("RunJobNow/{ServerName}/{DatabaseName}/{JobName}")]
		[HttpGet]
		public IActionResult RunJobNow(JobViewModel jobModel)
		{
			var MessageBusViewModel = _BusBackup.StartJobNow(jobModel);
            var url = Url.Action("Index", "Home", 
				new { ServerName = jobModel.ServerName, DatabaseName = jobModel.DatabaseName, 
					JobName = jobModel.JobName }) ?? _urlDefaut;
            return Redirect(url);
		}

		[Route("RestoreBackUpNow")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RestoreBackUpNow(BackUpType BackUpTypeName, string ServerName, string DatabaseName, string Path, string FileName)
		{
			var MessageBusViewModel = await _BusBackup.RestoreBackUpNowAsync(ServerName,DatabaseName, Path, FileName);
			var idMess = Guid.NewGuid();
			_messageBusViewModels.Add(idMess, MessageBusViewModel);
			var url = Url.Action("ManagerFile", "Home", new { BackUpTypeName = BackUpTypeName,
				DatabaseName = DatabaseName, ServerName= ServerName, IdMess = idMess }) ?? _urlDefaut;

			return Redirect(url);
		}

		[Route("ConfigRestore")]
		[HttpPost]
		public IActionResult ConfigRestore(ConfigRestoreViewModel configRestoreViewModel)
		{
			configRestoreViewModel = _busConfigViewModel.GetConfigRestoreViewModel(configRestoreViewModel);
			return View(configRestoreViewModel);
		}
		[Route("Recovery/{ServerName}/{DatabaseName}/{BackUpTypeName?}")]
		[HttpGet]
		public async Task<IActionResult> Recovery(BackUpType? BackUpTypeName, string DatabaseName, string ServerName)
		{
			var viewModel = await _BusBackup.ExecuteRecoveryDatabaseAsync(ServerName,DatabaseName);
			var idMess = Guid.NewGuid();
			_messageBusViewModels.Add(idMess, viewModel);
			var url = string.Empty;
			if (BackUpTypeName == null)
			{
				url = Url.Action("ManagerFolder", "Home", new
				{
					DatabaseName = DatabaseName,
					ServerName = ServerName,
					IdMess = idMess
				}) ?? _urlDefaut;

			}
			else
			{
				url = Url.Action("ManagerFile", "Home", new
				{
					BackUpTypeName = BackUpTypeName,
					DatabaseName = DatabaseName,
					ServerName = ServerName,
					IdMess = idMess
				}) ?? _urlDefaut;
			}
			return Redirect(url);
		}
        [Route("/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		private MessageBusViewModel? GetMessageBusViewModel(Guid? IdMess)
		{
            if (IdMess != null && IdMess != Guid.Empty)
            {

                var data = _messageBusViewModels.FirstOrDefault(x => x.Key == IdMess);
                if (!data.Equals(default(KeyValuePair<string, string>)))
                {
                    _messageBusViewModels.Remove(data.Key);
					return data.Value;
                }

            }
			return null;
        }
    }
}