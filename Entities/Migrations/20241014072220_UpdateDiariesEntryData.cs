using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiariesEntryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defd8035-ca84-4013-9f1d-1ae00af310b4",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 14, 14, 22, 19, 710, DateTimeKind.Local).AddTicks(8082));

            migrationBuilder.UpdateData(
                table: "DiaryEntries",
                keyColumn: "EntryId",
                keyValue: 2,
                column: "Mood",
                value: "Thinking");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 14, 14, 17, 34, 807, DateTimeKind.Local).AddTicks(5128));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 14, 14, 17, 34, 807, DateTimeKind.Local).AddTicks(4813));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defd8035-ca84-4013-9f1d-1ae00af310b4",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 14, 14, 17, 34, 807, DateTimeKind.Local).AddTicks(5227));

            migrationBuilder.UpdateData(
                table: "DiaryEntries",
                keyColumn: "EntryId",
                keyValue: 2,
                column: "Mood",
                value: "Reflective");
        }
    }
}
