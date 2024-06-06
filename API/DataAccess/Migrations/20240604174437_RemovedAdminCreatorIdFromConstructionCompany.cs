using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAdminCreatorIdFromConstructionCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionCompany_ConstructionCompanyAdmins_ConstructionCompanyAdminId",
                table: "ConstructionCompany");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionCompany_ConstructionCompanyAdminId",
                table: "ConstructionCompany");

            migrationBuilder.DropColumn(
                name: "ConstructionCompanyAdminId",
                table: "ConstructionCompany");

            migrationBuilder.AddColumn<Guid>(
                name: "ConstructionCompanyId",
                table: "ConstructionCompanyAdmins",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionCompanyAdmins_ConstructionCompanyId",
                table: "ConstructionCompanyAdmins",
                column: "ConstructionCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionCompanyAdmins_ConstructionCompany_ConstructionCompanyId",
                table: "ConstructionCompanyAdmins",
                column: "ConstructionCompanyId",
                principalTable: "ConstructionCompany",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionCompanyAdmins_ConstructionCompany_ConstructionCompanyId",
                table: "ConstructionCompanyAdmins");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionCompanyAdmins_ConstructionCompanyId",
                table: "ConstructionCompanyAdmins");

            migrationBuilder.DropColumn(
                name: "ConstructionCompanyId",
                table: "ConstructionCompanyAdmins");

            migrationBuilder.AddColumn<Guid>(
                name: "ConstructionCompanyAdminId",
                table: "ConstructionCompany",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
