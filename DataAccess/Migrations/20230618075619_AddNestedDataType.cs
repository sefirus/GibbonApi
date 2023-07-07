using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNestedDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "a06f906d-d7ee-47cd-82e3-24bc27fef8fd", "AQAAAAIAAYagAAAAEHNoZfjRDgSYf9VLHbqNXA6fzOV6HMmqdHHSjfLnkAhUWKkuRlBVwqWglux1rLoQWA==", "bf4953f1-58b4-4b7f-974c-5f87e7524bab" });

            migrationBuilder.InsertData(
                table: "DataTypes",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name", "NestedTypeId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2724), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2727), "String", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2732), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2732), "Int", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2736), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2737), "Float", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2739), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2739), "ObjectId", null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2741), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2742), "Uuid", null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2752), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2752), "Object", null },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2780), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2780), "Array", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2784), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2784), "Array", new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2786), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2787), "Array", new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2789), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2789), "Array", new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000011"), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2792), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2792), "Array", new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2795), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2795), "Array", new Guid("00000000-0000-0000-0000-000000000006") }
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

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

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DropColumn(
                name: "NestedTypeId",
                table: "DataTypes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e27e4f25-b8cf-4c15-b6f1-e7816a2610b9", "AQAAAAIAAYagAAAAEFR3qHHRIJltVXbfYkNbJ7H6MHu2pWL8jGC+lfrSErYR3/3o24Brh4txrNbzdFJH4g==", "fad1ba93-6425-4c6e-9e18-04d347247e1d" });
        }
    }
}
