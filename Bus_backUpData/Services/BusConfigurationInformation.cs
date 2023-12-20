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

        public bool DeleteJsonBackUp(string jobname)
        {
            var ConfigurationBackUpDelete = _dalConfigurationBackUp.FirstOrDefaultByJobName(jobname);
            if (ConfigurationBackUpDelete != null)
            {
				ConfigurationBackUpDelete.IsDeleted = true;
				_dalConfigurationBackUp.Update(ConfigurationBackUpDelete);
				return true;
            }
            return false;
        }
       


		/// <summary>
		/// Get chuỗi connect string 
		/// </summary>
		/// <param name="DatabaseName">thông tin get string</param>
		/// <returns>Chuỗi connectString</returns>
		public string GetConnectStringByDatabaseName(string DatabaseName)
        {
			var databaseConnect = _dalDatabaseConnect.FirstOrDefault(DatabaseName);
            if(databaseConnect == null) return string.Empty;
			var connectionstring = SettingConnection.GetConnection(databaseConnect);
            return connectionstring;
		}

		/// <summary>
		/// Get chuỗi connect string 
		/// </summary>
		/// <param name="JobName">thông tin get string</param>
		/// <returns>Chuỗi connectString</returns>
		public string GetConnectStringByJobName(string JobName)
		{
            var configurationBackUp = _dalConfigurationBackUp.FirstOrDefaultByJobName(JobName);
           return GetConnectStringByDatabaseName(configurationBackUp.DatabaseConnect.DatabaseName);
		}

		/// <summary>
		/// Get server name connect 
		/// </summary>
		/// <param name="DatabaseName">thông tin lấy servername</param>
		/// <returns> connectString</returns>
		public string GetServerNameByDatabaseName(string DatabaseName)
		{
			var Connectstring = GetConnectStringByDatabaseName(DatabaseName);
			var servername =  GetServerName(Connectstring);
			return servername ?? string.Empty;
		}

		/// <summary>
		/// Get server name connect 
		/// </summary>
		/// <param name="JobName">thông tin lấy servername</param>
		/// <returns> connectString</returns>
		public string GetServerNameByJobName(string JobName)
		{
			var Connectstring = GetConnectStringByJobName(JobName);
            var servername =  GetServerName(Connectstring);
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

		public bool IsJob(string JobName)
		{
            var connectString = GetConnectStringByJobName(JobName);
			var SysJobListName = _dalStoredProcedureServices.SqlQueryRaw(connectString, StringSql.SQlsysjobs).ToList();
			var SysJobName = SysJobListName.FirstOrDefault(x => x.ToLower() == JobName.ToLower());
			if (SysJobName == null) { return false; }
			return true;
		}
	}
}
