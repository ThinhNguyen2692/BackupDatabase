using ModelProject.Func;
using Bus_backUpData.Interface;
using ModelProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalBackup.Interface;
using ModelProject.ViewModels;
using DalStoredProcedure.Interface;
using Microsoft.Data.SqlClient;
using ModelProject.ViewModels.ModelRequest;
using DalBackup.Services;

namespace Bus_backUpData.Services
{
    public  class BusConfigurationInformation : IBusConfigurationInformation
    {
		private readonly IDalDatabaseConnect _dalDatabaseConnect;
		private readonly IDalConfigurationBackUp _dalConfigurationBackUp;
		private readonly IDalStoredProcedureServices _dalStoredProcedureServices;
		public BusConfigurationInformation(IDalDatabaseConnect dalDatabaseConnect, IDalConfigurationBackUp dalConfigurationBackUp, IDalStoredProcedureServices dalStoredProcedureServices) {
			_dalDatabaseConnect = dalDatabaseConnect;
			_dalConfigurationBackUp = dalConfigurationBackUp;
            _dalStoredProcedureServices = dalStoredProcedureServices;
		}

        /// <summary>
        /// Cập nhật isDelete ConfigurationBackUp get theo jobname và dataBaseName
        /// </summary>
        /// <param name="jobname"></param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        public bool DeleteBackUp(string ServerName, string DatabaseName, string JobName)
        {
            var ConfigurationBackUpDelete = _dalConfigurationBackUp.FirstOrDefault(JobName, DatabaseName, ServerName);
            var result = DeleteBackUpBy(ConfigurationBackUpDelete);
            return false;
        }

        /// <summary>
        /// Cập nhật isDelete ConfigurationBackUp
        /// </summary>
        /// <param name="id">id config</param>
        /// <returns></returns>
        public bool DeleteBackUpByID(Guid id)
        {
            var ConfigurationBackUpDelete = _dalConfigurationBackUp.FirstOrDefault(id);
            var result = DeleteBackUpBy(ConfigurationBackUpDelete);
            return result;
        }

        public bool DeleteBackUpBy(ConfigurationBackUp? configurationBackUp)
        {
            if (configurationBackUp != null)
            {
                configurationBackUp.IsDeleted = true;
                _dalConfigurationBackUp.Update(configurationBackUp);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get chuỗi connect string 
        /// </summary>
        /// <param name="DatabaseName">thông tin get string</param>
        /// <returns>Chuỗi connectString</returns>
        public string GetConnectStringByDatabase(string ServerName, string DatabaseName)
        {
			var databaseConnect = _dalDatabaseConnect.FirstOrDefault(ServerName,DatabaseName);
			var connectionstring = GetConnectString(databaseConnect);
            return connectionstring;
		}
        public string GetConnectStringByDatabase(Guid Id)
        {
            var databaseConnect = _dalDatabaseConnect.FirstOrDefault(Id);
            var connectionstring = GetConnectString(databaseConnect);
            return connectionstring;
        }
       
        public string GetConnectString(DatabaseConnect? databaseConnect)
        {
            if (databaseConnect == null) return string.Empty;
            var connectionstring = SettingConnection.GetConnection(databaseConnect);
            return connectionstring;
        }
        /// <summary>
        /// Get chuỗi connect string 
        /// </summary>
        /// <param name="JobName">thông tin get string</param>
        /// <returns>Chuỗi connectString</returns>
        public string GetConnectStringByJob(string ServerName, string DatbaseName,string JobName)
		{
            var configurationBackUp = _dalConfigurationBackUp.FirstOrDefault(JobName, DatbaseName, ServerName);
            return GetConnectStringByJob(configurationBackUp);
        }
        public string GetConnectStringByJob(Guid Id)
        {
            var configurationBackUp = _dalConfigurationBackUp.FirstOrDefault(Id);
            return GetConnectStringByJob(configurationBackUp);
        }

        public string GetConnectStringByJob(ConfigurationBackUp? configurationBackUp)
        {
            if (configurationBackUp == null || 
                configurationBackUp.DatabaseConnect == null || 
                configurationBackUp.DatabaseConnect.ServerConnects == null) return string.Empty;
            return GetConnectString(configurationBackUp.DatabaseConnect);
        }

        /// <summary>
        /// Get server name connect 
        /// </summary>
        /// <param name="DatabaseName">thông tin lấy servername</param>
        /// <returns> connectString</returns>
        public string GetServerNameByDatabase(string ServerName, string DatabaseName)
		{
			
            var databaseConnect = _dalDatabaseConnect.FirstOrDefault(ServerName, DatabaseName);
            var servername = GetServerNameByDatabase(databaseConnect);
            return servername ?? string.Empty;
        }
        public string GetServerNameByDatabase(Guid Id)
        {
            var databaseConnect = _dalDatabaseConnect.FirstOrDefault(Id);
            var servername = GetServerNameByDatabase(databaseConnect);
            return servername ?? string.Empty;
        }
        public string GetServerNameByDatabase(DatabaseConnect databaseConnect)
        {
            if (databaseConnect == null) return string.Empty;
            var Connectstring = GetConnectString(databaseConnect);
            Connectstring = Connectstring.Replace(databaseConnect.DatabaseName, Setting.UsingMaster);
            var servername = GetServerName(Connectstring);
            return servername ?? string.Empty;
        }
        /// <summary>
        /// Get server name connect 
        /// </summary>
        /// <param name="JobName">thông tin lấy servername</param>
        /// <returns> connectString</returns>
        public string GetServerNameByJob(string ServerName, string DatabaseName, string JobName)
		{
            var configurationBackUp = _dalConfigurationBackUp.FirstOrDefault(JobName, DatabaseName, ServerName);
            if (configurationBackUp == null) return string.Empty;
            var ConnectString = GetConnectString(configurationBackUp.DatabaseConnect);
            var servername = GetServerName(ConnectString);

            return servername ?? string.Empty;
		}

        public string GetServerNameByJob(Guid Id)
        {
            var configurationBackUp = _dalConfigurationBackUp.FirstOrDefault(Id);
            if (configurationBackUp == null) return string.Empty;
            var ConnectString = GetConnectString(configurationBackUp.DatabaseConnect);
            var servername = GetServerName(ConnectString);
            return servername ?? string.Empty;
        }

        /// <summary>
        /// thực thi lấy thông t
        /// </summary>
        /// <param name="Connectstring"></param>
        /// <returns></returns>
        public string GetServerName(string Connectstring)
		{
			var servername = _dalStoredProcedureServices.SqlQueryRaw(Connectstring, StringSql.SQlServerName);
			var servernameString = servername.FirstOrDefault() ?? string.Empty;
            return servernameString;
		}

		public bool IsJob(string ServerName, string DatabaseName, string JobName)
		{
            var configurationBackUp = _dalConfigurationBackUp.FirstOrDefault(JobName, DatabaseName, ServerName);
            if (configurationBackUp == null) return false;
            return IsJob(configurationBackUp.Id);
		}

        public bool IsJob(Guid Id)
        {
            var connectString = GetConnectStringByJob(Id);
            var ConfigurationBackUpDelete = _dalConfigurationBackUp.FirstOrDefault(Id);
			if (ConfigurationBackUpDelete == null) { return false; }
            var SysJobListName = _dalStoredProcedureServices.SqlQueryRaw(connectString, StringSql.SQlsysjobs).ToList();
            var SysJobName = SysJobListName.FirstOrDefault(x => x.ToLower() == ConfigurationBackUpDelete.BackupName.ToLower());
            if (SysJobName == null) { return false; }
            return true;
        }

        /// <summary>
        /// kiểm trả trạng thái restore Recovery;
        /// </summary>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>
        public bool IsRecovery(string ServerName, string DatabaseName)
		{
			var DatabasesStateDesc = StateDescDB(ServerName,DatabaseName);
			var IsRecovery = true;
			if (string.IsNullOrEmpty(DatabasesStateDesc) || DatabasesStateDesc != "ONLINE")
			{
				IsRecovery = false;
			}
			return IsRecovery;
		}
		public string? StateDescDB(string ServerName, string DatabaseName)
		{
			var SqlParameters = new List<SqlParameter>();
			SqlParameters.Add(new SqlParameter("@DatabaseName", DatabaseName));
			var Connectstring = GetConnectStringByDatabase(ServerName,DatabaseName);
			Connectstring = Connectstring.Replace(DatabaseName, Setting.UsingMaster);
			var DatabasesStateDescs = _dalStoredProcedureServices.SqlQueryRaw(Connectstring, StringSql.SQlSelectDatabasesStateDesc, SqlParameters);
			var DatabasesStateDesc = DatabasesStateDescs.FirstOrDefault();		
			return DatabasesStateDesc;
		}
        public JobViewModel GetFullJobModel(JobViewModel jobModel)
        {
            var Config = new ConfigurationBackUp();
            if ((string.IsNullOrEmpty(jobModel.DatabaseName) ||
                string.IsNullOrEmpty(jobModel.JobName) ||
                string.IsNullOrEmpty(jobModel.ServerName)) &&
                jobModel.Id != Guid.Empty)
            {
                Config = _dalConfigurationBackUp.FirstOrDefault(jobModel.Id);
                if (Config == null) { return jobModel; }
                jobModel.DatabaseName = Config.DatabaseConnect.DatabaseName;
                jobModel.JobName = Config.BackupName;
                jobModel.ServerName = Config.DatabaseConnect.ServerConnects.ServerName;
            }
            else if ((!string.IsNullOrEmpty(jobModel.DatabaseName) &&
                !string.IsNullOrEmpty(jobModel.JobName) &&
                !string.IsNullOrEmpty(jobModel.ServerName)) &&
                (jobModel.Id == Guid.Empty ||
                jobModel.Id == null))
            {
                Config = _dalConfigurationBackUp.FirstOrDefault(jobModel.JobName, jobModel.DatabaseName, jobModel.ServerName);
                if (Config == null) { return jobModel; }
                jobModel.Id = Config.Id;
				
			}
            return jobModel;
        }
        public string GetBackUpSettingPath(string ServerName, string DatabaseName)
        {
            var dataConfigurationBackUps = _dalConfigurationBackUp.GetData(ServerName, DatabaseName);
            var dataConfigurationBackUp = dataConfigurationBackUps.FirstOrDefault();
            if (dataConfigurationBackUp == null)
            {
                return string.Empty;
            }
            return dataConfigurationBackUp.BackUpSetting.Path;
        }
    }
}
