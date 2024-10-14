using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class MediaAttachmentsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "MediaAttachments",
                keyColumn: "MediaId",
                keyValue: 1,
                columns: new[] { "Bytes", "CreatedAt", "FilePath", "FileType", "Height", "PublicId", "Width" },
                values: new object[] { 21079L, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://res.cloudinary.com/dp34so8og/image/upload/v1728888961/r714qjypzjk74xzacfge.jpg", "jpg", 500, "r714qjypzjk74xzacfge", 500 });

            migrationBuilder.UpdateData(
                table: "MediaAttachments",
                keyColumn: "MediaId",
                keyValue: 2,
                columns: new[] { "Bytes", "CreatedAt", "EntryId", "FilePath", "FileType", "Height", "PublicId", "Width" },
                values: new object[] { 25927L, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "https://res.cloudinary.com/dp34so8og/image/upload/v1728888962/mhi7gxgc5npedgsrxywk.jpg", "jpg", 500, "mhi7gxgc5npedgsrxywk", 500 });

            migrationBuilder.InsertData(
                table: "MediaAttachments",
                columns: new[] { "MediaId", "Bytes", "CreatedAt", "EntryId", "FilePath", "FileType", "Height", "PublicId", "Width" },
                values: new object[,]
                {
                    { 3, 28629L, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "https://res.cloudinary.com/dp34so8og/image/upload/v1728889003/vwck4ehodogpda1mm0yb.jpg", "jpg", 500, "vwck4ehodogpda1mm0yb", 500 },
                    { 4, 30154L, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "https://res.cloudinary.com/dp34so8og/image/upload/v1728889005/dhvolfscyn4tubn0pfmd.jpg", "jpg", 500, "dhvolfscyn4tubn0pfmd", 500 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MediaAttachments",
                keyColumn: "MediaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MediaAttachments",
                keyColumn: "MediaId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 14, 0, 39, 59, 451, DateTimeKind.Local).AddTicks(2724));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 14, 0, 39, 59, 451, DateTimeKind.Local).AddTicks(2429));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "defd8035-ca84-4013-9f1d-1ae00af310b4",
                column: "CreatedAt",
                value: new DateTime(2024, 10, 14, 0, 39, 59, 451, DateTimeKind.Local).AddTicks(2763));

            migrationBuilder.UpdateData(
                table: "MediaAttachments",
                keyColumn: "MediaId",
                keyValue: 1,
                columns: new[] { "Bytes", "CreatedAt", "FilePath", "FileType", "Height", "PublicId", "Width" },
                values: new object[] { 0L, new DateTime(2024, 8, 10, 9, 20, 0, 0, DateTimeKind.Unspecified), "/media/park-photo.jpg", "image/jpeg", 0, "", 0 });

            migrationBuilder.UpdateData(
                table: "MediaAttachments",
                keyColumn: "MediaId",
                keyValue: 2,
                columns: new[] { "Bytes", "CreatedAt", "EntryId", "FilePath", "FileType", "Height", "PublicId", "Width" },
                values: new object[] { 0L, new DateTime(2024, 8, 11, 20, 45, 0, 0, DateTimeKind.Unspecified), 2, "/media/thoughts-audio.mp3", "audio/mpeg", 0, "", 0 });
        }
    }
}
