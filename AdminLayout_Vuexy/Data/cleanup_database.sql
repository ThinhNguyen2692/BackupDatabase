-- Kiểm tra và xóa bảng nếu tồn tại
IF OBJECT_ID('CommandLog', 'U') IS NOT NULL
    DROP TABLE CommandLog;

-- Kiểm tra và xóa các Stored Procedure nếu tồn tại
IF OBJECT_ID('BackupCreateJob', 'P') IS NOT NULL
    DROP PROCEDURE BackupCreateJob;

IF OBJECT_ID('BackupUpdate', 'P') IS NOT NULL
    DROP PROCEDURE BackupUpdate;

IF OBJECT_ID('CommandExecute', 'P') IS NOT NULL
    DROP PROCEDURE CommandExecute;

IF OBJECT_ID('DatabaseBackup', 'P') IS NOT NULL
    DROP PROCEDURE DatabaseBackup;

IF OBJECT_ID('DatabaseIntegrityCheck', 'P') IS NOT NULL
    DROP PROCEDURE DatabaseIntegrityCheck;

IF OBJECT_ID('IndexOptimize', 'P') IS NOT NULL
    DROP PROCEDURE IndexOptimize;