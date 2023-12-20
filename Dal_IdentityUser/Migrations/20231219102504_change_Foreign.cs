using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DalBackup.Migrations
{
    /// <inheritdoc />
    public partial class change_Foreign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_ScheduleBackups_ConfigurationBackUpId",
                table: "ScheduleBackups");

            migrationBuilder.DropIndex(
                name: "IX_FTPSettings_ConfigurationBackUpId",
                table: "FTPSettings");

            migrationBuilder.DropIndex(
                name: "IX_EmailConfirmations_ConfigurationBackUpId",
                table: "EmailConfirmations");

            migrationBuilder.DropIndex(
                name: "IX_BackUpSettings_ConfigurationBackUpId",
                table: "BackUpSettings");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "edee3d68-edbd-4ebe-957d-d3457d7bf872");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "27fca573-bf42-449a-9fee-1ed31c2dc376", 0, "a2a0b345-49cd-458e-bcf6-2e020f925be1", "thinh.nguyenngoc@vnresource.org", false, true, null, "THINH.NGUYENNGOC@VNRESOURCE.ORG", "ADMIN", "AQAAAAIAAYagAAAAEHuSDYTAkDibY4Mcuil9Nt1ksL4XDAYnK/hkQe0XgsSBzZsc2w/XiWUIGe6UwYFDKw==", null, false, "742e6953-461f-4ec3-a15a-cd559c1b0545", false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("3c2e8836-3745-451b-826d-da952513e0a6"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2840), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2841), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("3fe18c8d-cfdc-419a-a848-e07d8b383386"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2822), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2822), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("44ae543b-1c51-4e79-81f0-e1d99e274162"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2837), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2838), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("8c6d144e-2fad-4cc2-a1ad-fe7661110f31"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2826), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2826), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("a1907086-a621-4a7f-9784-fe7a65a9b01a"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2816), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2816), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("a35339f6-ab5e-4197-825a-424a3888d7fb"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2819), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2820), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("c8b1413f-2f1d-41cf-96eb-aa7eaf9714e4"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2788), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 12, 19, 17, 25, 4, 466, DateTimeKind.Unspecified).AddTicks(2810), new TimeSpan(0, 7, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27fca573-bf42-449a-9fee-1ed31c2dc376");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "edee3d68-edbd-4ebe-957d-d3457d7bf872", 0, "fab3ab3d-bbf1-4faa-b536-590c0422abce", "thinh.nguyenngoc@vnresource.org", false, true, null, "THINH.NGUYENNGOC@VNRESOURCE.ORG", "ADMIN", "AQAAAAIAAYagAAAAEItT0LPjvDGYa5LsqHok+jGWKLcVX3bmBzblc7lKeLzmdP70VrK6XYSyoVs3qec8EA==", null, false, "c13c91e5-4829-4cae-9acd-a2488b70cb54", false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("3c2e8836-3745-451b-826d-da952513e0a6"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9807), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9808), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("3fe18c8d-cfdc-419a-a848-e07d8b383386"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9789), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9789), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("44ae543b-1c51-4e79-81f0-e1d99e274162"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9804), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9805), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("8c6d144e-2fad-4cc2-a1ad-fe7661110f31"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9792), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9793), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("a1907086-a621-4a7f-9784-fe7a65a9b01a"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9782), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9782), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("a35339f6-ab5e-4197-825a-424a3888d7fb"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9786), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9786), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("c8b1413f-2f1d-41cf-96eb-aa7eaf9714e4"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9746), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 27, 8, 41, 38, 7, DateTimeKind.Unspecified).AddTicks(9776), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleBackups_ConfigurationBackUpId",
                table: "ScheduleBackups",
                column: "ConfigurationBackUpId");

            migrationBuilder.CreateIndex(
                name: "IX_FTPSettings_ConfigurationBackUpId",
                table: "FTPSettings",
                column: "ConfigurationBackUpId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailConfirmations_ConfigurationBackUpId",
                table: "EmailConfirmations",
                column: "ConfigurationBackUpId");

            migrationBuilder.CreateIndex(
                name: "IX_BackUpSettings_ConfigurationBackUpId",
                table: "BackUpSettings",
                column: "ConfigurationBackUpId");

            migrationBuilder.AddForeignKey(
                name: "FK_BackUpSettings_ConfigurationBackUps_ConfigurationBackUpId",
                table: "BackUpSettings",
                column: "ConfigurationBackUpId",
                principalTable: "ConfigurationBackUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailConfirmations_ConfigurationBackUps_ConfigurationBackUpId",
                table: "EmailConfirmations",
                column: "ConfigurationBackUpId",
                principalTable: "ConfigurationBackUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FTPSettings_ConfigurationBackUps_ConfigurationBackUpId",
                table: "FTPSettings",
                column: "ConfigurationBackUpId",
                principalTable: "ConfigurationBackUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleBackups_ConfigurationBackUps_ConfigurationBackUpId",
                table: "ScheduleBackups",
                column: "ConfigurationBackUpId",
                principalTable: "ConfigurationBackUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
