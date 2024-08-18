using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EntryTags",
                table: "EntryTags");

            migrationBuilder.DropIndex(
                name: "IX_EntryTags_EntryId",
                table: "EntryTags");

            migrationBuilder.DropColumn(
                name: "EntryTagId",
                table: "EntryTags");

            migrationBuilder.RenameColumn(
                name: "DiaryEntryId",
                table: "DiaryEntries",
                newName: "EntryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntryTags",
                table: "EntryTags",
                columns: new[] { "EntryId", "TagId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EntryTags",
                table: "EntryTags");

            migrationBuilder.RenameColumn(
                name: "EntryId",
                table: "DiaryEntries",
                newName: "DiaryEntryId");

            migrationBuilder.AddColumn<int>(
                name: "EntryTagId",
                table: "EntryTags",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntryTags",
                table: "EntryTags",
                column: "EntryTagId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryTags_EntryId",
                table: "EntryTags",
                column: "EntryId");
        }
    }
}
