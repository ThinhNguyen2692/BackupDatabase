using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DalBackup.Migrations
{
    /// <inheritdoc />
    public partial class Add_table_config : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryFTPs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    JobName = table.Column<string>(type: "TEXT", nullable: false),
                    FullFilePathName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryFTPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerConnect",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SeverName = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    PassWord = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerConnect", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weeklies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeklies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatabaseConnect",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DatabaseName = table.Column<string>(type: "TEXT", nullable: false),
                    ServerConnectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseConnect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatabaseConnect_ServerConnect_ServerConnectId",
                        column: x => x.ServerConnectId,
                        principalTable: "ServerConnect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BackUpSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BackUpType = table.Column<int>(type: "INTEGER", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigurationBackUpId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackUpSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationBackUps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BackupName = table.Column<string>(type: "TEXT", nullable: false),
                    BackUpSettingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FTPSettingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ScheduleBackupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmailConfirmationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DatabaseConnectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsScheduleBackup = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEmailConfirmation = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationBackUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationBackUps_BackUpSettings_BackUpSettingId",
                        column: x => x.BackUpSettingId,
                        principalTable: "BackUpSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationBackUps_DatabaseConnect_DatabaseConnectId",
                        column: x => x.DatabaseConnectId,
                        principalTable: "DatabaseConnect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailConfirmations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmailSuccess = table.Column<string>(type: "TEXT", nullable: false),
                    EmailFailure = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigurationBackUpId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailConfirmations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailConfirmations_ConfigurationBackUps_ConfigurationBackUpId",
                        column: x => x.ConfigurationBackUpId,
                        principalTable: "ConfigurationBackUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FTPSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    HostName = table.Column<string>(type: "TEXT", nullable: false),
                    Protocol = table.Column<int>(type: "INTEGER", nullable: false),
                    Port = table.Column<int>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    PassWord = table.Column<string>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    IsAutoDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    Months = table.Column<int>(type: "INTEGER", nullable: false),
                    Days = table.Column<int>(type: "INTEGER", nullable: false),
                    ConfigurationBackUpId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FTPSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FTPSettings_ConfigurationBackUps_ConfigurationBackUpId",
                        column: x => x.ConfigurationBackUpId,
                        principalTable: "ConfigurationBackUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleBackups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Occurs = table.Column<int>(type: "INTEGER", nullable: false),
                    RecursEveryDay = table.Column<int>(type: "INTEGER", nullable: false),
                    RecursEveryWeekly = table.Column<int>(type: "INTEGER", nullable: false),
                    MonthlyDay = table.Column<bool>(type: "INTEGER", nullable: false),
                    MonthlyThe = table.Column<bool>(type: "INTEGER", nullable: false),
                    DayEvery = table.Column<int>(type: "INTEGER", nullable: false),
                    DayMonth = table.Column<int>(type: "INTEGER", nullable: false),
                    TheOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    TheWeekly = table.Column<int>(type: "INTEGER", nullable: false),
                    TheMonth = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActionType = table.Column<bool>(type: "INTEGER", nullable: false),
                    FreqSubdayType = table.Column<int>(type: "INTEGER", nullable: false),
                    FreqSubdayInterval = table.Column<int>(type: "INTEGER", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    ConfigurationBackUpId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleBackups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleBackups_ConfigurationBackUps_ConfigurationBackUpId",
                        column: x => x.ConfigurationBackUpId,
                        principalTable: "ConfigurationBackUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleBackupWeeklies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ScheduleBackupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    WeeklyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleBackupWeeklies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleBackupWeeklies_ScheduleBackups_ScheduleBackupId",
                        column: x => x.ScheduleBackupId,
                        principalTable: "ScheduleBackups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleBackupWeeklies_Weeklies_WeeklyId",
                        column: x => x.WeeklyId,
                        principalTable: "Weeklies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "14001ec2-4a8c-48b7-9840-1a6c29c4cd64", 0, "dacaa1aa-a572-463d-8da2-692715caf1ec", "thinh.nguyenngoc@vnresource.org", false, true, null, "THINH.NGUYENNGOC@VNRESOURCE.ORG", "ADMIN", "AQAAAAIAAYagAAAAEJpWwxCe1onXTTJOlpldENP7Hxqqv7BLmbKLXzWMoa0NOP3nP1srX4Zf523eLgTp8A==", null, false, "406f4bdd-b2cc-4110-8b5d-e2219c3160f4", false, "Admin" });

            migrationBuilder.InsertData(
                table: "Weeklies",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "Name", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { new Guid("3c2e8836-3745-451b-826d-da952513e0a6"), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6693), new TimeSpan(0, 7, 0, 0, 0)), false, "Sunday", new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6694), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { new Guid("3fe18c8d-cfdc-419a-a848-e07d8b383386"), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6672), new TimeSpan(0, 7, 0, 0, 0)), false, "Thursday", new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6673), new TimeSpan(0, 7, 0, 0, 0)), 16 },
                    { new Guid("44ae543b-1c51-4e79-81f0-e1d99e274162"), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6690), new TimeSpan(0, 7, 0, 0, 0)), false, "Saturday", new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6691), new TimeSpan(0, 7, 0, 0, 0)), 64 },
                    { new Guid("8c6d144e-2fad-4cc2-a1ad-fe7661110f31"), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6676), new TimeSpan(0, 7, 0, 0, 0)), false, "Friday", new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6678), new TimeSpan(0, 7, 0, 0, 0)), 32 },
                    { new Guid("a1907086-a621-4a7f-9784-fe7a65a9b01a"), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6665), new TimeSpan(0, 7, 0, 0, 0)), false, "Tuesday", new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6665), new TimeSpan(0, 7, 0, 0, 0)), 4 },
                    { new Guid("a35339f6-ab5e-4197-825a-424a3888d7fb"), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6669), new TimeSpan(0, 7, 0, 0, 0)), false, "Wednesday", new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6670), new TimeSpan(0, 7, 0, 0, 0)), 8 },
                    { new Guid("c8b1413f-2f1d-41cf-96eb-aa7eaf9714e4"), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6623), new TimeSpan(0, 7, 0, 0, 0)), false, "Monday", new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6657), new TimeSpan(0, 7, 0, 0, 0)), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BackUpSettings_ConfigurationBackUpId",
                table: "BackUpSettings",
                column: "ConfigurationBackUpId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationBackUps_BackUpSettingId",
                table: "ConfigurationBackUps",
                column: "BackUpSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationBackUps_DatabaseConnectId",
                table: "ConfigurationBackUps",
                column: "DatabaseConnectId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationBackUps_EmailConfirmationId",
                table: "ConfigurationBackUps",
                column: "EmailConfirmationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationBackUps_FTPSettingId",
                table: "ConfigurationBackUps",
                column: "FTPSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationBackUps_ScheduleBackupId",
                table: "ConfigurationBackUps",
                column: "ScheduleBackupId");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseConnect_ServerConnectId",
                table: "DatabaseConnect",
                column: "ServerConnectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailConfirmations_ConfigurationBackUpId",
                table: "EmailConfirmations",
                column: "ConfigurationBackUpId");

            migrationBuilder.CreateIndex(
                name: "IX_FTPSettings_ConfigurationBackUpId",
                table: "FTPSettings",
                column: "ConfigurationBackUpId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleBackups_ConfigurationBackUpId",
                table: "ScheduleBackups",
                column: "ConfigurationBackUpId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleBackupWeeklies_ScheduleBackupId",
                table: "ScheduleBackupWeeklies",
                column: "ScheduleBackupId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleBackupWeeklies_WeeklyId",
                table: "ScheduleBackupWeeklies",
                column: "WeeklyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BackUpSettings_ConfigurationBackUps_ConfigurationBackUpId",
                table: "BackUpSettings",
                column: "ConfigurationBackUpId",
                principalTable: "ConfigurationBackUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationBackUps_EmailConfirmations_EmailConfirmationId",
                table: "ConfigurationBackUps",
                column: "EmailConfirmationId",
                principalTable: "EmailConfirmations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationBackUps_FTPSettings_FTPSettingId",
                table: "ConfigurationBackUps",
                column: "FTPSettingId",
                principalTable: "FTPSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationBackUps_ScheduleBackups_ScheduleBackupId",
                table: "ConfigurationBackUps",
                column: "ScheduleBackupId",
                principalTable: "ScheduleBackups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackUpSettings_ConfigurationBackUps_ConfigurationBackUpId",
                table: "BackUpSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailConfirmations_ConfigurationBackUps_ConfigurationBackUpId",
                table: "EmailConfirmations");

            migrationBuilder.DropForeignKey(
                name: "FK_FTPSettings_ConfigurationBackUps_ConfigurationBackUpId",
                table: "FTPSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleBackups_ConfigurationBackUps_ConfigurationBackUpId",
                table: "ScheduleBackups");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "HistoryFTPs");

            migrationBuilder.DropTable(
                name: "ScheduleBackupWeeklies");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Weeklies");

            migrationBuilder.DropTable(
                name: "ConfigurationBackUps");

            migrationBuilder.DropTable(
                name: "BackUpSettings");

            migrationBuilder.DropTable(
                name: "DatabaseConnect");

            migrationBuilder.DropTable(
                name: "EmailConfirmations");

            migrationBuilder.DropTable(
                name: "FTPSettings");

            migrationBuilder.DropTable(
                name: "ScheduleBackups");

            migrationBuilder.DropTable(
                name: "ServerConnect");
        }
    }
}
