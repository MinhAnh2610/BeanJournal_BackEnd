using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0508330E-790A-497C-A84A-5DE5E0D8367B", "84cc659b-8d62-4299-8473-4c905a79bb0d", "Role", "User", "USER" },
                    { "F6F6F8BD-F92A-43EF-A8D9-CCC665D5021F", "a3e9f59f-15dc-4b9f-bc4e-06a39e5a9c6a", "Role", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC", 0, "d81e6a9a-d634-4460-a5fb-c9d6605c0338", new DateTime(2024, 8, 18, 19, 24, 42, 36, DateTimeKind.Local).AddTicks(1121), "greenbean@example.com", true, false, null, "GREENBEAN@EXAMPLE.COM", "GREENBEAN", "AQAAAAEAACcQAAAAED7fJ3s5wEK9jFVlE+Se3dDwH8jZV6cR9yL5B5g3rY4Vpxfd5vQg==", null, false, "74b0d82f-2ef7-4c9b-92cb-8a4e94db1f3d", false, "greenbean" },
                    { "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D", 0, "7c9f577f-8a87-4c15-9306-b51848c2ac3b", new DateTime(2024, 8, 18, 19, 24, 42, 36, DateTimeKind.Local).AddTicks(723), "soybean@example.com", true, false, null, "SOYBEAN@EXAMPLE.COM", "SOYBEAN", "AQAAAAEAACcQAAAAEMu+LydDLTTvQci/f5hBc1WTMehHnsIXNl/3lwWChO/4WkXxQpA==", null, false, "29a0e19c-6e5a-4d7b-b474-015d2461ef76", false, "soybean" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagId", "Name" },
                values: new object[,]
                {
                    { 1, "Nature" },
                    { 2, "Reflection" },
                    { 3, "Productivity" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0508330E-790A-497C-A84A-5DE5E0D8367B", "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC" },
                    { "F6F6F8BD-F92A-43EF-A8D9-CCC665D5021F", "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D" }
                });

            migrationBuilder.InsertData(
                table: "DiaryEntries",
                columns: new[] { "EntryId", "Content", "CreatedAt", "Mood", "Title", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Went to the park today, it was sunny and relaxing.", new DateTime(2024, 8, 10, 9, 15, 0, 0, DateTimeKind.Unspecified), "Happy", "A Day at the Park", new DateTime(2024, 8, 10, 9, 15, 0, 0, DateTimeKind.Unspecified), "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC" },
                    { 2, "Spent some time thinking about life, feeling a bit melancholy.", new DateTime(2024, 8, 11, 20, 30, 0, 0, DateTimeKind.Unspecified), "Reflective", "Reflective Evening", new DateTime(2024, 8, 11, 20, 30, 0, 0, DateTimeKind.Unspecified), "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC" },
                    { 3, "Had a great start today, finished a lot of tasks and feeling accomplished.", new DateTime(2024, 8, 12, 7, 45, 0, 0, DateTimeKind.Unspecified), "Energetic", "Productive Morning", new DateTime(2024, 8, 12, 7, 45, 0, 0, DateTimeKind.Unspecified), "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC" }
                });

            migrationBuilder.InsertData(
                table: "EntryTags",
                columns: new[] { "EntryId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "MediaAttachments",
                columns: new[] { "MediaId", "CreatedAt", "EntryId", "FilePath", "FileType" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 10, 9, 20, 0, 0, DateTimeKind.Unspecified), 1, "/media/park-photo.jpg", "image/jpeg" },
                    { 2, new DateTime(2024, 8, 11, 20, 45, 0, 0, DateTimeKind.Unspecified), 2, "/media/thoughts-audio.mp3", "audio/mpeg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0508330E-790A-497C-A84A-5DE5E0D8367B", "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "F6F6F8BD-F92A-43EF-A8D9-CCC665D5021F", "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D" });

            migrationBuilder.DeleteData(
                table: "EntryTags",
                keyColumns: new[] { "EntryId", "TagId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "EntryTags",
                keyColumns: new[] { "EntryId", "TagId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "EntryTags",
                keyColumns: new[] { "EntryId", "TagId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "MediaAttachments",
                keyColumn: "MediaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MediaAttachments",
                keyColumn: "MediaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0508330E-790A-497C-A84A-5DE5E0D8367B");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "F6F6F8BD-F92A-43EF-A8D9-CCC665D5021F");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "C8E6EC09-E26E-4CB9-8FE3-E167AF44CB8D");

            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "EntryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "EntryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "EntryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "507F10EC-3BAB-4C22-B4AD-4D5E3FDBC2AC");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}
