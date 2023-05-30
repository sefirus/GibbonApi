using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.ModelsConfigurations;

public class SchemaFieldConfiguration : IEntityTypeConfiguration<SchemaField>
{
    public void Configure(EntityTypeBuilder<SchemaField> builder)
    {
        builder.HasKey(sf => sf.Id);
        builder.Property(sf => sf.FieldName).IsRequired();
        builder.HasOne(sf => sf.SchemaObject)
            .WithMany(so => so.Fields)
            .HasForeignKey(sf => sf.SchemaObjectId);
        builder.HasOne(sf => sf.DataType)
            .WithMany()
            .HasForeignKey(sf => sf.DataTypeId);
        builder.HasOne(sf => sf.ParentField)
            .WithMany(sf => sf.ChildFields)
            .HasForeignKey(sf => sf.ParentFieldId);
    }
}