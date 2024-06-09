using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MaintenanceRequestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_RequestHandlers_RequestHandlerId",
                table: "MaintenanceRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestHandlerId",
                table: "MaintenanceRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_RequestHandlers_RequestHandlerId",
                table: "MaintenanceRequests",
                column: "RequestHandlerId",
                principalTable: "RequestHandlers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_RequestHandlers_RequestHandlerId",
                table: "MaintenanceRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestHandlerId",
                table: "MaintenanceRequests",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_RequestHandlers_RequestHandlerId",
                table: "MaintenanceRequests",
                column: "RequestHandlerId",
                principalTable: "RequestHandlers",
                principalColumn: "Id");
        }
    }
}
