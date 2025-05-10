using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Categories_CategoryId",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Announcements",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_CategoryId",
                table: "Announcements",
                newName: "IX_Announcements_SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_SubCategories_SubCategoryId",
                table: "Announcements",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_SubCategories_SubCategoryId",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "Announcements",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_SubCategoryId",
                table: "Announcements",
                newName: "IX_Announcements_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Categories_CategoryId",
                table: "Announcements",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
