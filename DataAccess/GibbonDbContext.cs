using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class GibbonDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<WorkspacePermission> WorkspacePermissions { get; set; }
    public DbSet<DataType> DataTypes { get; set; }
    public DbSet<SchemaObject> SchemaObjects { get; set; }
    public DbSet<SchemaField> SchemaFields { get; set; }
    public DbSet<FieldValue> FieldValues { get; set; }
    public DbSet<StoredDocument> StoredDocuments { get; set; }
}