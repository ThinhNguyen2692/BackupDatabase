using Bus_backUpData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelProject.ViewModels;
using ModelProject.Models;
using static Bus_backUpData.Services.AutoModelMapper;
using ModelProject.Func;
using AutoMapper;
using System.Globalization;
using System.Net;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using HRM.SC.Core.Security;
using Microsoft.Data.SqlClient;
using DalStoredProcedure.Interface;
using ModelProject.ViewModels.ViewModelSeverConfig;
using DalBackup.Interface;
using ModelProject.ViewModels.ViewModelConnect;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ModelProject.ViewModels.RestoreViewModel;
using ModelProject.ViewModels.ModelRequest;

namespace Bus_backUpData.Services
{
	public class BusBackup : IBusBackup
	{
		private readonly IBusFTP _BusFTP;
		private readonly IBusConfigurationInformation _busConfigurationInformation;
		private readonly IBusScheduleTask _busScheduleTask;
		private readonly IBusConfigViewModel busConfigViewModel;
		private readonly IDalStoredProcedureServices _dalStoredProcedureServices;
		private readonly IDalDatabaseConnect _dalDatabaseConnect;
		private readonly IDalConfigurationBackUp _dalConfigurationBackUp;
		private readonly IMapper _mapper;
		public BusBackup(IBusFTP BusFTP, IBusConfigurationInformation busConfigurationInformation,
			IBusScheduleTask busScheduleTask, IBusConfigViewModel busConfigViewModel,
			IDalStoredProcedureServices dalStoredProcedureServices,
			IDalDatabaseConnect dalDatabaseConnect,
			IDalConfigurationBackUp dalConfigurationBackUp,
			 IMapper mapper)
		{
			_BusFTP = BusFTP;
			_busConfigurationInformation = busConfigurationInformation;
			_busScheduleTask = busScheduleTask;
			this.busConfigViewModel = busConfigViewModel;
			_dalStoredProcedureServices = dalStoredProcedureServices;
			_dalDatabaseConnect = dalDatabaseConnect;
			_dalConfigurationBackUp = dalConfigurationBackUp;
			_mapper = mapper;
		}
		/// <summary>
		/// Lưu cấu hình
		/// </summary>
		/// <param name="configurationBackUpViewModel"></param>
		/// <returns></returns>
		public bool SaveSetting(ConfigurationBackUp ConfigurationBackUp)
		{
			var LogName = string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy"));
			var mapper = MapperConfig<ConfigurationBackUpViewModel, ConfigurationBackUp>.InitializeAutomapper();
			//mã hóa mật khẩu
			// if(!string.IsNullOrEmpty(ConfigurationBackUp.FTPSetting.PassWord)) ConfigurationBackUp.FTPSetting.PassWord = Encryption.EncryptV2(ConfigurationBackUp.FTPSetting.PassWord);
			//cập nhật hoặc thêm mới cấu hình
			if (ConfigurationBackUp.Id != Guid.Empty)
			{
				WriteLogFile.WriteLog(LogName, string.Format("SaveSetting_{0}: {1}", ConfigurationBackUp, "Update Schedule task"), Setting.FoderBackUp);

				if (ConfigurationBackUp.IsScheduleBackup == true)
				{
					// cập nhật Schedule Task
					var UpdateScheduleTask = _busScheduleTask.UpdateScheduleTaskAsync(ConfigurationBackUp.ScheduleBackup, ConfigurationBackUp.BackupName, ConfigurationBackUp.Id);

					if (ConfigurationBackUp.FTPSetting.IsAutoDelete)
					{
						var UpdateScheduleTaskDeleteFTPAsync = _busScheduleTask.UpdateScheduleTaskDeleteFTPAsync(ConfigurationBackUp.FTPSetting.Months, ConfigurationBackUp.FTPSetting.Days, ConfigurationBackUp.BackupName, ConfigurationBackUp.Id);
					}
				}
			}
			else
			{
				WriteLogFile.WriteLog(LogName, string.Format("SaveSetting_{0}: {1}", ConfigurationBackUp, "Create Schedule task"), Setting.FoderBackUp);
				// tạo mới Schedule Task
				//ConfigurationBackUp.Id = Guid.NewGuid();
				if (ConfigurationBackUp.IsScheduleBackup == true)
				{
					var createScheduleTask = _busScheduleTask.CreateScheduleTaskAsync(ConfigurationBackUp.ScheduleBackup, ConfigurationBackUp.BackupName, ConfigurationBackUp.Id);
					if (ConfigurationBackUp.FTPSetting.IsAutoDelete)
					{
						var UpdateScheduleTaskDeleteFTPAsync = _busScheduleTask.CreateScheduleTaskDeleteFTPAsync(ConfigurationBackUp.FTPSetting.Months, ConfigurationBackUp.FTPSetting.Days, ConfigurationBackUp.BackupName, ConfigurationBackUp.Id);
					}
				}
			}
			_dalConfigurationBackUp.AddOrUpdate(ConfigurationBackUp);
			return true;
		}

		//Tạo job backup
		public MessageBusViewModel CreateJob(ConfigurationBackUpViewModel configurationBackUpViewModel)
		{
			List<string> Log = new List<string>();
			string LogMess = "CreateJob_Start---------------------" + configurationBackUpViewModel.BackupName + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss");
			Log.Add(LogMess);
			var LogName = string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy"));
			var configurationBackUpViewModelJson = System.Text.Json.JsonSerializer.Serialize(configurationBackUpViewModel);
			Log.Add(string.Format("Data_ConfigurationBackUpViewModel: {0}", configurationBackUpViewModelJson.ToString()));
			var MessageBus = new MessageBusViewModel();
			try
			{

				var ConfigurationBackUp = _mapper.Map<ConfigurationBackUp>(configurationBackUpViewModel);
				#region set dữ liệu cấu hình để gọi SQL Server Agent stored procedures
				var enabled = ConfigurationBackUp.IsScheduleBackup ? 1 : 0;
				var IsenabledJob = ConfigurationBackUp.IsEnabled ? 1 : 0;
				var Occurs = ConfigurationBackUp.ScheduleBackup.Occurs.ToString();
				int freq_interval = 0;
				int freq_recurrence_factor = 0;
				int freq_relative_interval = 0;
				int freq_subday_interval = 0;
				int freq_subday_type = 1;
				if (ConfigurationBackUp.ScheduleBackup.Occurs == ModelProject.Models.Occurs.Day)
				{
					freq_interval = ConfigurationBackUp.ScheduleBackup.RecursEveryDay;
					if (ConfigurationBackUp.ScheduleBackup.ActionType == true)
					{
						freq_subday_interval = ConfigurationBackUp.ScheduleBackup.FreqSubdayInterval;
						freq_subday_type = (int)ConfigurationBackUp.ScheduleBackup.FreqSubdayType;
					}
				}
				else if (ConfigurationBackUp.ScheduleBackup.Occurs == ModelProject.Models.Occurs.Weekly)
				{
					freq_interval = ConfigurationBackUp.ScheduleBackup.ScheduleBackupWeeklies.Sum(x => x.Weekly.Value);
					freq_recurrence_factor = ConfigurationBackUp.ScheduleBackup.RecursEveryWeekly;
				}
				else
				{
					if (ConfigurationBackUp.ScheduleBackup.MonthlyDay)
					{
						freq_interval = ConfigurationBackUp.ScheduleBackup.DayEvery;
						freq_recurrence_factor = ConfigurationBackUp.ScheduleBackup.DayMonth;
					}
					else
					{
						freq_interval = ((int)ConfigurationBackUp.ScheduleBackup.TheWeekly);
						freq_recurrence_factor = ConfigurationBackUp.ScheduleBackup.TheMonth;
						freq_relative_interval = ((int)ConfigurationBackUp.ScheduleBackup.TheOrder);
					}
				}
				var parsedTime = DateTime.ParseExact(ConfigurationBackUp.ScheduleBackup.FirstDate.ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
				if (!int.TryParse(parsedTime.ToString("yyyyMMdd"), out int FirstDate)) { FirstDate = 0; }
				if (!int.TryParse(parsedTime.ToString("HHmm") + "00", out int runtime)) { runtime = 0; }
				if (!int.TryParse("99991231", out int EndDate)) { EndDate = 0; }
				if (!int.TryParse("235959", out int EndTime)) { EndTime = 0; }
				if (ConfigurationBackUp.ScheduleBackup.ActionType == true)
				{
					if (!int.TryParse(ConfigurationBackUp.ScheduleBackup.EndTime.ToString("HHmmss"), out EndTime)) { EndTime = 0; }
				}
				string BackUpTypeString = "Full";
				if (ConfigurationBackUp.BackUpSetting.BackUpType == BackUpType.Differential)
				{
					BackUpTypeString = "DIFF";
				}
				else if (ConfigurationBackUp.BackUpSetting.BackUpType == BackUpType.Log)
				{
					BackUpTypeString = "LOG";
				}
				#endregion set dữ liệu cấu hình để gọi SQL Server Agent stored procedures

				//SqlParameters gọi stored
				var SqlParameters = new List<SqlParameter>();
				SqlParameters.Add(new SqlParameter("@DatabaseName", configurationBackUpViewModel.DatabaseConnectViewModel.DatabaseName.ToString()));
				SqlParameters.Add(new SqlParameter("@BackupName", ConfigurationBackUp.BackupName));
				SqlParameters.Add(new SqlParameter("@BackupType", ConfigurationBackUp.BackUpSetting.BackUpType.ToString()));
				SqlParameters.Add(new SqlParameter("@BackupPath", ConfigurationBackUp.BackUpSetting.Path));
				SqlParameters.Add(new SqlParameter("@enabled", enabled));
				SqlParameters.Add(new SqlParameter("@Occurs_freq_type", ((int)ConfigurationBackUp.ScheduleBackup.Occurs)));
				SqlParameters.Add(new SqlParameter("@freq_relative_interval_parameters", freq_interval));
				SqlParameters.Add(new SqlParameter("@Day_Recurs_every", freq_relative_interval));
				SqlParameters.Add(new SqlParameter("@freq_recurrence_factor1", freq_recurrence_factor));
				SqlParameters.Add(new SqlParameter("@start_date", FirstDate));
				SqlParameters.Add(new SqlParameter("@end_date", EndDate));
				SqlParameters.Add(new SqlParameter("@start_time", runtime));
				SqlParameters.Add(new SqlParameter("@IsenabledJob", IsenabledJob));
				SqlParameters.Add(new SqlParameter("@freqSubdayType", freq_subday_type));
				SqlParameters.Add(new SqlParameter("@freqSubdayInterval", freq_subday_interval));
				SqlParameters.Add(new SqlParameter("@end_time", EndTime));

				var temp = SqlParameters.Select(x => x.SqlValue.ToString()).ToList();

				if (temp != null)
				{
					var SqlParametersjson = System.Text.Json.JsonSerializer.Serialize(temp);
					Log.Add(string.Format("Dữ liệu gọi store: {0}", SqlParametersjson.ToString()));
				}
				//update or create backup
				try
				{
					var databaseConnect = _dalDatabaseConnect.FirstOrDefault(configurationBackUpViewModel.DatabaseConnectViewModel.DatabaseName);
					if (databaseConnect == null)
					{
						MessageBus.MessageStatus = MessageStatus.Error;
						MessageBus.Message = "Database Connect Error";
						return MessageBus;
					}
					ConfigurationBackUp.DatabaseConnectId = databaseConnect.Id;
					var connectionstring = SettingConnection.GetConnection(databaseConnect);
					if (ConfigurationBackUp.Id != Guid.Empty) //update
					{
						_dalStoredProcedureServices.ExecuteSqlRaw(connectionstring, StringSql.SQlBackupUpdate, SqlParameters);
					}
					else //create
					{
						var SysJobListName = _dalStoredProcedureServices.SqlQueryRaw(connectionstring, StringSql.SQlsysjobs).ToList();
						var SysJobName = SysJobListName.FirstOrDefault(x => x.ToLower() == ConfigurationBackUp.BackupName.ToLower());
						if (SysJobName != null)
						{
							MessageBus.MessageStatus = MessageStatus.Error;
							MessageBus.Message = "The name Job already exists";
							return MessageBus;
						}
						_dalStoredProcedureServices.ExecuteSqlRaw(connectionstring, StringSql.SQlBackup, SqlParameters);
					}
				}
				catch (Exception ex)
				{
					Log.Add(string.Format("CreateJob_BackUpError_ExecuteSqlRaw: {0}", ex.Message));
				}
				//lưu cấu hình
				Log.Add("SaveSetting_Start--------------------" + configurationBackUpViewModel.BackupName + "--------" + DateTime.Now.ToString("ddMMyyyy"));
				WriteLogFile.WriteLog(LogName, Log, Setting.FoderBackUp);
				Log = new List<string>();

				SaveSetting(ConfigurationBackUp);

				Log.Add("SaveSetting_End-----------------------" + configurationBackUpViewModel.BackupName + "--------" + DateTime.Now.ToString("ddMMyyyy"));
				MessageBus.MessageStatus = MessageStatus.Success;
				MessageBus.Message = "Success";
				Log.Add("CreateJob_End-------------------------" + configurationBackUpViewModel.BackupName + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"));
				WriteLogFile.WriteLog(LogName, Log, Setting.FoderBackUp);
				var checkConnectFTp = _BusFTP.CheckFtpConnection(ConfigurationBackUp.FTPSetting.HostName, ConfigurationBackUp.FTPSetting.UserName, ConfigurationBackUp.FTPSetting.PassWord);
				if (!checkConnectFTp)
				{
                    MessageBus.MessageStatus = MessageStatus.FTPFail;
                    MessageBus.Message = "FTP connect fail";

                }
				return MessageBus;
			}
			catch (Exception ex)
			{
				Log.Add(string.Format("CreateJob_BackUpError_Notdetermined: {0}", ex.Message));
				Log.Add("CreateJob_End-------------------------" + configurationBackUpViewModel.BackupName + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"));
				WriteLogFile.WriteLog(LogName, Log, Setting.FoderBackUp);
				MessageBus.MessageStatus = MessageStatus.Error;
				MessageBus.Message = "Error";
				return MessageBus;
			}
		}
		/// <summary>
		/// Delete Job
		/// </summary>
		/// <param name="JobName">Jobname để xóa</param>
		/// <returns></returns>
		public MessageBusViewModel DeleteJob(JobModel jobModel)
		{
			var MessageBusViewModel = new MessageBusViewModel();
            jobModel = _busConfigurationInformation.GetFullJobModel(jobModel);
            try
			{
				if (_busConfigurationInformation.IsJob(jobModel.JobName, jobModel.DatabaseName))
				{
					WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")),
						"DeleteJob_Start--------------" + jobModel.JobName + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderBackUp);
					var SqlParameters = new List<SqlParameter>();
					SqlParameters.Add(new SqlParameter("@job_name", jobModel.JobName));
					var connectString = _busConfigurationInformation.GetConnectStringByJobName(jobModel.JobName,jobModel.DatabaseName);
					_dalStoredProcedureServices.ExecuteSqlRaw(connectString, StringSql.SQlsp_delete_job, SqlParameters);
					//_context.Database
					// .ExecuteSqlRaw(StringSql.SQlsp_delete_job, SqlParameters);
					//_busScheduleTask.DeleteScheduleTask(JobName);
				}
				var DeleteJsonBackUp = _busConfigurationInformation.DeleteJsonBackUpByID(jobModel.Id);
				if (DeleteJsonBackUp == true)
				{
					_busScheduleTask.DeleteScheduleTask(jobModel.JobName, jobModel.Id);
				}
				MessageBusViewModel.MessageStatus = DeleteJsonBackUp ? MessageStatus.Success : MessageStatus.Error;
				MessageBusViewModel.Message = DeleteJsonBackUp ? "Success" : "Error";

			}
			catch (Exception ex)
			{
				WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), "DeleteJob_Error: " + ex.Message, Setting.FoderBackUp);

				MessageBusViewModel.MessageStatus = MessageStatus.Error;
				MessageBusViewModel.Message = "Error";
			}
			WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")),
				  "DeleteJob_End---------------" + jobModel.JobName + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderBackUp);
			return MessageBusViewModel;
		}

		public MessageBusViewModel StartJobNow(JobModel jobModel)
		{
			WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")),
				  "StartJobNow_Start--------------" + jobModel.JobName + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderBackUp);
			var MessageBusViewModel = new MessageBusViewModel();
			try
			{
                jobModel = _busConfigurationInformation.GetFullJobModel(jobModel);

                var SqlParameters = new List<SqlParameter>();
				SqlParameters.Add(new SqlParameter("@job_name", jobModel.JobName));
				var connectionstring = _busConfigurationInformation.GetConnectStringByJobName(jobModel.JobName, jobModel.DatabaseName);
				_dalStoredProcedureServices.ExecuteSqlRaw(connectionstring, StringSql.SQlsp_start_job, SqlParameters);
				var conFig = _dalConfigurationBackUp.GetData().FirstOrDefault(x => x.BackupName.ToLower() == jobModel.JobName.ToLower());

				if (conFig != null)
				{
					_BusFTP.PushFPT(conFig, null);
				}

				MessageBusViewModel.MessageStatus = MessageStatus.Success;
				MessageBusViewModel.Message = "Success";
			}
			catch (Exception ex)
			{
				WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), "DeleteJob_Error: " + ex.Message, Setting.FoderBackUp);
				MessageBusViewModel.MessageStatus = MessageStatus.Error;
				MessageBusViewModel.Message = "Error";
			}
			WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")),
				 "StartJobNow_End--------------" + jobModel.JobName + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderBackUp);
			return MessageBusViewModel;
		}

		public MessageBusViewModel RestoreBackUpNow(string DatabaseName, string Path, string FileName)
		{
			var messageBusViewModel = new MessageBusViewModel();
			try
			{
				string pathFileBackup = $"{Setting.PathbackUp}{Path}\\{FileName}";
				var connectionstring = _busConfigurationInformation.GetConnectStringByDatabaseName(DatabaseName);
				if (string.IsNullOrEmpty(connectionstring))
				{
					messageBusViewModel.MessageStatus = MessageStatus.Error;
					messageBusViewModel.Message = MessageStatus.Error.ToString();
					return messageBusViewModel;
				}
				var isCheckDb = _dalStoredProcedureServices.CheckConnection(connectionstring);
				var IsRecovery = false;
				if (isCheckDb)
				{
				 IsRecovery = _busConfigurationInformation.IsRecovery(DatabaseName);
				}
				connectionstring = connectionstring.Replace(DatabaseName, Setting.UsingMaster);
				// Mở kết nối đến master database
				using (SqlConnection connection = new SqlConnection(connectionstring))
				{
					connection.Open();
					if (isCheckDb) {
						string SQlSINGLE_USER = string.Format(StringSql.SQlSINGLE_USER, DatabaseName);
						using (SqlCommand command = new SqlCommand(SQlSINGLE_USER, connection))
						{
							//command.Parameters.AddWithValue("@DatabaseName", DatabaseName);
							command.ExecuteNonQuery();
						}
					}
					// Tạo và thực thi lệnh RESTORE DATABASE
					using (SqlCommand command = new SqlCommand(StringSql.SQlRestoreBackup, connection))
					{
						command.Parameters.AddWithValue("@DatabaseName", DatabaseName);
						command.Parameters.AddWithValue("@BackupFilePath", FileName);
						command.ExecuteNonQuery();
					}
					if (isCheckDb && !IsRecovery)
					{
						string SQlMULTI_USER = string.Format(StringSql.SQlMULTI_USER, DatabaseName);
						using (SqlCommand command = new SqlCommand(SQlMULTI_USER, connection))
						{
							command.ExecuteNonQuery();
						}
					}
				}
				messageBusViewModel.MessageStatus = MessageStatus.Success;
				messageBusViewModel.Message = MessageStatus.Success.ToString();
			}
			catch (Exception ex)
			{
				messageBusViewModel.MessageStatus = MessageStatus.Error;
				messageBusViewModel.Message = ex.Message;
			}

			return messageBusViewModel;
		}

		public MessageBusViewModel ExecuteRecoveryDatabase(string DatabaseName)
		{
			var Recovery = _busConfigurationInformation.StateDescDB(DatabaseName);
			var messageBusViewModel = new MessageBusViewModel();
			if (string.IsNullOrEmpty(Recovery)) {
				messageBusViewModel.MessageStatus = MessageStatus.Error;
				messageBusViewModel.Message = $"{DatabaseName} not found.";
				return messageBusViewModel;
			}else if (Recovery == "ONLINE")
			{
				messageBusViewModel.MessageStatus = MessageStatus.Error;
				messageBusViewModel.Message = $"{DatabaseName} in the online state";
				return messageBusViewModel;
			}
			var connectionstring = _busConfigurationInformation.GetConnectStringByDatabaseName(DatabaseName);
			var SqlParameters = new List<SqlParameter>();
			SqlParameters.Add(new SqlParameter("@DatabaseName", DatabaseName));
			connectionstring = connectionstring.Replace(DatabaseName, Setting.UsingMaster);
			 _dalStoredProcedureServices.ExecuteSqlRaw(connectionstring, StringSql.SQlRestoreBackupRecovery, SqlParameters);
			var isCheckDb = _dalStoredProcedureServices.CheckConnection(connectionstring);
			if (!isCheckDb)
			{
				messageBusViewModel.MessageStatus = MessageStatus.Error;
				messageBusViewModel.Message = $"Restore {MessageStatus.Error.ToString()}";
				return messageBusViewModel;
			}
			messageBusViewModel.MessageStatus = MessageStatus.Success;
			messageBusViewModel.Message = $"Restore {MessageStatus.Success.ToString()}";
			return messageBusViewModel;
		}
	}
}
