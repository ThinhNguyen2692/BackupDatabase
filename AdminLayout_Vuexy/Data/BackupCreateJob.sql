Create PROCEDURE [dbo].[BackupCreateJob]
@DatabaseName nvarchar(max),
@BackupName nvarchar(max) ,
@BackupType nvarchar(max) ,
@BackupPath nvarchar(max),
@enabled int = 1,
@Occurs_freq_type int = 4,
@freq_relative_interval_parameters int = 1,
@Day_Recurs_every int = 0, 
@freq_recurrence_factor1 int = 0, 
@start_date int = 20231020, --ngày bắt đầu	
@end_date int = 99991231, 
@start_time int = 130000,
@IsenabledJob int = 1,
@freqSubdayType int = 0,
@freqSubdayInterval int = 0,
@end_time int = 0
AS 
begin 
/****** Object:  Job [DatabaseBackup - HRM8_Developing - FULL]    Script Date: 06/18/2021 12:10:35 PM ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
/****** Object:  JobCategory [Database Maintenance]    Script Date: 06/18/2021 12:10:35 PM ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'Database Maintenance' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'Database Maintenance'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=@BackupName, 
		@enabled=@IsenabledJob, 
		@notify_level_eventlog=2, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'Source: https://ola.hallengren.com', 
		@category_name=N'Database Maintenance', 
		@owner_login_name=N'sa', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
DECLARE @EXECstring nvarchar(max) = 'sqlcmd -E -S $(ESCAPE_SQUOTE(SRVR)) -d '+@DatabaseName+' -Q "EXECUTE [dbo].[DatabaseBackup] @Databases ='' '+@DatabaseName+''', @Directory = '''+@BackupPath+''', @BackupType = '''+@BackupType+''', @Verify = ''Y'', @CleanupTime = 120, @CheckSum = ''Y'', @LogToTable = ''Y''" -b'

/****** Object:  Step [DatabaseBackup - HRM8_Developing - FULL]    Script Date: 06/18/2021 12:10:35 PM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=@BackupName, 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'CmdExec', 
		@command= @EXECstring, 
		@output_file_name=N'$(ESCAPE_SQUOTE(SQLLOGDIR))\DatabaseBackup_$(ESCAPE_SQUOTE(JOBID))_$(ESCAPE_SQUOTE(STEPID))_$(ESCAPE_SQUOTE(STRTDT))_$(ESCAPE_SQUOTE(STRTTM)).txt', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
select @jobId
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_name=@BackupName, @name=@BackupName, 
		@enabled=@enabled, 
		@freq_type=@Occurs_freq_type, -- Occurs
		@freq_interval=@freq_relative_interval_parameters, --val()
		@freq_relative_interval=@Day_Recurs_every, --Occurs = 32
		@freq_recurrence_factor=@freq_recurrence_factor1, --Occurs ever
		@active_start_date=@start_date, --ngày bắt đầu
		@active_end_date=@end_date, 
		@active_start_time=@start_time, 
		@active_end_time=@end_time,
		@freq_subday_interval = @freqSubdayInterval,
        @freq_subday_type = @freqSubdayType
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:

end

