using Bus_backUpData.Interface;
using DalStoredProcedure.Interface;
using ModelProject.Func;
using ModelProject.Models;
using ModelProject.ViewModels.ViewModelSeverConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Bus_backUpData.Services
{
    public class BusStoredProcedureServices : IBusStoredProcedureServices
    {
        public readonly IDalStoredProcedureServices _dalStoredProcedureServices;
        public BusStoredProcedureServices(IDalStoredProcedureServices dalStoredProcedureServices) {
            _dalStoredProcedureServices = dalStoredProcedureServices;
        }

        public bool CheckConnection(ServerConnectionViewModel serverConnectionViewModel)
        {
            var connectionstring = SettingConnection.GetConnection(serverConnectionViewModel.ServerName, serverConnectionViewModel.DatabaseName, serverConnectionViewModel.UserName, serverConnectionViewModel.Password);
            var result = _dalStoredProcedureServices.CheckConnection(connectionstring);
            return result;
        }

        public List<string> GetListDatabaseNameServer(ServerConnectionViewModel serverConnectionViewModel)
        {
            var connectionstring = SettingConnection.GetConnection(serverConnectionViewModel.ServerName, serverConnectionViewModel.DatabaseName, serverConnectionViewModel.UserName, serverConnectionViewModel.Password);
            var result = _dalStoredProcedureServices.SqlQueryRaw(connectionstring, StringSql.SQlsysdatabases).ToList();
            return result.ToList();
        }

        public void CreateSettingDatabase(ServerConnectionViewModel serverConnectionViewModel)
        {
			string pathLocation = Path.GetFullPath("Data\\SettingSql.sql");
            var sqlScript = LibrarySettingFileConfig.ReadSqlFile(pathLocation);
			sqlScript = sqlScript.Replace("DatabaseNameSetting", serverConnectionViewModel.DatabaseName);
			var connectionString = SettingConnection.GetConnection(serverConnectionViewModel.ServerName, serverConnectionViewModel.DatabaseName, serverConnectionViewModel.UserName, serverConnectionViewModel.Password);
			string[] commands = Regex.Split(sqlScript, @"^\s*GO\s*$", RegexOptions.Multiline);
			try
			{
				// Tạo kết nối đến cơ sở dữ liệu
				using (IDbConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					if (!StoredProcedureExists(connection, "DatabaseBackup"))
					{
						// Tạo đối tượng Command để thực hiện lệnh SQL
						using (IDbCommand command = connection.CreateCommand())
						{

							foreach (var cmdText in commands)
							{
								if (!string.IsNullOrWhiteSpace(cmdText))
								{
									command.CommandText = cmdText;
									command.ExecuteNonQuery();
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}
		public bool VerifyBackupIntegrity(IDbConnection connection, string databaseName, string Pathbak)
		{
			// Kiểm tra tính toàn vẹn của tệp sao lưu
			string sql = $@"
            USE master;
            RESTORE VERIFYONLY
            FROM DISK = N'{Pathbak}';";

			try
			{
				connection.Execute(sql);
				Console.WriteLine($"Backup integrity verified for {databaseName}.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error verifying backup integrity for {databaseName}: {ex.Message}");
                return false;
			}
            return true;
		}

		public void CreateBackupStoredProcedure(IDbConnection connection, string databaseName, string Pathbak)
		{
			// Tạo stored procedure backup cho cơ sở dữ liệu
			string sql = $@"BACKUP DATABASE [{databaseName}] TO DISK = '{Pathbak}' WITH FORMAT";
			connection.Execute(sql);
		}


		public bool StoredProcedureExists(IDbConnection connection, string procedureName)
		{
			using (IDbCommand command = connection.CreateCommand())
			{
				command.CommandText = "SELECT COUNT(*) FROM sys.procedures WHERE name = @ProcedureName";
				command.Parameters.Add(new SqlParameter("@ProcedureName", SqlDbType.NVarChar, 128) { Value = procedureName });

				int count = Convert.ToInt32(command.ExecuteScalar());
				return count > 0;
			}
		}

	}
}
        