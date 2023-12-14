using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DalBackup.Migrations
{
    /// <inheritdoc />
    public partial class change_name_colum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14001ec2-4a8c-48b7-9840-1a6c29c4cd64");

            migrationBuilder.RenameColumn(
                name: "SeverName",
                table: "ServerConnect",
                newName: "ServerName");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "edee3d68-edbd-4ebe-957d-d3457d7bf872");

            migrationBuilder.RenameColumn(
                name: "ServerName",
                table: "ServerConnect",
                newName: "SeverName");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "14001ec2-4a8c-48b7-9840-1a6c29c4cd64", 0, "dacaa1aa-a572-463d-8da2-692715caf1ec", "thinh.nguyenngoc@vnresource.org", false, true, null, "THINH.NGUYENNGOC@VNRESOURCE.ORG", "ADMIN", "AQAAAAIAAYagAAAAEJpWwxCe1onXTTJOlpldENP7Hxqqv7BLmbKLXzWMoa0NOP3nP1srX4Zf523eLgTp8A==", null, false, "406f4bdd-b2cc-4110-8b5d-e2219c3160f4", false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("3c2e8836-3745-451b-826d-da952513e0a6"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6693), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6694), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("3fe18c8d-cfdc-419a-a848-e07d8b383386"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6672), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6673), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("44ae543b-1c51-4e79-81f0-e1d99e274162"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6690), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6691), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("8c6d144e-2fad-4cc2-a1ad-fe7661110f31"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6676), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6678), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("a1907086-a621-4a7f-9784-fe7a65a9b01a"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6665), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6665), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("a35339f6-ab5e-4197-825a-424a3888d7fb"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6669), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6670), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Weeklies",
                keyColumn: "Id",
                keyValue: new Guid("c8b1413f-2f1d-41cf-96eb-aa7eaf9714e4"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6623), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 11, 11, 9, 59, 16, 715, DateTimeKind.Unspecified).AddTicks(6657), new TimeSpan(0, 7, 0, 0, 0)) });
        }
    }
}
