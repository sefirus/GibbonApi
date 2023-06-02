using Core.Entities;
using DataAccess.ModelsConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class GibbonDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<WorkspaceRole> Roles { get; set; }
    public DbSet<WorkspacePermission> WorkspacePermissions { get; set; }
    public DbSet<DataType> DataTypes { get; set; }
    public DbSet<SchemaObject> SchemaObjects { get; set; }
    public DbSet<SchemaField> SchemaFields { get; set; }
    public DbSet<FieldValue> FieldValues { get; set; }
    public DbSet<StoredDocument> StoredDocuments { get; set; }
    
    public GibbonDbContext(DbContextOptions<GibbonDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        modelBuilder.SeedRoles();
        modelBuilder.SeedWorkspaceRoles();
        modelBuilder.SeedAdmin();
    }
}