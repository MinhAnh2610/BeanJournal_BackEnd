using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class DockerContainerSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 23, 49, 58, 64, DateTimeKind.Local).AddTicks(2532));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 23, 49, 58, 64, DateTimeKind.Local).AddTicks(2020));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defd8035-ca84-4013-9f1d-1ae00af310b4",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 12, 23, 49, 58, 64, DateTimeKind.Local).AddTicks(2681));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                column: "CreatedAt",
                value: new DateTime(2024, 8, 27, 19, 42, 41, 769, DateTimeKind.Local).AddTicks(4160));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                column: "CreatedAt",
                value: new DateTime(2024, 8, 27, 19, 42, 41, 769, DateTimeKind.Local).AddTicks(3773));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defd8035-ca84-4013-9f1d-1ae00af310b4",
                column: "CreatedAt",
                value: new DateTime(2024, 8, 27, 19, 42, 41, 769, DateTimeKind.Local).AddTicks(4245));
        }
    }
}
