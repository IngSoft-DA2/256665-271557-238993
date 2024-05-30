using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddingConstructionCompanyAdminDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserCreator",
                table: "ConstructionCompany",
                newName: "UserCreatorId");

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ConstructionCompanyAdminId",
                table: "ConstructionCompany",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConstructionCompanyAdmins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionCompanyAdmins", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionCompany_ConstructionCompanyAdminId",
                table: "ConstructionCompany",
                column: "ConstructionCompanyAdminId",
                unique: true,
                filter: "[ConstructionCompanyAdminId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionCompany_ConstructionCompanyAdmins_ConstructionCompanyAdminId",
                table: "ConstructionCompany",
                column: "ConstructionCompanyAdminId",
                principalTable: "ConstructionCompanyAdmins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionCompany_ConstructionCompanyAdmins_ConstructionCompanyAdminId",
                table: "ConstructionCompany");

            migrationBuilder.DropTable(
                name: "ConstructionCompanyAdmins");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionCompany_ConstructionCompanyAdminId",
                table: "ConstructionCompany");

            migrationBuilder.DropColumn(
                name: "ConstructionCompanyAdminId",
                table: "ConstructionCompany");

            migrationBuilder.RenameColumn(
                name: "UserCreatorId",
                table: "ConstructionCompany",
                newName: "UserCreator");

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
