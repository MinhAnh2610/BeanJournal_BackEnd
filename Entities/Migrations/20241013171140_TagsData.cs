using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class TagsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 14, 0, 11, 38, 54, DateTimeKind.Local).AddTicks(6614));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 14, 0, 11, 38, 54, DateTimeKind.Local).AddTicks(6311));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defd8035-ca84-4013-9f1d-1ae00af310b4",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 14, 0, 11, 38, 54, DateTimeKind.Local).AddTicks(6656));

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagId", "IconPublicId", "IconUrl", "ImagePublicId", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 4, "", "", "", "", "Travel" },
                    { 5, "", "", "", "", "Wellness" },
                    { 6, "", "", "", "", "Creativity" },
                    { 7, "", "", "", "", "Work" },
                    { 8, "", "", "", "", "Family" },
                    { 9, "", "", "", "", "Mindfulness" },
                    { 10, "", "", "", "", "Self-improvement" }
                });

            migrationBuilder.InsertData(
                table: "EntryTags",
                columns: new[] { "EntryId", "TagId" },
                values: new object[,]
                {
                    { 1, 9 },
                    { 1, 10 },
                    { 2, 5 },
                    { 2, 10 },
                    { 3, 7 },
                    { 3, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EntryTags",
                keyColumns: new[] { "EntryId", "TagId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "EntryTags",
                keyColumns: new[] { "EntryId", "TagId" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "EntryTags",
                keyColumns: new[] { "EntryId", "TagId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "EntryTags",
                keyColumns: new[] { "EntryId", "TagId" },
                keyValues: new object[] { 2, 10 });

            migrationBuilder.DeleteData(
                table: "EntryTags",
                keyColumns: new[] { "EntryId", "TagId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "EntryTags",
                keyColumns: new[] { "EntryId", "TagId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 10);

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
    }
}
