using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.ModelsConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Username).IsRequired();
        builder.Property(u => u.Email).IsRequired();
        builder.HasOne(u => u.ApplicationRole)
            .WithMany()
            .HasForeignKey(u => u.ApplicationRoleId);
    }
}