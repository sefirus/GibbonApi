using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.ModelsConfigurations;

public class FieldValueConfiguration : IEntityTypeConfiguration<FieldValue>
{
    public void Configure(EntityTypeBuilder<FieldValue> builder)
    {
        builder.HasKey(fv => fv.Id);
        builder.Property(fv => fv.Value).IsRequired();
        builder.HasOne(fv => fv.Document)
            .WithMany(d => d.FieldValues)
            .HasForeignKey(fv => fv.DocumentId);
        builder.HasOne(fv => fv.SchemaField)
            .WithMany(sf => sf.FieldValues)
            .HasForeignKey(fv => fv.SchemaFieldId);
    }
}