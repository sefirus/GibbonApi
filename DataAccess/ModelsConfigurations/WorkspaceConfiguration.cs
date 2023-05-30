using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.ModelsConfigurations;

public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Name).IsRequired();
        builder.HasOne(w => w.Owner)
            .WithMany(u => u.OwnedWorkspaces)
            .HasForeignKey(w => w.OwnerId);
        
    }
}