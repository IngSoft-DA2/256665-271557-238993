using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Seedadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flat_Owners_OwnerId",
                table: "Flat");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Administrators",
                newName: "Lastname");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Flat",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Administrators",
                columns: new[] { "Id", "Email", "Firstname", "Lastname", "Password", "Role" },
                values: new object[] { new Guid("e1a402b9-6760-46bc-8362-7cfdeda9f162"), "seedAdmin@example.com", "seedAdminName", "seedAdminLastName", "seedAdminPassword", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Flat_Owners_OwnerId",
                table: "Flat",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flat_Owners_OwnerId",
                table: "Flat");

            migrationBuilder.DeleteData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: new Guid("e1a402b9-6760-46bc-8362-7cfdeda9f162"));

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Administrators",
                newName: "LastName");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Flat",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Flat_Owners_OwnerId",
                table: "Flat",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");
        }
    }
}
