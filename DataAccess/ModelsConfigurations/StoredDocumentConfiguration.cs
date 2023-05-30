using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.ModelsConfigurations;

public class StoredDocumentConfiguration : IEntityTypeConfiguration<StoredDocument>
{
    public void Configure(EntityTypeBuilder<StoredDocument> builder)
    {
        builder.HasKey(sd => sd.Id);
        builder.HasOne(sd => sd.SchemaObject)
            .WithMany(so => so.StoredDocuments)
            .HasForeignKey(sd => sd.SchemaObjectId);
        builder.HasMany(sd => sd.FieldValues)
            .WithOne(fv => fv.Document)
            .HasForeignKey(fv => fv.DocumentId);
    }
}