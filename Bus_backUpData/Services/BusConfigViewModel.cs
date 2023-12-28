using Bus_backUpData.Data;
using ModelProject.Func;
using Bus_backUpData.Interface;
using ModelProject.Models;
using ModelProject.ViewModels;
using HRM.SC.Core.Security;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bus_backUpData.Services.AutoModelMapper;
using DalBackup.Interface;
using DalStoredProcedure.Interface;
using AutoMapper;
using ModelProject.ViewModels.ViewModelConnect;
using DalBackup.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ModelProject.ViewModels.RestoreViewModel;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Bus_backUpData.Services
{
    public class BusConfigViewModel : IBusConfigViewModel
    {

        public Context _context;
        private readonly IDalConfigurationBackUp _dalConfigurationBackUp;
        private readonly IDalStoredProcedureServices _dalStoredProcedureServices;
        private readonly IBusConfigurationInformation _busConfigurationInformation;
        private readonly IBusConfigServer _busConfigServer;
        private readonly IDalDatabaseConnect _dalDatabaseConnect;
		private readonly IMapper _mapper;
		public BusConfigViewModel(Context Context, 
            IDalConfigurationBackUp dalConfigurationBackUp, 
            IDalStoredProcedureServices dalStoredProcedureServices, 
            IBusConfigurationInformation busConfigurationInformation,
            IMapper mapper, 
            IDalDatabaseConnect dalDatabaseConnect,
            IBusConfigServer busConfigServer)
        {
            _context = Context;
            Setting.DatabaseName = _context.Database.GetDbConnection().Database;
            _dalConfigurationBackUp = dalConfigurationBackUp;
			_dalStoredProcedureServices = dalStoredProcedureServices;
            _busConfigurationInformation = busConfigurationInformation;
            _mapper = mapper;
            _dalDatabaseConnect = dalDatabaseConnect;
            _busConfigServer = busConfigServer;

        }
        /// <summary>
        /// Get all danh sach cau hinh
        /// </summary>
        /// <returns></returns>
        public List<ConfigurationBackUpViewModel> GetConfigurationBackUpViewModel(string DatabaseName)
        {
            var data = _dalConfigurationBackUp.GetData(DatabaseName);
            var ConfigurationBackUpViewModel = MapConfigurationBackUpViewModel(data);
            return ConfigurationBackUpViewModel;
        }
        public List<ConfigurationBackUpViewModel> GetConfigurationBackUpViewModel(Guid id)
        {
            var data = _dalConfigurationBackUp.GetData(id);
            var ConfigurationBackUpViewModel = MapConfigurationBackUpViewModel(data);
            return ConfigurationBackUpViewModel;
        }
        public List<ConfigurationBackUpViewModel> GetConfigurationBackUpViewModel()
        {
            var data = _dalConfigurationBackUp.GetData();
            var ConfigurationBackUpViewModel = MapConfigurationBackUpViewModel(data);
            return ConfigurationBackUpViewModel;
        }
        public List<ConfigurationBackUpViewModel> MapConfigurationBackUpViewModel(List<ConfigurationBackUp> configurationBackUps)
        {
            var ConfigurationBackUpViewModel = _mapper.Map<List<ConfigurationBackUpViewModel>>(configurationBackUps);
            ConfigurationBackUpViewModel.ForEach(x => { x.BackUpSetting.Name = x.DatabaseConnectViewModel.DatabaseName; });
            return ConfigurationBackUpViewModel;
        }
        /// <summary>
        /// get cấu hình theo JobName
        /// </summary>
        /// <param name="JobName"></param>
        /// <returns></returns>
        public ConfigurationBackUpViewModel GetConfigurationBackUpViewModelByJobName(string DatabaseName , string JobName)
        {
            var ConfigurationBackUpViewModels = GetConfigurationBackUpViewModel(DatabaseName);
            var ConfigurationBackUpViewModel = ConfigurationBackUpViewModels.FirstOrDefault(x => x.BackupName == JobName);
            if (ConfigurationBackUpViewModel == null) return new ConfigurationBackUpViewModel();
            ConfigurationBackUpViewModel.BackUpViewModels = ConfigurationBackUpViewModels.Select(x => new BackUpViewModel() { Id = x.Id, Name = x.BackupName }).ToList();
            ConfigurationBackUpViewModel.JobHistoryViewModels = GetJobHistoryViewModels(ConfigurationBackUpViewModel);
            ConfigurationBackUpViewModel.FTPSetting.PassWord = ConfigurationBackUpViewModel.FTPSetting.PassWord;
            return ConfigurationBackUpViewModel;
        }


        public ConfigurationBackUpViewModel GetConfigurationBackUpViewModelByJobId(Guid ConfigId)
        {
            var ConfigurationBackUpViewModels = GetConfigurationBackUpViewModel();
            var ConfigurationBackUpViewModel = ConfigurationBackUpViewModels.FirstOrDefault(x => x.Id == ConfigId);
            if (ConfigurationBackUpViewModel == null) return new ConfigurationBackUpViewModel();         
            ConfigurationBackUpViewModel.JobHistoryViewModels = GetJobHistoryViewModels(ConfigurationBackUpViewModel);
            ConfigurationBackUpViewModel.FTPSetting.PassWord = ConfigurationBackUpViewModel.FTPSetting.PassWord;
            return ConfigurationBackUpViewModel;
        }

        public DatabaseConnectViewModel GetDatabaseNameConnectViewModel(string DatabaseName)
        {
            var databaseConnect = _dalDatabaseConnect.FirstOrDefault(DatabaseName);
            if(databaseConnect == null) return new DatabaseConnectViewModel();
            var viewModel = _mapper.Map<DatabaseConnectViewModel>(databaseConnect);
            return viewModel;
        }

        /// <summary>
        /// Get viewhistory Job
        /// </summary>
        /// <param name="jobname"></param>
        /// <returns></returns>
        public List<JobHistoryViewModel> GetJobHistoryViewModels(ConfigurationBackUpViewModel  configurationBackUpViewModel)
        {
            if (_busConfigurationInformation.IsJob(configurationBackUpViewModel.Id) == false) return new List<JobHistoryViewModel>();
            //var SqlParametersJobHistory = new List<SqlParameter>();
            //SqlParametersJobHistory.Add(new SqlParameter("@job_name", jobname));
            //var tesst = new {  };
            // var JobHistory = _context.Database.SqlQueryRaw<>("exec msdb.dbo.sp_help_jobhistory null, @job_name", new SqlParameter("job_name", jobname));

            List<JobHistoryViewModel> JobHistoryViewModels = new List<JobHistoryViewModel>();
			var connectString = _busConfigurationInformation.GetConnectStringByJobId(configurationBackUpViewModel.Id);
			using (SqlConnection connection = new SqlConnection(connectString))
            {
                connection.Open();

                string queryString = $"exec msdb.dbo.sp_help_jobhistory null, @job_name, null, null, null, null , null ,null, null , null , null , null , null , null, @mode";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    // Create a parameterized query
                    command.Parameters.AddWithValue("@job_name", configurationBackUpViewModel.BackupName);
                    command.Parameters.AddWithValue("@mode", "FULL");
                    //command.Parameters.AddWithValue("@mode", "FULL");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var JobHistoryViewModel = new JobHistoryViewModel();
                            JobHistoryViewModel.instance_id = int.Parse(reader[0].ToString());
                            JobHistoryViewModel.job_id = Guid.Parse(reader[1].ToString());
                            JobHistoryViewModel.job_name = reader[2].ToString();
                            JobHistoryViewModel.message = reader[7].ToString();
                            JobHistoryViewModel.run_status = (RunStatus)int.Parse(reader[8].ToString());
                            JobHistoryViewModel.run_date = int.Parse(reader[9].ToString());
                            JobHistoryViewModel.run_time = int.Parse(reader[10].ToString());
                            JobHistoryViewModel.run_duration = int.Parse(reader[11].ToString());
                            JobHistoryViewModel.server = reader[16].ToString();
                            JobHistoryViewModels.Add(JobHistoryViewModel);
                        }
                    }
                }
            }
            //IQueryable<sysjobhistory> orders = _context.Database.SqlQuery<sysjobhistory>($"select *  from msdb.dbo.sysjobhistory");
            return JobHistoryViewModels;
        }
      

        public ManagerFolderViewModel GetBackUpTypeFolderInformation(string DatabaseName)
        {
			var ServerName = _busConfigurationInformation.GetServerNameByDatabaseName(DatabaseName);

			if (string.IsNullOrEmpty(ServerName)) return new ManagerFolderViewModel();
			var ServerNameStr = ServerName;
			ServerNameStr = ServerNameStr.Replace("\\", "$");
            var managerFolderViewModel = new ManagerFolderViewModel();
            managerFolderViewModel.DatabaseConnectViewModel.ServerName = ServerNameStr;
            managerFolderViewModel.DatabaseConnectViewModel.DatabaseName = DatabaseName;
			string pathLocation = Setting.PathbackUp + $"\\{ServerNameStr}\\{DatabaseName}";
			var directoryFolder = new DirectoryInfo(pathLocation);
            if (!directoryFolder.Exists)
            {
				managerFolderViewModel.MessageBusViewModel.MessageStatus = MessageStatus.Error;
                managerFolderViewModel.MessageBusViewModel.Message = "No bak files found.";
			}
			foreach (var value in Enum.GetValues(typeof(BackUpType)))
            {
                var backUpTypeViewModel = new BackUpTypeViewModel();
                backUpTypeViewModel.Name = value.ToString();
				 pathLocation = Setting.PathbackUp + $"\\{ServerNameStr}\\{DatabaseName}\\{value.ToString()}";
				var directoryInfo = new DirectoryInfo(pathLocation);
                long FolderSize = 0;
				if (directoryInfo.Exists)
				{
					// Lấy thời gian cập nhật gần nhất của thư mục
					backUpTypeViewModel.LastUpdateTime = directoryInfo.LastWriteTime;
					FolderSize = CalculateFolderSize(directoryInfo);
                    FolderSize = CalculateFolderSizeMB(FolderSize);
                }               
				backUpTypeViewModel.FolderSize = FolderSize +" MB";
				managerFolderViewModel.BackUpTypeFolder.Add(backUpTypeViewModel);
			}
			managerFolderViewModel.IsRecovery = _busConfigurationInformation.IsRecovery(DatabaseName);
			return managerFolderViewModel;
		}

        //tính size Folder MB
        private long CalculateFolderSizeMB(long fileSize)
        {
			double folderSizeKB = fileSize / 1024.0;
			double folderSizeMB = folderSizeKB / 1024.0;

            return (long)folderSizeMB;
		}

        /// <summary>
        /// tính size Folder
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
		private long CalculateFolderSize(DirectoryInfo directory)
		{
			long folderSize = 0;

			// Lặp qua tất cả các tệp và thư mục trong thư mục
			foreach (FileInfo file in directory.GetFiles())
			{
				folderSize += file.Length;
			}

			foreach (DirectoryInfo subDirectory in directory.GetDirectories())
			{
				folderSize += CalculateFolderSize(subDirectory);
			}

			return folderSize;
		}

        /// <summary>
        /// đọc file bak theo backUpType
        /// </summary>
        /// <param name="backUpType"></param>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>
		public ManagerFileViewModel GetBackUpTypeFileInformation(BackUpType backUpType, string DatabaseName)
        {
			var ServerName = _busConfigurationInformation.GetServerNameByDatabaseName(DatabaseName);

			if (string.IsNullOrEmpty(ServerName)) return new ManagerFileViewModel();
			var ServerNameStr = ServerName;
			ServerNameStr = ServerNameStr.Replace("\\", "$");
			var managerFolderViewModel = new ManagerFileViewModel();
			managerFolderViewModel.DatabaseConnectViewModel.ServerName = ServerNameStr;
			managerFolderViewModel.DatabaseConnectViewModel.DatabaseName = DatabaseName;
            string path = $"\\{ServerNameStr}\\{DatabaseName}\\{backUpType.ToString()}";
			managerFolderViewModel.PathLocation = path;
			string pathLocation = Setting.PathbackUp + path;
			var directoryInfo = new DirectoryInfo(pathLocation);
            if (directoryInfo.Exists)
            {
				var myFiledirectory = directoryInfo.GetFiles().ToList();
				foreach (var item in myFiledirectory)
				{
					var fileInfomationViewModel = new FileInfomationViewModel();
					fileInfomationViewModel.Name = item.Name;
					fileInfomationViewModel.FullName = item.FullName;
					fileInfomationViewModel.Size = CalculateFolderSizeMB(item.Length)+ " MB";
					fileInfomationViewModel.Extension = string.IsNullOrEmpty(item.Extension) ? string.Empty : item.Extension.Substring(1);
					fileInfomationViewModel.LastUpdateTime = item.LastWriteTime;
					managerFolderViewModel.FileInfomationViewModels.Add(fileInfomationViewModel);
				}
				managerFolderViewModel.FileInfomationViewModels = managerFolderViewModel.FileInfomationViewModels.OrderByDescending(x => x.LastUpdateTime).ToList();
			}

			managerFolderViewModel.BackUpType = backUpType;
			managerFolderViewModel.IsRecovery = _busConfigurationInformation.IsRecovery(DatabaseName);
			return managerFolderViewModel;
        }
	
        public ConfigRestoreViewModel GetConfigRestoreViewModel(ConfigRestoreViewModel configRestoreViewModel)
        {
            var serverConnect = _dalDatabaseConnect.FirstOrDefault(configRestoreViewModel.DatabaseName);
            if (serverConnect != null)
            {
                configRestoreViewModel.DatabaseConnectViewModel.ServerName = serverConnect.ServerConnects.ServerName;
                configRestoreViewModel.DatabaseConnectViewModel.PassWord = serverConnect.ServerConnects.PassWord;
                configRestoreViewModel.DatabaseConnectViewModel.UserName = serverConnect.ServerConnects.UserName;
            }
            return configRestoreViewModel;
        }
    }
}
