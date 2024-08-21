using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class RefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                columns: new[] { "CreatedAt", "RefreshToken" },
                values: new object[] { new DateTime(2024, 8, 21, 23, 23, 53, 15, DateTimeKind.Local).AddTicks(595), "" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                columns: new[] { "CreatedAt", "RefreshToken" },
                values: new object[] { new DateTime(2024, 8, 21, 23, 23, 53, 15, DateTimeKind.Local).AddTicks(217), "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                column: "CreatedAt",
                value: new DateTime(2024, 8, 21, 19, 33, 13, 999, DateTimeKind.Local).AddTicks(865));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                column: "CreatedAt",
                value: new DateTime(2024, 8, 21, 19, 33, 13, 999, DateTimeKind.Local).AddTicks(481));
        }
    }
}
