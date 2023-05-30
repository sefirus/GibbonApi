using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.ModelsConfigurations;

public class SchemaObjectConfiguration : IEntityTypeConfiguration<SchemaObject>
{
    public void Configure(EntityTypeBuilder<SchemaObject> builder)
    {
        builder.HasKey(so => so.Id);
        builder.Property(so => so.Name).IsRequired();
        builder.HasOne(so => so.Workspace)
            .WithMany(w => w.SchemaObjects)
            .HasForeignKey(so => so.WorkspaceId);
    }
}