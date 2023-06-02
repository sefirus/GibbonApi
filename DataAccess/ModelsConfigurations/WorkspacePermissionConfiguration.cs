using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.ModelsConfigurations;

public class WorkspacePermissionConfiguration : IEntityTypeConfiguration<WorkspacePermission>
{
    public void Configure(EntityTypeBuilder<WorkspacePermission> builder)
    {
        builder.HasKey(wp => wp.Id);
        builder.HasOne(wp => wp.WorkspaceRole)
            .WithMany()
            .HasForeignKey(wp => wp.RoleId);
        builder.HasOne(wp => wp.User)
            .WithMany(u => u.WorkspacePermissions)
            .HasForeignKey(wp => wp.UserId);
        builder.HasOne(wp => wp.Workspace)
            .WithMany(w => w.WorkspacePermissions)
            .HasForeignKey(wp => wp.WorkspaceId);
    }
}