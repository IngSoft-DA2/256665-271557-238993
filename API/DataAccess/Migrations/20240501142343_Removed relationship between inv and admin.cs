using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Removedrelationshipbetweeninvandadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Administrators_AdministratorId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_AdministratorId",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "AdministratorId",
                table: "Invitations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdministratorId",
                table: "Invitations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_AdministratorId",
                table: "Invitations",
                column: "AdministratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Administrators_AdministratorId",
                table: "Invitations",
                column: "AdministratorId",
                principalTable: "Administrators",
                principalColumn: "Id");
        }
    }
}
