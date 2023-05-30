using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ApplicationRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workspaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsAiEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workspaces_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchemaObjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchemaObjects_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkspacePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspacePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkspacePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkspacePermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkspacePermissions_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchemaFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsPrimaryKey = table.Column<bool>(type: "boolean", nullable: false),
                    SchemaObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldName = table.Column<string>(type: "text", nullable: false),
                    ParentFieldId = table.Column<Guid>(type: "uuid", nullable: true),
                    DataTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValidatorJson = table.Column<string>(type: "text", nullable: false),
                    IsArray = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchemaFields_DataTypes_DataTypeId",
                        column: x => x.DataTypeId,
                        principalTable: "DataTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchemaFields_SchemaFields_ParentFieldId",
                        column: x => x.ParentFieldId,
                        principalTable: "SchemaFields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SchemaFields_SchemaObjects_SchemaObjectId",
                        column: x => x.SchemaObjectId,
                        principalTable: "SchemaObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoredDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SchemaObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoredDocuments_SchemaObjects_SchemaObjectId",
                        column: x => x.SchemaObjectId,
                        principalTable: "SchemaObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    SchemaFieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    IsGenerated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldValues_SchemaFields_SchemaFieldId",
                        column: x => x.SchemaFieldId,
                        principalTable: "SchemaFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldValues_StoredDocuments_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "StoredDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FieldValues_DocumentId",
                table: "FieldValues",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldValues_SchemaFieldId",
                table: "FieldValues",
                column: "SchemaFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemaFields_DataTypeId",
                table: "SchemaFields",
                column: "DataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemaFields_ParentFieldId",
                table: "SchemaFields",
                column: "ParentFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemaFields_SchemaObjectId",
                table: "SchemaFields",
                column: "SchemaObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemaObjects_WorkspaceId",
                table: "SchemaObjects",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_StoredDocuments_SchemaObjectId",
                table: "StoredDocuments",
                column: "SchemaObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ApplicationRoleId",
                table: "Users",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspacePermissions_RoleId",
                table: "WorkspacePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspacePermissions_UserId",
                table: "WorkspacePermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspacePermissions_WorkspaceId",
                table: "WorkspacePermissions",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_OwnerId",
                table: "Workspaces",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldValues");

            migrationBuilder.DropTable(
                name: "WorkspacePermissions");

            migrationBuilder.DropTable(
                name: "SchemaFields");

            migrationBuilder.DropTable(
                name: "StoredDocuments");

            migrationBuilder.DropTable(
                name: "DataTypes");

            migrationBuilder.DropTable(
                name: "SchemaObjects");

            migrationBuilder.DropTable(
                name: "Workspaces");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
