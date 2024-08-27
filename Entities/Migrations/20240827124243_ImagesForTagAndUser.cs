using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class ImagesForTagAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconPublicId",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImagePublicId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                columns: new[] { "CreatedAt", "ProfileImagePublicId", "ProfileImageUrl" },
                values: new object[] { new DateTime(2024, 8, 27, 19, 42, 41, 769, DateTimeKind.Local).AddTicks(4160), "", "" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                columns: new[] { "CreatedAt", "ProfileImagePublicId", "ProfileImageUrl" },
                values: new object[] { new DateTime(2024, 8, 27, 19, 42, 41, 769, DateTimeKind.Local).AddTicks(3773), "", "" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defd8035-ca84-4013-9f1d-1ae00af310b4",
                columns: new[] { "CreatedAt", "ProfileImagePublicId", "ProfileImageUrl" },
                values: new object[] { new DateTime(2024, 8, 27, 19, 42, 41, 769, DateTimeKind.Local).AddTicks(4245), "", "" });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 1,
                columns: new[] { "IconPublicId", "IconUrl", "ImagePublicId", "ImageUrl" },
                values: new object[] { "", "", "", "" });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 2,
                columns: new[] { "IconPublicId", "IconUrl", "ImagePublicId", "ImageUrl" },
                values: new object[] { "", "", "", "" });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 3,
                columns: new[] { "IconPublicId", "IconUrl", "ImagePublicId", "ImageUrl" },
                values: new object[] { "", "", "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconPublicId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ProfileImagePublicId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                column: "CreatedAt",
                value: new DateTime(2024, 8, 25, 11, 13, 41, 220, DateTimeKind.Local).AddTicks(1376));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                column: "CreatedAt",
                value: new DateTime(2024, 8, 25, 11, 13, 41, 220, DateTimeKind.Local).AddTicks(992));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defd8035-ca84-4013-9f1d-1ae00af310b4",
                column: "CreatedAt",
                value: new DateTime(2024, 8, 25, 11, 13, 41, 220, DateTimeKind.Local).AddTicks(1455));
        }
    }
}
