﻿using Bus_backUpData.Interface;
using Bus_backUpData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelProject.ViewModels;
using ModelProject.Models;
using static Bus_backUpData.Services.AutoModelMapper;
using ModelProject.Func;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Globalization;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.Arm;
using HRM.SC.Core.Security;
using Azure;

namespace Bus_backUpData.Services
{
    public class BusBackup : IBusBackup
    {
        private Context _context;
        private IBusFTP _BusFTP;
        private IBusConfigurationBackUp _busConfigurationBackUp;
        private IBusScheduleTask _busScheduleTask;
        private IBusConfigViewModel busConfigViewModel;
        public BusBackup(Context Context, IBusFTP BusFTP, IBusConfigurationBackUp busConfigurationBackUp, IBusScheduleTask busScheduleTask, IBusConfigViewModel busConfigViewModel)
        {
            _context = Context;
            _BusFTP = BusFTP;
            Setting.DatabaseName = _context.Database.GetDbConnection().Database;
            _busConfigurationBackUp = busConfigurationBackUp;
            _busScheduleTask = busScheduleTask;
            this.busConfigViewModel = busConfigViewModel;
        }
        /// <summary>
        /// Lưu cấu hình
        /// </summary>
        /// <param name="configurationBackUpViewModel"></param>
        /// <returns></returns>
        public bool SaveSetting(ConfigurationBackUpViewModel configurationBackUpViewModel)
        {
            var LogName = string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy"));
            List<ConfigurationBackUp> configurationBackUps = new List<ConfigurationBackUp>();
            configurationBackUps = _busConfigurationBackUp.LoadJsonBackUp();
            var mapper = MapperConfig<ConfigurationBackUpViewModel, ConfigurationBackUp>.InitializeAutomapper();
            var ConfigurationBackUp = mapper.Map<ConfigurationBackUpViewModel, ConfigurationBackUp>(configurationBackUpViewModel);
            //mã hóa mật khẩu
            if(!string.IsNullOrEmpty(ConfigurationBackUp.FTPSetting.PassWord)) ConfigurationBackUp.FTPSetting.PassWord = Encryption.EncryptV2(ConfigurationBackUp.FTPSetting.PassWord);
            //cập nhật hoặc thêm mới cấu hình
            if (ConfigurationBackUp.Id != Guid.Empty)
            {
                WriteLogFile.WriteLog(LogName, string.Format("SaveSetting_{0}: {1}", configurationBackUpViewModel, "Update Schedule task"), Setting.FoderBackUp);

                if (ConfigurationBackUp.IsScheduleBackup == true)
                {
                    // cập nhật Schedule Task
                    var UpdateScheduleTask = _busScheduleTask.UpdateScheduleTaskAsync(ConfigurationBackUp.ScheduleBackup, ConfigurationBackUp.BackupName);

                    if (ConfigurationBackUp.FTPSetting.IsAutoDelete)
                    {
                        var UpdateScheduleTaskDeleteFTPAsync = _busScheduleTask.UpdateScheduleTaskDeleteFTPAsync(ConfigurationBackUp.FTPSetting.Months, ConfigurationBackUp.FTPSetting.Days, ConfigurationBackUp.BackupName);

                    }
                }

                _busConfigurationBackUp.DeleteJsonBackUp(configurationBackUpViewModel.BackupName);
            }
            else
            {
                WriteLogFile.WriteLog(LogName, string.Format("SaveSetting_{0}: {1}", configurationBackUpViewModel, "Create Schedule task"), Setting.FoderBackUp);
                // tạo mới Schedule Task
                ConfigurationBackUp.Id = Guid.NewGuid();
                if (ConfigurationBackUp.IsScheduleBackup == true)
                {
                    var createScheduleTask = _busScheduleTask.CreateScheduleTaskAsync(ConfigurationBackUp.ScheduleBackup, ConfigurationBackUp.BackupName);
                    if (ConfigurationBackUp.FTPSetting.IsAutoDelete)
                    {
                        var UpdateScheduleTaskDeleteFTPAsync = _busScheduleTask.CreateScheduleTaskDeleteFTPAsync(ConfigurationBackUp.FTPSetting.Months, ConfigurationBackUp.FTPSetting.Days, ConfigurationBackUp.BackupName);
                    }
                }
            }
            _busConfigurationBackUp.AddJsonBackUp(ConfigurationBackUp);
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
              
                var mapper = MapperConfig<ConfigurationBackUpViewModel, ConfigurationBackUp>.InitializeAutomapper();
                var ConfigurationBackUp = mapper.Map<ConfigurationBackUpViewModel, ConfigurationBackUp>(configurationBackUpViewModel);

                #region set dữ liệu cấu hình để gọi SQL Server Agent stored procedures
                var enabled = ConfigurationBackUp.IsScheduleBackup ? 1 : 0;
                var IsenabledJob = ConfigurationBackUp.IsEnabled ? 1 : 0;
                var Occurs = ConfigurationBackUp.ScheduleBackup.Occurs.ToString();
                int freq_interval = 0;
                int freq_recurrence_factor = 0;
                int freq_relative_interval = 0;
                if (ConfigurationBackUp.ScheduleBackup.Occurs == ModelProject.Models.Occurs.Day)
                {
                    freq_interval = ConfigurationBackUp.ScheduleBackup.RecursEveryDay;
                }
                else if (ConfigurationBackUp.ScheduleBackup.Occurs == ModelProject.Models.Occurs.Weekly)
                {
                    freq_interval = ConfigurationBackUp.ScheduleBackup.Weeklies.Sum(x => ((int)x));
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
                SqlParameters.Add(new SqlParameter("@DatabaseName", _context.Database.GetDbConnection().Database));
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
                var temp = SqlParameters.Select(x => x.SqlValue.ToString()).ToList();

                if (temp != null)
                {
                    var SqlParametersjson = System.Text.Json.JsonSerializer.Serialize(temp);
                    Log.Add(string.Format("Dữ liệu gọi store: {0}", SqlParametersjson.ToString()));
                }
                //update or create backup
                try
                {
                    if (ConfigurationBackUp.Id != Guid.Empty) //update
                    {
                        _context.Database
                        .ExecuteSqlRaw("BackupUpdate @DatabaseName , @BackupName , @BackupType, @BackupPath , @enabled, @Occurs_freq_type, @freq_relative_interval_parameters, @Day_Recurs_every , @freq_recurrence_factor1, @start_date , @end_date, @start_time, @IsenabledJob",
                        SqlParameters);
                    }
                    else //create
                    {
                        var SysJobListName = _context.Database.SqlQueryRaw<string>("select name from msdb.dbo.sysjobs").ToList();
                        var SysJobName = SysJobListName.FirstOrDefault(x => x.ToLower() == ConfigurationBackUp.BackupName.ToLower());
                        if (SysJobName != null)
                        {
                            MessageBus.MessageStatus = MessageStatus.Error;
                            MessageBus.Message = "The name Job already exists";
                            return MessageBus;
                        }
                       _context.Database
                      .ExecuteSqlRaw("BackupDemo @DatabaseName , @BackupName , @BackupType, @BackupPath , @enabled, @Occurs_freq_type, @freq_relative_interval_parameters, @Day_Recurs_every , @freq_recurrence_factor1, @start_date , @end_date, @start_time, @IsenabledJob",
                      SqlParameters);
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
               
                SaveSetting(configurationBackUpViewModel);
               
                Log.Add("SaveSetting_End-----------------------" + configurationBackUpViewModel.BackupName + "--------" + DateTime.Now.ToString("ddMMyyyy"));
                MessageBus.MessageStatus = MessageStatus.Success;
                MessageBus.Message = "Success";
                Log.Add("CreateJob_End-------------------------" + configurationBackUpViewModel.BackupName + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"));
                WriteLogFile.WriteLog(LogName, Log, Setting.FoderBackUp);
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
        public MessageBusViewModel DeleteJob(string JobName)
        {
            var MessageBusViewModel = new MessageBusViewModel();
            try
            {
                if (busConfigViewModel.IsJob(JobName))
                {
                    WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")),
                        "DeleteJob_Start--------------" + JobName + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderBackUp);
                    var SqlParameters = new List<SqlParameter>();
                    SqlParameters.Add(new SqlParameter("@job_name", JobName));
                    _context.Database
                     .ExecuteSqlRaw("msdb.dbo.sp_delete_job null, @job_name", SqlParameters);
                    _busScheduleTask.DeleteScheduleTask(JobName);
                }
                var DeleteJsonBackUp = _busConfigurationBackUp.DeleteJsonBackUp(JobName);
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
                  "DeleteJob_End---------------" + JobName + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderBackUp);
            return MessageBusViewModel;
        }


        public MessageBusViewModel StartJobNow(string jobname)
        {
            WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")),
                  "StartJobNow_Start--------------" + jobname + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderBackUp);
            var MessageBusViewModel = new MessageBusViewModel();
            try
            {
                var SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@job_name", jobname));
                _context.Database
                 .ExecuteSqlRaw("msdb.dbo.sp_start_job @job_name", SqlParameters);
                var conFig = _busConfigurationBackUp.LoadJsonBackUp().FirstOrDefault(x => x.BackupName.ToLower() == jobname.ToLower());
                
                if (conFig != null)
                {
                     conFig.FTPSetting.PassWord = Encryption.DecryptV2(conFig.FTPSetting.PassWord);
                    _BusFTP.PushFPT(conFig,null);
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
                 "StartJobNow_End--------------" + jobname + "--------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderBackUp);
            return MessageBusViewModel;
        }
    }
}
