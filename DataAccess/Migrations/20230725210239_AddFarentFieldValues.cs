using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddFarentFieldValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentFieldId",
                table: "FieldValues",
                type: "uuid",
                nullable: true);

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
                name: "IX_FieldValues_ParentFieldId",
                table: "FieldValues",
                column: "ParentFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldValues_FieldValues_ParentFieldId",
                table: "FieldValues",
                column: "ParentFieldId",
                principalTable: "FieldValues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldValues_FieldValues_ParentFieldId",
                table: "FieldValues");

            migrationBuilder.DropIndex(
                name: "IX_FieldValues_ParentFieldId",
                table: "FieldValues");

            migrationBuilder.DropColumn(
                name: "ParentFieldId",
                table: "FieldValues");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8b74840-41ea-4ee6-a425-d1637893f2e4", "AQAAAAIAAYagAAAAEHM87ertoks99nVjP0ztHEHOPNzhaUAq7zX5hPs8e4zu05IWoeI8YGYzH1c/pMjRLA==", "b9f8efbf-07f7-4148-846c-f66decd3f972" });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8599), new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8604) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8606), new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8606) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8607), new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8608) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8609), new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8609) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8610), new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8619) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8624), new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8625) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8626), new DateTime(2023, 7, 21, 19, 38, 47, 216, DateTimeKind.Utc).AddTicks(8626) });
        }
    }
}
