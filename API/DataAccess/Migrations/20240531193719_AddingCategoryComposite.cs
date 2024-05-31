using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddingCategoryComposite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryFatherId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryType",
                table: "Categories",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryFatherId",
                table: "Categories",
                column: "CategoryFatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryFatherId",
                table: "Categories",
                column: "CategoryFatherId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryFatherId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryFatherId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryFatherId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "Categories");
        }
    }
}
