using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationStoredDocumentPKToSchemaField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryKeySchemaFieldId",
                table: "StoredDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_StoredDocuments_PrimaryKeySchemaFieldId",
                table: "StoredDocuments",
                column: "PrimaryKeySchemaFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoredDocuments_SchemaFields_PrimaryKeySchemaFieldId",
                table: "StoredDocuments",
                column: "PrimaryKeySchemaFieldId",
                principalTable: "SchemaFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoredDocuments_SchemaFields_PrimaryKeySchemaFieldId",
                table: "StoredDocuments");

            migrationBuilder.DropIndex(
                name: "IX_StoredDocuments_PrimaryKeySchemaFieldId",
                table: "StoredDocuments");

            migrationBuilder.DropColumn(
                name: "PrimaryKeySchemaFieldId",
                table: "StoredDocuments");

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
    }
}
