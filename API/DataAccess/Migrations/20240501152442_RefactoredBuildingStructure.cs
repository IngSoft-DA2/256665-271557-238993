using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredBuildingStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Buildings_LocationId",
                table: "Buildings");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_LocationId",
                table: "Buildings",
                column: "LocationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Buildings_LocationId",
                table: "Buildings");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_LocationId",
                table: "Buildings",
                column: "LocationId");
        }
    }
}
