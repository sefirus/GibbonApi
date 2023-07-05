using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddValidationPropertiesToTheSchemaField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidatorJson",
                table: "SchemaFields");

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "SchemaFields",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Max",
                table: "SchemaFields",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Min",
                table: "SchemaFields",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pattern",
                table: "SchemaFields",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "SchemaFields",
                type: "text",
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
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5564), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5567) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5572), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5572) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5668), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5668) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5671), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5672) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5673), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5673) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5680), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5680) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5709), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5709) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5712), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5712) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5714), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5714) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5716), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5717) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5718), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5719) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5721), new DateTime(2023, 7, 5, 19, 38, 55, 330, DateTimeKind.Utc).AddTicks(5721) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "SchemaFields");

            migrationBuilder.DropColumn(
                name: "Max",
                table: "SchemaFields");

            migrationBuilder.DropColumn(
                name: "Min",
                table: "SchemaFields");

            migrationBuilder.DropColumn(
                name: "Pattern",
                table: "SchemaFields");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "SchemaFields");

            migrationBuilder.AddColumn<string>(
                name: "ValidatorJson",
                table: "SchemaFields",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a06f906d-d7ee-47cd-82e3-24bc27fef8fd", "AQAAAAIAAYagAAAAEHNoZfjRDgSYf9VLHbqNXA6fzOV6HMmqdHHSjfLnkAhUWKkuRlBVwqWglux1rLoQWA==", "bf4953f1-58b4-4b7f-974c-5f87e7524bab" });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2724), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2727) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2732), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2732) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2736), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2737) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2739), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2739) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2741), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2742) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2752), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2752) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2780), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2784), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2784) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2786), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2787) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2789), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2789) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2792), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2792) });

            migrationBuilder.UpdateData(
                table: "DataTypes",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2795), new DateTime(2023, 6, 18, 7, 56, 19, 328, DateTimeKind.Utc).AddTicks(2795) });
        }
    }
}
