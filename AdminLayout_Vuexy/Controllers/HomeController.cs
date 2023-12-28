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

namespace AdminLayout_Vuexy.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IBusBackup _BusBackup;
		private readonly IBusConfigViewModel _busConfigViewModel;
		private static Dictionary<Guid, MessageBusViewModel> _messageBusViewModels = new Dictionary<Guid, MessageBusViewModel>();
		public HomeController(ILogger<HomeController> logger, IBusBackup busBackup, IBusConfigViewModel busConfigViewModel)
		{
			_logger = logger;
			_BusBackup = busBackup;
			_busConfigViewModel = busConfigViewModel;
		}

		[Route("/{DatabaseName}/{JobName?}")]
		[HttpGet]
		public IActionResult Index(JobModel jobModel)
		{
			ConfigurationBackUpViewModel configurationBackUpViewModel = new ConfigurationBackUpViewModel();
			if (!string.IsNullOrEmpty(jobModel.JobName))
			{
				configurationBackUpViewModel = _busConfigViewModel.GetConfigurationBackUpViewModelByJobName(jobModel.DatabaseName, jobModel.JobName);
				if (configurationBackUpViewModel.Id != Guid.Empty)
					configurationBackUpViewModel.JobHistoryViewModels = configurationBackUpViewModel.JobHistoryViewModels.Take(10).ToList();
			}
			if (configurationBackUpViewModel.Id == Guid.Empty)
			{
				var ListConfig = _busConfigViewModel.GetConfigurationBackUpViewModel(jobModel.DatabaseName).ToList();
				var DatabaseConnectViewModel = _busConfigViewModel.GetDatabaseNameConnectViewModel(jobModel.DatabaseName);
				var BackUpViewModel = new BackUpViewModel();
				BackUpViewModel.Name = string.IsNullOrEmpty(jobModel.JobName) ? "JobNew" : jobModel.JobName;
				BackUpViewModel.Id = Guid.Empty;
				BackUpViewModel.IsSelect = false;
				configurationBackUpViewModel.ScheduleBackup.RecursEveryWeekly = 1;
				configurationBackUpViewModel.ScheduleBackup.RecursEveryDay = 1;
				configurationBackUpViewModel.ScheduleBackup.DayEvery = 1;
				configurationBackUpViewModel.ScheduleBackup.DayMonth = 1;
				configurationBackUpViewModel.FTPSetting.Months = 1;
				configurationBackUpViewModel.BackUpSetting.Name = jobModel.DatabaseName;
				configurationBackUpViewModel.BackUpSetting.Path = Setting.PathbackUp;
				configurationBackUpViewModel.BackupName = BackUpViewModel.Name;
				configurationBackUpViewModel.IsEnabled = true;
				configurationBackUpViewModel.JobHistoryViewModels = new List<JobHistoryViewModel>();
				configurationBackUpViewModel.IsScheduleBackup = true;
				configurationBackUpViewModel.BackUpViewModels.Add(BackUpViewModel);
				configurationBackUpViewModel.BackUpViewModels.AddRange(ListConfig.Select(x => new BackUpViewModel() { Id = x.Id, Name = x.BackupName }).ToList());
				configurationBackUpViewModel.MessageBusViewModel.MessageStatus = MessageStatus.None;
				configurationBackUpViewModel.DatabaseConnectViewModel = DatabaseConnectViewModel;


			}
			return View("Index", configurationBackUpViewModel);

		}

		[Route("/ManagerFolder/{DatabaseName}/{IdMess?}")]
		public IActionResult Privacy(JobModel jobModel, Guid? IdMess)
		{
			var viewModel = _busConfigViewModel.GetBackUpTypeFolderInformation(jobModel.DatabaseName);
			viewModel.MessageBusViewModel = GetMessageBusViewModel(IdMess);
			return View(viewModel);
		}

		[Route("/ManagerFile/{BackUpTypeName}/{DatabaseName}/{IdMess?}")]
		public IActionResult ManagerFile(BackUpType BackUpTypeName, JobModel jobModel, Guid? IdMess)
		{
			var viewModel = _busConfigViewModel.GetBackUpTypeFileInformation(BackUpTypeName, jobModel.DatabaseName);
            viewModel.MessageBusViewModel = GetMessageBusViewModel(IdMess);
            return View(viewModel);
		}
		//[Route("/ManagerFile")]
		//public IActionResult ManagerFile()
		//{

		//	return View();
		//}

		[HttpPost("Backup")]
		public IActionResult Backup(ConfigurationBackUpViewModel BackUpViewModel)
		{
			var CreateJob = _BusBackup.CreateJob(BackUpViewModel);

			if (CreateJob.MessageStatus == MessageStatus.Error)
			{
				BackUpViewModel.MessageBusViewModel = CreateJob;
				return View("Index", BackUpViewModel);
			}
			else
			{
				ConfigurationBackUpViewModel configurationBackUpViewModel = new ConfigurationBackUpViewModel();
				configurationBackUpViewModel = _busConfigViewModel.GetConfigurationBackUpViewModelByJobName(BackUpViewModel.DatabaseConnectViewModel.DatabaseName, BackUpViewModel.BackupName);
				configurationBackUpViewModel.MessageBusViewModel = CreateJob;
				return View("Index", configurationBackUpViewModel);
			}
		}

		[Route("/DeleteJob/{DatabaseName}/{JobName}")]
		public IActionResult DeleteJob(JobModel jobModel)
		{
			var MessageBusViewModel = _BusBackup.DeleteJob(jobModel);
			if (MessageBusViewModel.MessageStatus == MessageStatus.Error)
			{
				var configurationBackUpViewModel = _busConfigViewModel.GetConfigurationBackUpViewModelByJobName(jobModel.DatabaseName, jobModel.JobName);
				configurationBackUpViewModel.MessageBusViewModel = MessageBusViewModel;
				return View("Index", configurationBackUpViewModel);
			}
			return Redirect("/"+jobModel.DatabaseName);
		}

		[Route("RunJobNow/{DatabaseName}/{JobName}")]
		[HttpGet]
		public IActionResult RunJobNow(JobModel jobModel)
		{
			var MessageBusViewModel = _BusBackup.StartJobNow(jobModel);
			return Redirect($"/{jobModel.DatabaseName}/{jobModel.JobName}");
		}

		[Route("RestoreBackUpNow")]
		[HttpPost]
		public IActionResult RestoreBackUpNow(BackUpType BackUpTypeName, string DatabaseName, string Path, string FileName)
		{
			var MessageBusViewModel = _BusBackup.RestoreBackUpNow(DatabaseName, Path, FileName);
			var idMess = Guid.NewGuid();
			_messageBusViewModels.Add(idMess, MessageBusViewModel);
			return Redirect($"ManagerFile/{BackUpTypeName}/{DatabaseName}/{idMess}");
		}

		[Route("ConfigRestore")]
		[HttpPost]
		public IActionResult ConfigRestore(ConfigRestoreViewModel configRestoreViewModel)
		{
			configRestoreViewModel = _busConfigViewModel.GetConfigRestoreViewModel(configRestoreViewModel);
			return View(configRestoreViewModel);
		}
		[Route("Recovery/{DatabaseName}/{BackUpTypeName?}")]
		[HttpGet]
		public IActionResult Recovery(BackUpType? BackUpTypeName, string DatabaseName)
		{
			var viewModel = _BusBackup.ExecuteRecoveryDatabase(DatabaseName);
			var idMess = Guid.NewGuid();
			_messageBusViewModels.Add(idMess, viewModel);
			if (BackUpTypeName == null)
			{
				var ManagerFolderViewModel = _busConfigViewModel.GetBackUpTypeFolderInformation(DatabaseName);
				ManagerFolderViewModel.MessageBusViewModel = viewModel;
				return Redirect($"/ManagerFolder/{DatabaseName}/{idMess}");

			}
			var ManagerFileViewModel = _busConfigViewModel.GetBackUpTypeFileInformation(BackUpTypeName.Value, DatabaseName);
			ManagerFileViewModel.MessageBusViewModel = viewModel;
			
			return Redirect($"/ManagerFile/{BackUpTypeName}/{DatabaseName}/{idMess}");
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		private MessageBusViewModel GetMessageBusViewModel(Guid? IdMess)
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
			return new MessageBusViewModel();
        }
    }
}