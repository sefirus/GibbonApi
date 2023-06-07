using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameWorkspaceRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkspacePermissions_Roles_RoleId",
                table: "WorkspacePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "WorkspaceRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkspaceRoles",
                table: "WorkspaceRoles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e27e4f25-b8cf-4c15-b6f1-e7816a2610b9", "AQAAAAIAAYagAAAAEFR3qHHRIJltVXbfYkNbJ7H6MHu2pWL8jGC+lfrSErYR3/3o24Brh4txrNbzdFJH4g==", "fad1ba93-6425-4c6e-9e18-04d347247e1d" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspacePermissions_WorkspaceRoles_RoleId",
                table: "WorkspacePermissions",
                column: "RoleId",
                principalTable: "WorkspaceRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkspacePermissions_WorkspaceRoles_RoleId",
                table: "WorkspacePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkspaceRoles",
                table: "WorkspaceRoles");

            migrationBuilder.RenameTable(
                name: "WorkspaceRoles",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e82e3507-6814-4f88-b983-d6ea81c6476e", "AQAAAAIAAYagAAAAEOf2tYjW47dx4Rz+TCbN5buEa10WIdEW82r8A+uLqXc9ISm5Ys4hLehumsDyhv+iCQ==", "728b6e93-25ad-40b4-8f3e-72052993c72a" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspacePermissions_Roles_RoleId",
                table: "WorkspacePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
