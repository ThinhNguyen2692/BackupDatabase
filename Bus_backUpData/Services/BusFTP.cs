using Bus_backUpData.Data;
using ModelProject.Func;
using Bus_backUpData.Interface;
using ModelProject.Models;
using ModelProject.ViewModels;
using HRM.SC.Core.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Bus_backUpData.Services.AutoModelMapper;
using DalBackup.Interface;

namespace Bus_backUpData.Services
{
    public class BusFTP : IBusFTP
    {
        private readonly IBusConfigViewModel _BusConfig;
        private readonly IBusConfigurationInformation _busConfigurationInformation;
        private readonly IDalConfigurationBackUp _dalConfigurationBackUp;
        private readonly IBusHistoryFTP _busHistoryFTP;

        public BusFTP(IBusConfigViewModel busConfig, IBusConfigurationInformation busConfigurationBackUp, IBusHistoryFTP busHistoryFTP, IDalConfigurationBackUp dalConfigurationBackUp )
        {

            _BusConfig = busConfig;
            _busConfigurationInformation = busConfigurationBackUp;
            _busHistoryFTP = busHistoryFTP;
            _dalConfigurationBackUp = dalConfigurationBackUp;
        }
        /// <summary>
        /// Đẩy File sang FTP
        /// </summary>
        /// <param name="fTPSettingViewModel"></param>
        /// <param name="fTPSetting"></param>
        /// <returns></returns>
        public bool PushFPT(ConfigurationBackUp ConfigurationBackUp, ConfigurationBackUpViewModel ConfigurationBackUpViewModel)
        {
            var LogName = string.Format("{0}{1}", "LogSchedule", DateTime.Now.ToString("ddMMyyyy"));
            if (ConfigurationBackUpViewModel != null)
            {
                var mapper = MapperConfig<ConfigurationBackUpViewModel, ConfigurationBackUp>.InitializeAutomapper();
                ConfigurationBackUp = mapper.Map<ConfigurationBackUpViewModel, ConfigurationBackUp>(ConfigurationBackUpViewModel);
            }
            WriteLogFile.WriteLog(LogName, "JobTask_PushFPT--------Run-------" + ConfigurationBackUp.BackupName + "------------------------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderTask);

            var fTPSettingJoson = System.Text.Json.JsonSerializer.Serialize(ConfigurationBackUp.FTPSetting);
            WriteLogFile.WriteLog(LogName, string.Format("JobTask_PushFPT__DataRun__Jobname: {0}. Data: {1}", ConfigurationBackUp.BackupName, fTPSettingJoson), Setting.FoderTask);

            if (string.IsNullOrEmpty(ConfigurationBackUp.FTPSetting.UserName) || string.IsNullOrEmpty(ConfigurationBackUp.FTPSetting.PassWord) || string.IsNullOrEmpty(ConfigurationBackUp.FTPSetting.Path) || string.IsNullOrEmpty(ConfigurationBackUp.FTPSetting.HostName)) return true;
            try
            {
                var ServerName = _busConfigurationInformation.GetServerNameByJobName(ConfigurationBackUp.BackupName);

                if (string.IsNullOrEmpty(ServerName)) return false;
                var ServerNameStr = ServerName;
                ServerNameStr = ServerNameStr.Replace("\\", "$");
                string pathLocation = Setting.PathbackUp + $"\\{ServerNameStr}\\{Setting.DatabaseName}";
                var directory = new DirectoryInfo(pathLocation);
                string fileNamePush = $"{Setting.DatabaseName}_{ConfigurationBackUp.BackUpSetting.BackUpType}";
                var myFiledirectory = directory.GetFiles().Where(x => x.Name.ToString().ToLower().Contains(fileNamePush.ToLower())).ToList();

                var myFile = myFiledirectory.OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
                var FileName = ConfigurationBackUp.FTPSetting.Path + "\\" + myFile.Name;
                WriteLogFile.WriteLog(LogName, string.Format("JobTask_PushFPT___DataPush__JobName: {0}. FileName: {1}. FullName: {2}. UserName: {3}. Pass: {4}", ConfigurationBackUp.BackupName, FileName, myFile.FullName, ConfigurationBackUp.FTPSetting.UserName, ConfigurationBackUp.FTPSetting.PassWord), Setting.FoderTask);
                UploadFile(ConfigurationBackUp.FTPSetting.UserName, ConfigurationBackUp.FTPSetting.PassWord, FileName, myFile.FullName);
                var FileNameHistory = ConfigurationBackUp.FTPSetting.Path + "/" + myFile.Name;
                SaveFileFTP(ConfigurationBackUp.BackupName, FileNameHistory);
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(LogName, string.Format("JobTask_PushFPT___{0}__Error: {1}", ConfigurationBackUp.BackupName, ex.Message), Setting.FoderTask);

                return false;
            }
            return true;
        }
        public bool DeleteFTP(string JobName)
        {

            var LogName = string.Format("{0}{1}", "LogSchedule", DateTime.Now.ToString("ddMMyyyy"));
            WriteLogFile.WriteLog(LogName, "JobTask_DeleteFTP_Start---------------" + JobName + "------------------------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderTask);
            var settingConfogbackup = _dalConfigurationBackUp.GetData().FirstOrDefault(x => x.BackupName == JobName);
            var settingConfogbackupJson = System.Text.Json.JsonSerializer.Serialize(settingConfogbackup);
            WriteLogFile.WriteLog(LogName, string.Format("JobTask_DeleteFTP__DataRun__Jobname: {0}. Data: {1}", JobName, settingConfogbackupJson), Setting.FoderTask);

            if (settingConfogbackup != null)
            {
                if (string.IsNullOrEmpty(settingConfogbackup.FTPSetting.UserName)
                    || string.IsNullOrEmpty(settingConfogbackup.FTPSetting.PassWord)
                    || string.IsNullOrEmpty(settingConfogbackup.FTPSetting.Path)
                    || string.IsNullOrEmpty(settingConfogbackup.FTPSetting.HostName)) return true;

                var ListFileFTP = _busHistoryFTP.LoadFileFTP();
                var ListFileNameFTP = ListFileFTP.Where(x => x.JobName.Contains(JobName)).ToList();
                var ListFileNameFTPJson = System.Text.Json.JsonSerializer.Serialize(ListFileNameFTP);
                WriteLogFile.WriteLog(LogName, string.Format("JobTask_DeleteFTP___DataDelete__JobName: {0}. UserName: {1}. PassWord: {2}.ListFileNameFTP: {3} ",
                    JobName
                    , settingConfogbackup.FTPSetting.UserName
                    , settingConfogbackup.FTPSetting.PassWord
                    , ListFileNameFTPJson), Setting.FoderTask);
                if (ListFileNameFTP.Count == 0) { return true; }

                var listJobName = DeleteFiles(settingConfogbackup.FTPSetting.UserName, settingConfogbackup.FTPSetting.PassWord, ListFileNameFTP);

                _busHistoryFTP.DeleteFTPRange(listJobName);
                if (listJobName.Count != ListFileNameFTP.Count)
                {
                    var listJobNameJson = System.Text.Json.JsonSerializer.Serialize(listJobName);
                    WriteLogFile.WriteLog(LogName, string.Format("JobTask_DeleteFTP_Data_File_Name_Delete_fail: {0}", listJobNameJson.ToString()), Setting.FoderTask);
                    WriteLogFile.WriteLog(LogName, "JobTask_DeleteFTP_End-----------------" + JobName + "------------------------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderTask);
                    return false;
                }

            }
            WriteLogFile.WriteLog(LogName, "JobTask_DeleteFTP_End-----------------" + JobName + "------------------------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderTask);
            return true;
        }
        private void UploadFile(string UserName, string Pass, string host, string From)
        {
            try
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogSchedule", DateTime.Now.ToString("ddMMyyyy")), "JobTask_PushFPT_UploadFile: start", Setting.FoderTask);
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(UserName, Pass);
                    client.UploadFile(host, WebRequestMethods.Ftp.UploadFile, From);
                }
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogSchedule", DateTime.Now.ToString("ddMMyyyy")), "JobTask_PushFPT_UploadFile: End", Setting.FoderTask);
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogSchedule", DateTime.Now.ToString("ddMMyyyy")), "JobTask_PushFPT_UploadFile_Error: " + ex.Message, Setting.FoderTask);
                throw;
            }
        }
        private List<HistoryFTP> DeleteFiles(string UserName, string Pass, List<HistoryFTP> fileNames)
        {
            var fileNameDelete = new List<HistoryFTP>();
            try
            {
                foreach (var item in fileNames)
                {
                    var tam = DeleteFile(UserName, Pass, item);
                    fileNameDelete.Add(item);
                }
                return fileNameDelete;
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogSchedule", DateTime.Now.ToString("ddMMyyyy")), "JobTask_DeleteFTP_DeleteFiles_Error: " + ex.Message, Setting.FoderTask);
                return fileNameDelete;
            }

        }
        private string DeleteFile(string UserName, string Pass, HistoryFTP fileName)
        {
            try
            {
                var PassWord = Encryption.DecryptV2(Pass);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(fileName.FullFilePathName);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(UserName, PassWord);
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return response.StatusDescription;
                }
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogSchedule", DateTime.Now.ToString("ddMMyyyy")), "JobTask_DeleteFTP_DeleteFile_Error: " + ex.Message, Setting.FoderTask);
                throw;
            }

        }

        public bool SaveFileFTP(string JobName, string FileName)
        {
            try
            {
                var FileFTP = new HistoryFTP() { JobName = JobName, FullFilePathName = FileName };
                _busHistoryFTP.AddHistoryFTP(FileFTP);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<HistoryFTP> GetHistoryFTPS(string JobName)
        {
            var historFTP = _busHistoryFTP.LoadFileFTP();
            historFTP = historFTP.Where(x => x.JobName == JobName).ToList();
            return historFTP;
        }

        public async void JobTaskPushFTp(string JobName)
        {
            var LogName = string.Format("{0}{1}", "LogSchedule", DateTime.Now.ToString("ddMMyyyy"));
            var configurationBackUp = _BusConfig.GetConfigurationBackUpViewModelByJobName(JobName);
            if (configurationBackUp != null)
            {
                var LogNew = configurationBackUp.JobHistoryViewModels.OrderByDescending(x => x.run_date).OrderByDescending(x => x.run_time).FirstOrDefault();
                if (LogNew != null)
                {
                    var instance_id_Job = LogNew.instance_id;
                    var checkLog = false;

                    while(checkLog == false)
                    {
                        if (LogNew.run_status == RunStatus.InProgress)
                        {
                            await Task.Delay(180000);
                            var listLogJob = _BusConfig.GetJobHistoryViewModels(JobName);
                            LogNew = listLogJob.FirstOrDefault(x => x.instance_id == instance_id_Job);
                        }
                        else
                        {
                            checkLog = true;
                        }
                    }
                    var LogNewMess = LogNew == null ? "LogNewBackUp = NUll" : "LogNewBackUp instance_id = " + LogNew.instance_id;

                    WriteLogFile.WriteLog(LogName, LogNewMess, Setting.FoderTask);


                    string tilte = string.Empty;
                    string EmailTo = string.Empty;
                    if (LogNew.run_status == RunStatus.Succeeded)
                    {
                        PushFPT(null, configurationBackUp);
                        tilte = $"{JobName} Backup Database Success";
                        EmailTo = configurationBackUp.EmailConfirmation.EmailSuccess;
                    }
                    else { tilte = $"{JobName} Backup Database Error"; EmailTo = configurationBackUp.EmailConfirmation.EmailFailure; }

                    string body = $"<h1> {tilte}</h1><p>{LogNew.message}</p>";
                    if (configurationBackUp.IsEmailConfirmation)
                    {
                        LibraryEmail.SendMail(SettingEmail.SubjectEmailNoti, body, EmailTo, SettingEmail.Email, SettingEmail.PassEmail);
                    }
                }
            }
        }

    }
}
