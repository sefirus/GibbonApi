using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNestedTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataTypes_DataTypes_NestedTypeId",
                table: "DataTypes");

            migrationBuilder.DropIndex(
                name: "IX_DataTypes_NestedTypeId",
                table: "DataTypes");

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"));

            migrationBuilder.DropColumn(
                name: "NestedTypeId",
                table: "DataTypes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb8da24e-3d50-4d0a-b0e4-ddd82b005be3", "AQAAAAIAAYagAAAAELI7W1vK2jifEMmHoM22zN5A6qwY38Q2iQtgI+ZR+RRrJ33M51XXUO9NsTh9OkgIyw==", "8f5d1bbe-b655-4dc7-b8ed-e713904d0f7a" });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1072), new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1075) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1077), new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1077) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1079), new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1079) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1080), new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1080) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1081), new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1082) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1087), new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1088) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1089), new DateTime(2023, 7, 11, 20, 45, 45, 952, DateTimeKind.Utc).AddTicks(1089) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NestedTypeId",
                table: "DataTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4671767-51dc-4d11-bb91-1de7cbbcc45b", "AQAAAAIAAYagAAAAEKgZyn4ZTiXTkAHpaElp9S228fu/e3LxSBAK5jZ5lvzILXPuFEWSepbElXQEl33now==", "f6aef1d4-2b75-4019-9abe-ebb0eca010e5" });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedDate", "ModifiedDate", "NestedTypeId" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5564), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5567), null });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedDate", "ModifiedDate", "NestedTypeId" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5572), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5572), null });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedDate", "ModifiedDate", "NestedTypeId" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5668), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5668), null });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedDate", "ModifiedDate", "NestedTypeId" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5671), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5672), null });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedDate", "ModifiedDate", "NestedTypeId" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5673), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5673), null });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedDate", "ModifiedDate", "NestedTypeId" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5680), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5680), null });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedDate", "ModifiedDate", "NestedTypeId" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5709), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5709), new Guid("00000000-0000-0000-0000-000000000001") });

            migrationBuilder.InsertData(
                table: "DataTypes",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name", "NestedTypeId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5712), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5712), "Array", new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5714), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5714), "Array", new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5716), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5717), "Array", new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000011"), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5718), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5719), "Array", new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5721), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5721), "Array", new Guid("00000000-0000-0000-0000-000000000006") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataTypes_NestedTypeId",
                table: "DataTypes",
                column: "NestedTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataTypes_DataTypes_NestedTypeId",
                table: "DataTypes",
                column: "NestedTypeId",
                principalTable: "DataTypes",
                principalColumn: "Id");
        }
    }
}
