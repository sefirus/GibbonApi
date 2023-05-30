using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.ModelsConfigurations;

public class DataTypeConfiguration : IEntityTypeConfiguration<DataType>
{
    public void Configure(EntityTypeBuilder<DataType> builder)
    {
        builder.HasKey(dt => dt.Id);
        builder.Property(dt => dt.Name).IsRequired();
    }
}