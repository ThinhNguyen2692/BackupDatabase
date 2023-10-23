using AdminLayout_Vuexy.Models;
using Bus_backUpData.Func;
using Bus_backUpData.Interface;
using Bus_backUpData.Models;
using Bus_backUpData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminLayout_Vuexy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusBackup _BusBackup;
        private readonly IBusConfigViewModel _BusConfig;

        public HomeController(ILogger<HomeController> logger, IBusBackup busBackup, IBusConfigViewModel busConfig)
        {
            _logger = logger;
            _BusBackup = busBackup;
            _BusConfig = busConfig;
        }

        [Route("/{JobName?}")]
        [HttpGet]
        public IActionResult Index(string JobName = "")
       {

                ConfigurationBackUpViewModel configurationBackUpViewModel = new ConfigurationBackUpViewModel();
                if (!string.IsNullOrEmpty(JobName))
                {
                    configurationBackUpViewModel = _BusConfig.GetConfigurationBackUpViewModelByJobName(JobName);
                }
                if(configurationBackUpViewModel.Id  == Guid.Empty)
                {
                    var ListConfig = _BusConfig.GetConfigurationBackUpViewModel().ToList();
                    var BackUpViewModel = new BackUpViewModel();
                    BackUpViewModel.Name = string.IsNullOrEmpty(JobName) ? "JobNew" : JobName;
                    BackUpViewModel.Id = Guid.Empty;
                    BackUpViewModel.IsSelect = false;
                    configurationBackUpViewModel.ScheduleBackup.RecursEveryWeekly = 1;
                    configurationBackUpViewModel.ScheduleBackup.RecursEveryDay = 1;
                    configurationBackUpViewModel.ScheduleBackup.DayEvery = 1;
                    configurationBackUpViewModel.ScheduleBackup.DayMonth = 1;
                    configurationBackUpViewModel.FTPSetting.Months = 1;
                    configurationBackUpViewModel.BackUpSetting.Name = Setting.DatabaseName;
                    configurationBackUpViewModel.BackUpSetting.Path = Setting.PathbackUp;
                    configurationBackUpViewModel.BackupName = BackUpViewModel.Name;
                    configurationBackUpViewModel.IsEnabled = true;
                    configurationBackUpViewModel.IsScheduleBackup = true;
                    configurationBackUpViewModel.BackUpViewModels.Add(BackUpViewModel);
                    configurationBackUpViewModel.BackUpViewModels.AddRange(ListConfig.Select(x => new BackUpViewModel() { Id = x.Id, Name = x.BackupName }).ToList());
                    configurationBackUpViewModel.MessageBusViewModel.MessageStatus = MessageStatus.None;

                }
                return View("Index", configurationBackUpViewModel);
           
        }

        public IActionResult Privacy()
        {
            return View();
        }

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
                configurationBackUpViewModel = _BusConfig.GetConfigurationBackUpViewModelByJobName(BackUpViewModel.BackupName);
                configurationBackUpViewModel.MessageBusViewModel = CreateJob;
                return View("Index", configurationBackUpViewModel);
            }
        }

        [Route("/DeleteJob/{JobName?}")]
        public IActionResult DeleteJob(string JobName)
        {
            var MessageBusViewModel = _BusBackup.DeleteJob(JobName);
            if (MessageBusViewModel.MessageStatus == MessageStatus.Error)
            {
                var configurationBackUpViewModel = _BusConfig.GetConfigurationBackUpViewModelByJobName(JobName);
                configurationBackUpViewModel.MessageBusViewModel = MessageBusViewModel;
                return View("Index", configurationBackUpViewModel);
            }
            return Redirect("/");
        }

        [Route("RunJobNow/{JobName?}")]
        [HttpGet]
        public IActionResult RunJobNow(string JobName)
        {
            var MessageBusViewModel = _BusBackup.StartJobNow(JobName);
            return Redirect("/" + JobName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}