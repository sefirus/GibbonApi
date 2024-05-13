using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixSnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_AspNetUsers_UserId",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Workspaces_UserId",
                table: "Workspaces");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workspaces");

            migrationBuilder.DropColumn(
                name: "IsReadOnly",
                table: "SchemaObjects");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9fefe1fc-994b-4879-9081-c4e00e2750f3", "AQAAAAIAAYagAAAAED5sHTcYek3qX46dhEV+x3uxWrv97Vx74cbAkhSrD+X8UPiz6EAeoy6cWnjeSd2QZQ==", "0556b929-8e1e-4b3d-9337-f43285b1a912" });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5949), new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5954) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5958), new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5959) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5960), new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5961) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5962), new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5963) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5964), new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5964) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5971), new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5972) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5973), new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5973) });

            migrationBuilder.InsertData(
                table: "DataTypes",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5974), new DateTime(2024, 5, 12, 21, 44, 0, 395, DateTimeKind.Utc).AddTicks(5975), "Boolean" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Workspaces",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadOnly",
                table: "SchemaObjects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29fdfc18-abc7-4a1d-8752-323e18f8bb03", "AQAAAAIAAYagAAAAEFECXKpTc+twLqEd4GhH/Nx55zUDpZbR8ov9Qy5nV1b/uRLnFt+Vz7eY9m9CT1ln+A==", "ddc43b0c-eaee-44b3-81e7-65d41bd1b8e4" });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8914), new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8917) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8919), new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8920) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8921), new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8921) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8922), new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8923) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8924), new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8924) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8931), new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8932) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8933), new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8934) });

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_UserId",
                table: "Workspaces",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_AspNetUsers_UserId",
                table: "Workspaces",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
