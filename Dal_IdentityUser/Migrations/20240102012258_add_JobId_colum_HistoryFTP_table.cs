using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DalBackup.Migrations
{
    /// <inheritdoc />
    public partial class add_JobId_colum_HistoryFTP_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27fca573-bf42-449a-9fee-1ed31c2dc376");

            migrationBuilder.AddColumn<Guid>(
                name: "JobId",
                table: "HistoryFTPs",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0c015039-4e3c-481b-b002-cc4cd631fcc8");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "HistoryFTPs");

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
    }
}
