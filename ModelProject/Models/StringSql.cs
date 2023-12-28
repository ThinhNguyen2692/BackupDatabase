using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models
{
    public static class StringSql
    {
        public const string SQlBackupUpdate = "BackupUpdate @DatabaseName , @BackupName , @BackupType, @BackupPath , @enabled, @Occurs_freq_type, @freq_relative_interval_parameters, @Day_Recurs_every , @freq_recurrence_factor1, @start_date , @end_date, @start_time, @IsenabledJob, @freqSubdayType,@freqSubdayInterval, @end_time";
        public const string SQlBackup = "BackupCreateJob @DatabaseName , @BackupName , @BackupType, @BackupPath , @enabled, @Occurs_freq_type, @freq_relative_interval_parameters, @Day_Recurs_every , @freq_recurrence_factor1, @start_date , @end_date, @start_time, @IsenabledJob,@freqSubdayType ,@freqSubdayInterval,@end_time";
        public const string SQlsp_delete_job = "msdb.dbo.sp_delete_job null, @job_name";
        public const string SQlsp_start_job = "msdb.dbo.sp_start_job @job_name";
        public const string SQlsysjobs = "select name from msdb.dbo.sysjobs";
        public const string SQlServerName = "SELECT @@servername";
        public const string SQlsysdatabases = "SELECT name FROM master.dbo.sysdatabases";
        //Sử dụng database master chạy các lệnh
        public const string SQlRestoreBackup = "RESTORE DATABASE @DatabaseName FROM DISK = @BackupFilePath WITH REPLACE, NORECOVERY";
        public const string SQlRestoreBackupRecovery = "RESTORE DATABASE @DatabaseName WITH RECOVERY";
		public const string SQlSINGLE_USER = "ALTER DATABASE [{0}] SET Single_User WITH Rollback Immediate";
		public const string SQlMULTI_USER = "ALTER DATABASE [{0}] SET Multi_User";
		public const string SQlSelectDatabasesStateDesc = "SELECT state_desc FROM sys.databases WHERE database_id = DB_ID(@DatabaseName)";
		//Sử dụng database master chạy các lệnh
	}
}
