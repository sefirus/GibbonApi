using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExcessiveColumnsFormValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FieldValues");

            migrationBuilder.DropColumn(
                name: "IsGenerated",
                table: "FieldValues");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "FieldValues");

            migrationBuilder.AddColumn<bool>(
                name: "IsGenerated",
                table: "StoredDocuments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21c3b90a-a403-4bcc-bd40-4a5fda03a986", "AQAAAAIAAYagAAAAEC91IMaSKOyCZDCW54LWduYYDT3AkOkimPxwHs1TPhomWFoGlZwgsVa2OdN73/qnBQ==", "64c205e8-ceff-4616-98cc-28f72dcff367" });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9642), new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9647) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9648), new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9649) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9651), new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9651) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9652), new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9652) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9653), new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9662) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9669), new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9669) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9670), new DateTime(2023, 7, 15, 10, 56, 12, 583, DateTimeKind.Utc).AddTicks(9671) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGenerated",
                table: "StoredDocuments");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FieldValues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsGenerated",
                table: "FieldValues",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "FieldValues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
