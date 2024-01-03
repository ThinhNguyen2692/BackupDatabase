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
using ModelProject.ViewModels;

namespace Bus_backUpData.Services
{
    public class BusStoredProcedureServices : IBusStoredProcedureServices
    {
        public readonly IDalStoredProcedureServices _dalStoredProcedureServices;
        public BusStoredProcedureServices(IDalStoredProcedureServices dalStoredProcedureServices) {
            _dalStoredProcedureServices = dalStoredProcedureServices;
        }

        public async Task<bool> CheckConnectionAsync(ServerConnectionViewModel serverConnectionViewModel)
        {
            var connectionstring = SettingConnection.GetConnection(serverConnectionViewModel.ServerName, serverConnectionViewModel.DatabaseName, serverConnectionViewModel.UserName, serverConnectionViewModel.Password);
            var result = await _dalStoredProcedureServices.CheckConnectionAsync(connectionstring);
            return result;
        }

        public List<string> GetListDatabaseNameServer(ServerConnectionViewModel serverConnectionViewModel)
        {
            var connectionstring = SettingConnection.GetConnection(serverConnectionViewModel.ServerName, serverConnectionViewModel.DatabaseName, serverConnectionViewModel.UserName, serverConnectionViewModel.Password);
            var result = _dalStoredProcedureServices.SqlQueryRaw(connectionstring, StringSql.SQlsysdatabases).ToList();
            return result.ToList();
        }

        public MessageBusViewModel CreateSettingDatabase(ServerConnectionViewModel serverConnectionViewModel)
        {
			string pathLocationSettingSql = Path.GetFullPath("Data\\SettingSql.sql");
			string pathLocationCleanupDatabase = Path.GetFullPath("Data\\cleanup_database.sql");
            var sqlScriptSettingSql = LibrarySettingFileConfig.ReadSqlFile(pathLocationSettingSql);
            var sqlScriptCleanupDatabase = LibrarySettingFileConfig.ReadSqlFile(pathLocationCleanupDatabase);
            sqlScriptSettingSql = sqlScriptSettingSql.Replace("DatabaseNameSetting", serverConnectionViewModel.DatabaseName);
			var connectionString = SettingConnection.GetConnection(serverConnectionViewModel.ServerName, serverConnectionViewModel.DatabaseName, serverConnectionViewModel.UserName, serverConnectionViewModel.Password);
            var Messagecleanup_database = ScriptExecute(sqlScriptCleanupDatabase, connectionString);
            var MessageSettingSql = ScriptExecute(sqlScriptSettingSql, connectionString);
            if(Messagecleanup_database.MessageStatus == MessageStatus.Error || MessageSettingSql.MessageStatus == MessageStatus.Error)
            {
                Messagecleanup_database.MessageStatus = MessageStatus.Error;
                Messagecleanup_database.Message = $"CleanupDatabase: {Messagecleanup_database.Message}. CreateSettingSql {MessageSettingSql.Message}.";
            }
            return Messagecleanup_database;
        }

		public MessageBusViewModel ScriptExecute(string sqlScript, string connectionString)
		{
            var messageBusViewModel = new MessageBusViewModel();
            string[] commands = Regex.Split(sqlScript, @"^\s*GO\s*$", RegexOptions.Multiline);
            try
            {
                // Tạo kết nối đến cơ sở dữ liệu
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                   
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
                messageBusViewModel.MessageStatus = MessageStatus.Success;
                messageBusViewModel.Message = MessageStatus.Success.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                messageBusViewModel.MessageStatus = MessageStatus.Error;
                messageBusViewModel.Message = ex.Message;
            }
            return messageBusViewModel;
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
        