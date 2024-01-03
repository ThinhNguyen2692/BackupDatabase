using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DalBackup.Migrations
{
    /// <inheritdoc />
    public partial class add_Quartz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0c015039-4e3c-481b-b002-cc4cd631fcc8");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "678d547d-e0ad-46db-9264-68aff65b4c4b", 0, "f8c01591-1113-47ef-869e-6cbb534b1261", "thinh.nguyenngoc@vnresource.org", false, true, null, "THINH.NGUYENNGOC@VNRESOURCE.ORG", "ADMIN", "AQAAAAIAAYagAAAAELCVNBGg1fLkRrjVxjJdur5nXX7m03/AdcCx5DoaHLgaaLp1AGZZFr8XLRzRSWb5wQ==", null, false, "3645d2de-314e-4bf2-aae7-7256ea46cd7b", false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("3c2e8836-3745-451b-826d-da952513e0a6"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(8001), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(8002), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("3fe18c8d-cfdc-419a-a848-e07d8b383386"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7987), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7987), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("44ae543b-1c51-4e79-81f0-e1d99e274162"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7998), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7999), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("8c6d144e-2fad-4cc2-a1ad-fe7661110f31"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7990), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7990), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("a1907086-a621-4a7f-9784-fe7a65a9b01a"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7979), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7980), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("a35339f6-ab5e-4197-825a-424a3888d7fb"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7983), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7984), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("c8b1413f-2f1d-41cf-96eb-aa7eaf9714e4"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7949), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7975), new TimeSpan(0, 7, 0, 0, 0)) });

			migrationBuilder.Sql("DROP TABLE IF EXISTS QRTZ_FIRED_TRIGGERS;\r\nDROP TABLE IF EXISTS QRTZ_PAUSED_TRIGGER_GRPS;\r\nDROP TABLE IF EXISTS QRTZ_SCHEDULER_STATE;\r\nDROP TABLE IF EXISTS QRTZ_LOCKS;\r\nDROP TABLE IF EXISTS QRTZ_SIMPROP_TRIGGERS;\r\nDROP TABLE IF EXISTS QRTZ_SIMPLE_TRIGGERS;\r\nDROP TABLE IF EXISTS QRTZ_CRON_TRIGGERS;\r\nDROP TABLE IF EXISTS QRTZ_BLOB_TRIGGERS;\r\nDROP TABLE IF EXISTS QRTZ_TRIGGERS;\r\nDROP TABLE IF EXISTS QRTZ_JOB_DETAILS;\r\nDROP TABLE IF EXISTS QRTZ_CALENDARS;");
			migrationBuilder.Sql("CREATE TABLE QRTZ_JOB_DETAILS(\r\n    SCHED_NAME NVARCHAR(120) NOT NULL,\r\n\tJOB_NAME NVARCHAR(150) NOT NULL,\r\n    JOB_GROUP NVARCHAR(150) NOT NULL,\r\n    DESCRIPTION NVARCHAR(250) NULL,\r\n    JOB_CLASS_NAME   NVARCHAR(250) NOT NULL,\r\n    IS_DURABLE BIT NOT NULL,\r\n    IS_NONCONCURRENT BIT NOT NULL,\r\n    IS_UPDATE_DATA BIT  NOT NULL,\r\n\tREQUESTS_RECOVERY BIT NOT NULL,\r\n    JOB_DATA BLOB NULL,\r\n    PRIMARY KEY (SCHED_NAME,JOB_NAME,JOB_GROUP)\r\n);");
			migrationBuilder.Sql("CREATE TABLE QRTZ_TRIGGERS\r\n  (\r\n    SCHED_NAME NVARCHAR(120) NOT NULL,\r\n\tTRIGGER_NAME NVARCHAR(150) NOT NULL,\r\n    TRIGGER_GROUP NVARCHAR(150) NOT NULL,\r\n    JOB_NAME NVARCHAR(150) NOT NULL,\r\n    JOB_GROUP NVARCHAR(150) NOT NULL,\r\n    DESCRIPTION NVARCHAR(250) NULL,\r\n    NEXT_FIRE_TIME BIGINT NULL,\r\n    PREV_FIRE_TIME BIGINT NULL,\r\n    PRIORITY INTEGER NULL,\r\n    TRIGGER_STATE NVARCHAR(16) NOT NULL,\r\n    TRIGGER_TYPE NVARCHAR(8) NOT NULL,\r\n    START_TIME BIGINT NOT NULL,\r\n    END_TIME BIGINT NULL,\r\n    CALENDAR_NAME NVARCHAR(200) NULL,\r\n    MISFIRE_INSTR INTEGER NULL,\r\n    JOB_DATA BLOB NULL,\r\n    PRIMARY KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP),\r\n    FOREIGN KEY (SCHED_NAME,JOB_NAME,JOB_GROUP)\r\n        REFERENCES QRTZ_JOB_DETAILS(SCHED_NAME,JOB_NAME,JOB_GROUP)\r\n);");
			migrationBuilder.Sql("CREATE TABLE QRTZ_SIMPLE_TRIGGERS\r\n  (\r\n    SCHED_NAME NVARCHAR(120) NOT NULL,\r\n\tTRIGGER_NAME NVARCHAR(150) NOT NULL,\r\n    TRIGGER_GROUP NVARCHAR(150) NOT NULL,\r\n    REPEAT_COUNT BIGINT NOT NULL,\r\n    REPEAT_INTERVAL BIGINT NOT NULL,\r\n    TIMES_TRIGGERED BIGINT NOT NULL,\r\n    PRIMARY KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP),\r\n    FOREIGN KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP)\r\n        REFERENCES QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP) ON DELETE CASCADE\r\n);");
			migrationBuilder.Sql("CREATE TRIGGER DELETE_SIMPLE_TRIGGER DELETE ON QRTZ_TRIGGERS\r\nBEGIN\r\n\tDELETE FROM QRTZ_SIMPLE_TRIGGERS WHERE SCHED_NAME=OLD.SCHED_NAME AND TRIGGER_NAME=OLD.TRIGGER_NAME AND TRIGGER_GROUP=OLD.TRIGGER_GROUP;\r\nEND\r\n;\r\n");
			migrationBuilder.Sql("CREATE TABLE QRTZ_SIMPROP_TRIGGERS \r\n  (\r\n    SCHED_NAME NVARCHAR (120) NOT NULL ,\r\n    TRIGGER_NAME NVARCHAR (150) NOT NULL ,\r\n    TRIGGER_GROUP NVARCHAR (150) NOT NULL ,\r\n    STR_PROP_1 NVARCHAR (512) NULL,\r\n    STR_PROP_2 NVARCHAR (512) NULL,\r\n    STR_PROP_3 NVARCHAR (512) NULL,\r\n    INT_PROP_1 INT NULL,\r\n    INT_PROP_2 INT NULL,\r\n    LONG_PROP_1 BIGINT NULL,\r\n    LONG_PROP_2 BIGINT NULL,\r\n    DEC_PROP_1 NUMERIC NULL,\r\n    DEC_PROP_2 NUMERIC NULL,\r\n    BOOL_PROP_1 BIT NULL,\r\n    BOOL_PROP_2 BIT NULL,\r\n    TIME_ZONE_ID NVARCHAR(80) NULL,\r\n\tPRIMARY KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP),\r\n\tFOREIGN KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP)\r\n        REFERENCES QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP) ON DELETE CASCADE\r\n);");
			migrationBuilder.Sql("CREATE TRIGGER DELETE_SIMPROP_TRIGGER DELETE ON QRTZ_TRIGGERS\r\nBEGIN\r\n\tDELETE FROM QRTZ_SIMPROP_TRIGGERS WHERE SCHED_NAME=OLD.SCHED_NAME AND TRIGGER_NAME=OLD.TRIGGER_NAME AND TRIGGER_GROUP=OLD.TRIGGER_GROUP;\r\nEND\r\n;");
			migrationBuilder.Sql("CREATE TABLE QRTZ_CRON_TRIGGERS\r\n  (\r\n    SCHED_NAME NVARCHAR(120) NOT NULL,\r\n\tTRIGGER_NAME NVARCHAR(150) NOT NULL,\r\n    TRIGGER_GROUP NVARCHAR(150) NOT NULL,\r\n    CRON_EXPRESSION NVARCHAR(250) NOT NULL,\r\n    TIME_ZONE_ID NVARCHAR(80),\r\n    PRIMARY KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP),\r\n    FOREIGN KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP)\r\n        REFERENCES QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP) ON DELETE CASCADE\r\n);");
			migrationBuilder.Sql("CREATE TRIGGER DELETE_CRON_TRIGGER DELETE ON QRTZ_TRIGGERS\r\nBEGIN\r\n\tDELETE FROM QRTZ_CRON_TRIGGERS WHERE SCHED_NAME=OLD.SCHED_NAME AND TRIGGER_NAME=OLD.TRIGGER_NAME AND TRIGGER_GROUP=OLD.TRIGGER_GROUP;\r\nEND\r\n;");
			migrationBuilder.Sql("CREATE TABLE QRTZ_BLOB_TRIGGERS\r\n  (\r\n    SCHED_NAME NVARCHAR(120) NOT NULL,\r\n\tTRIGGER_NAME NVARCHAR(150) NOT NULL,\r\n    TRIGGER_GROUP NVARCHAR(150) NOT NULL,\r\n    BLOB_DATA BLOB NULL,\r\n    PRIMARY KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP),\r\n    FOREIGN KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP)\r\n        REFERENCES QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP) ON DELETE CASCADE\r\n);");
			migrationBuilder.Sql("CREATE TRIGGER DELETE_BLOB_TRIGGER DELETE ON QRTZ_TRIGGERS\r\nBEGIN\r\n\tDELETE FROM QRTZ_BLOB_TRIGGERS WHERE SCHED_NAME=OLD.SCHED_NAME AND TRIGGER_NAME=OLD.TRIGGER_NAME AND TRIGGER_GROUP=OLD.TRIGGER_GROUP;\r\nEND\r\n;");
			migrationBuilder.Sql("CREATE TABLE QRTZ_CALENDARS\r\n  (\r\n    SCHED_NAME NVARCHAR(120) NOT NULL,\r\n\tCALENDAR_NAME  NVARCHAR(200) NOT NULL,\r\n    CALENDAR BLOB NOT NULL,\r\n    PRIMARY KEY (SCHED_NAME,CALENDAR_NAME)\r\n);");
			migrationBuilder.Sql("CREATE TABLE QRTZ_PAUSED_TRIGGER_GRPS\r\n  (\r\n    SCHED_NAME NVARCHAR(120) NOT NULL,\r\n\tTRIGGER_GROUP NVARCHAR(150) NOT NULL, \r\n    PRIMARY KEY (SCHED_NAME,TRIGGER_GROUP)\r\n);");
			migrationBuilder.Sql("CREATE TABLE QRTZ_FIRED_TRIGGERS\r\n  (\r\n    SCHED_NAME NVARCHAR(120) NOT NULL,\r\n\tENTRY_ID NVARCHAR(140) NOT NULL,\r\n    TRIGGER_NAME NVARCHAR(150) NOT NULL,\r\n    TRIGGER_GROUP NVARCHAR(150) NOT NULL,\r\n    INSTANCE_NAME NVARCHAR(200) NOT NULL,\r\n    FIRED_TIME BIGINT NOT NULL,\r\n    SCHED_TIME BIGINT NOT NULL,\r\n\tPRIORITY INTEGER NOT NULL,\r\n    STATE NVARCHAR(16) NOT NULL,\r\n    JOB_NAME NVARCHAR(150) NULL,\r\n    JOB_GROUP NVARCHAR(150) NULL,\r\n    IS_NONCONCURRENT BIT NULL,\r\n    REQUESTS_RECOVERY BIT NULL,\r\n    PRIMARY KEY (SCHED_NAME,ENTRY_ID)\r\n);");
			migrationBuilder.Sql("CREATE TABLE QRTZ_SCHEDULER_STATE\r\n  (\r\n    SCHED_NAME NVARCHAR(120) NOT NULL,\r\n\tINSTANCE_NAME NVARCHAR(200) NOT NULL,\r\n    LAST_CHECKIN_TIME BIGINT NOT NULL,\r\n    CHECKIN_INTERVAL BIGINT NOT NULL,\r\n    PRIMARY KEY (SCHED_NAME,INSTANCE_NAME)\r\n);");
			migrationBuilder.Sql("CREATE TABLE QRTZ_LOCKS\r\n  (\r\n    SCHED_NAME NVARCHAR(120) NOT NULL,\r\n\tLOCK_NAME  NVARCHAR(40) NOT NULL, \r\n    PRIMARY KEY (SCHED_NAME,LOCK_NAME)\r\n);");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "678d547d-e0ad-46db-9264-68aff65b4c4b");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0c015039-4e3c-481b-b002-cc4cd631fcc8", 0, "302382ea-9154-4ed1-95be-16831559a62d", "thinh.nguyenngoc@vnresource.org", false, true, null, "THINH.NGUYENNGOC@VNRESOURCE.ORG", "ADMIN", "AQAAAAIAAYagAAAAELWLtzbMBy9QOIDnX1hri5qXHRJ7Fg+vDi2h7bZZqoMKhr+39liGv3bGTSulok+cfQ==", null, false, "d65adeb6-a93f-4171-8c9c-68be38ca168f", false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("3c2e8836-3745-451b-826d-da952513e0a6"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8490), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8490), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("3fe18c8d-cfdc-419a-a848-e07d8b383386"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8474), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8475), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("44ae543b-1c51-4e79-81f0-e1d99e274162"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8487), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8487), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("8c6d144e-2fad-4cc2-a1ad-fe7661110f31"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8477), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8478), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("a1907086-a621-4a7f-9784-fe7a65a9b01a"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8468), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8469), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("a35339f6-ab5e-4197-825a-424a3888d7fb"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8471), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8472), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("c8b1413f-2f1d-41cf-96eb-aa7eaf9714e4"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8440), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 2, 8, 22, 58, 500, DateTimeKind.Unspecified).AddTicks(8464), new TimeSpan(0, 7, 0, 0, 0)) });
        }
    }
}
