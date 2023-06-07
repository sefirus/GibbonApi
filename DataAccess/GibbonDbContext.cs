using Core.Entities;
using Core.Interfaces;
using DataAccess.ModelsConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class GibbonDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<WorkspaceRole> WorkspaceRoles { get; set; }
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
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e is
            {
                Entity: ICreatableEntity, 
                State: EntityState.Added or EntityState.Modified
            });

        foreach (var entityEntry in entries)
        {
            var creatableEntry = (ICreatableEntity)entityEntry.Entity; 
            creatableEntry.ModifiedDate = DateTime.UtcNow;

            if (entityEntry.State != EntityState.Added)
            {
                continue;
            }
            
            if (creatableEntry.Id == default)
            {
                creatableEntry.Id = Guid.NewGuid();
            }
            creatableEntry.CreatedDate = DateTime.UtcNow;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}