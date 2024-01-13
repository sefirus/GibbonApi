﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(GibbonDbContext))]
    [Migration("20230725210239_AddFarentFieldValues")]
    partial class AddFarentFieldValues
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.DataType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DataTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            CreatedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8914),
                            ModifiedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8917),
                            Name = "String"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            CreatedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8919),
                            ModifiedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8920),
                            Name = "Int"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            CreatedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8921),
                            ModifiedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8921),
                            Name = "Float"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            CreatedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8922),
                            ModifiedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8923),
                            Name = "ObjectId"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000005"),
                            CreatedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8924),
                            ModifiedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8924),
                            Name = "Uuid"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000006"),
                            CreatedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8931),
                            ModifiedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8932),
                            Name = "Object"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000007"),
                            CreatedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8933),
                            ModifiedDate = new DateTime(2023, 7, 25, 21, 2, 39, 594, DateTimeKind.Utc).AddTicks(8934),
                            Name = "Array"
                        });
                });

            modelBuilder.Entity("Core.Entities.FieldValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentFieldId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SchemaFieldId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("ParentFieldId");

                    b.HasIndex("SchemaFieldId");

                    b.ToTable("FieldValues");
                });

            modelBuilder.Entity("Core.Entities.SchemaField", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DataTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsArray")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPrimaryKey")
                        .HasColumnType("boolean");

                    b.Property<int?>("Length")
                        .HasColumnType("integer");

                    b.Property<double?>("Max")
                        .HasColumnType("double precision");

                    b.Property<double?>("Min")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ParentFieldId")
                        .HasColumnType("uuid");

                    b.Property<string>("Pattern")
                        .HasColumnType("text");

                    b.Property<Guid>("SchemaObjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("Summary")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DataTypeId");

                    b.HasIndex("ParentFieldId");

                    b.HasIndex("SchemaObjectId");

                    b.ToTable("SchemaFields");
                });

            modelBuilder.Entity("Core.Entities.SchemaObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("WorkspaceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("SchemaObjects");
                });

            modelBuilder.Entity("Core.Entities.StoredDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsGenerated")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PrimaryKeySchemaFieldId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SchemaObjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryKeySchemaFieldId");

                    b.HasIndex("SchemaObjectId");

                    b.ToTable("StoredDocuments");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("ApplicationRoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            AccessFailedCount = 0,
                            ApplicationRoleId = new Guid("00000000-0000-0000-0000-000000000001"),
                            ConcurrencyStamp = "29fdfc18-abc7-4a1d-8752-323e18f8bb03",
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "dev@email.com",
                            EmailConfirmed = false,
                            IsActive = true,
                            LockoutEnabled = false,
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NormalizedEmail = "DEV@EMAIL.COM",
                            NormalizedUserName = "DEV",
                            PasswordHash = "AQAAAAIAAYagAAAAEFECXKpTc+twLqEd4GhH/Nx55zUDpZbR8ov9Qy5nV1b/uRLnFt+Vz7eY9m9CT1ln+A==",
                            PhoneNumber = "00 000 000 0000",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ddc43b0c-eaee-44b3-81e7-65d41bd1b8e4",
                            TwoFactorEnabled = false,
                            UserName = "dev"
                        });
                });

            modelBuilder.Entity("Core.Entities.Workspace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsAiEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Workspaces");
                });

            modelBuilder.Entity("Core.Entities.WorkspacePermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WorkspaceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("WorkspacePermissions");
                });

            modelBuilder.Entity("Core.Entities.WorkspaceRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WorkspaceRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000011"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Owner"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000012"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000013"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "General"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Name = "SuperUser",
                            NormalizedName = "SUPERUSER"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Name = "RegularUser",
                            NormalizedName = "REGULARUSER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("00000000-0000-0000-0000-000000000001"),
                            RoleId = new Guid("00000000-0000-0000-0000-000000000001")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Core.Entities.FieldValue", b =>
                {
                    b.HasOne("Core.Entities.StoredDocument", "Document")
                        .WithMany("FieldValues")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.FieldValue", "ParentField")
                        .WithMany("ChildFields")
                        .HasForeignKey("ParentFieldId");

                    b.HasOne("Core.Entities.SchemaField", "SchemaField")
                        .WithMany("FieldValues")
                        .HasForeignKey("SchemaFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("ParentField");

                    b.Navigation("SchemaField");
                });

            modelBuilder.Entity("Core.Entities.SchemaField", b =>
                {
                    b.HasOne("Core.Entities.DataType", "DataType")
                        .WithMany()
                        .HasForeignKey("DataTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.SchemaField", "ParentField")
                        .WithMany("ChildFields")
                        .HasForeignKey("ParentFieldId");

                    b.HasOne("Core.Entities.SchemaObject", "SchemaObject")
                        .WithMany("Fields")
                        .HasForeignKey("SchemaObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DataType");

                    b.Navigation("ParentField");

                    b.Navigation("SchemaObject");
                });

            modelBuilder.Entity("Core.Entities.SchemaObject", b =>
                {
                    b.HasOne("Core.Entities.Workspace", "Workspace")
                        .WithMany("SchemaObjects")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("Core.Entities.StoredDocument", b =>
                {
                    b.HasOne("Core.Entities.SchemaField", "PrimaryKeySchemaField")
                        .WithMany()
                        .HasForeignKey("PrimaryKeySchemaFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.SchemaObject", "SchemaObject")
                        .WithMany("StoredDocuments")
                        .HasForeignKey("SchemaObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrimaryKeySchemaField");

                    b.Navigation("SchemaObject");
                });

            modelBuilder.Entity("Core.Entities.Workspace", b =>
                {
                    b.HasOne("Core.Entities.User", null)
                        .WithMany("OwnedWorkspaces")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Core.Entities.WorkspacePermission", b =>
                {
                    b.HasOne("Core.Entities.WorkspaceRole", "WorkspaceRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "User")
                        .WithMany("WorkspacePermissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Workspace", "Workspace")
                        .WithMany("WorkspacePermissions")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Workspace");

                    b.Navigation("WorkspaceRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Entities.FieldValue", b =>
                {
                    b.Navigation("ChildFields");
                });

            modelBuilder.Entity("Core.Entities.SchemaField", b =>
                {
                    b.Navigation("ChildFields");

                    b.Navigation("FieldValues");
                });

            modelBuilder.Entity("Core.Entities.SchemaObject", b =>
                {
                    b.Navigation("Fields");

                    b.Navigation("StoredDocuments");
                });

            modelBuilder.Entity("Core.Entities.StoredDocument", b =>
                {
                    b.Navigation("FieldValues");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Navigation("OwnedWorkspaces");

                    b.Navigation("WorkspacePermissions");
                });

            modelBuilder.Entity("Core.Entities.Workspace", b =>
                {
                    b.Navigation("SchemaObjects");

                    b.Navigation("WorkspacePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
