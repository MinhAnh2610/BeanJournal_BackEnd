using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMinhAnh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defd8035-ca84-4013-9f1d-1ae00af310b4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 22, 15, 15, 9, 856, DateTimeKind.Local).AddTicks(8688));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 22, 15, 15, 9, 856, DateTimeKind.Local).AddTicks(8291));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 14, 14, 22, 19, 710, DateTimeKind.Local).AddTicks(8036));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 14, 14, 22, 19, 710, DateTimeKind.Local).AddTicks(7724));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImagePublicId", "ProfileImageUrl", "RefreshToken", "RefreshTokenExpirationDateTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "defd8035-ca84-4013-9f1d-1ae00af310b4", 0, "5d73f102-7151-4b61-9ebf-06b963b8cd8b", new DateTime(2024, 10, 14, 14, 22, 19, 710, DateTimeKind.Local).AddTicks(8082), "minhanh26102004@gmail.com", true, false, null, "MINHANH26102004@GMAIL.COM", "MINHANH", "AQAAAAIAAYagAAAAEF1C5OJZKke9FZG7yE/6PV1UCtvpkedJLuEkNdqw0LmFDBbS0im5ogkNVaz95LlYbQ==", null, false, "", "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7EFGZ3VVT5HYJLWQHLEWVRPUCRJZNSD7", false, "minhanh" });
        }
    }
}
