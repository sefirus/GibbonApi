using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public static class Seed
{
    public static void SeedRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(new[]
        {
            new IdentityRole<Guid>() {Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "SuperUser", NormalizedName = RolesEnum.SuperUser.ToUpper()},
            new IdentityRole<Guid>() {Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "RegularUser", NormalizedName = RolesEnum.RegularUser.ToUpper()}
        });
    }
    
    public static void SeedWorkspaceRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkspaceRole>().HasData(new[]
        {
            new WorkspaceRole() {Id = Guid.Parse("00000000-0000-0000-0000-000000000011"), Name = RolesEnum.Owner},
            new WorkspaceRole() {Id = Guid.Parse("00000000-0000-0000-0000-000000000012"), Name = RolesEnum.Admin},
            new WorkspaceRole() {Id = Guid.Parse("00000000-0000-0000-0000-000000000013"), Name = RolesEnum.General},
        });
    }

    public static void SeedAdmin(this ModelBuilder modelBuilder)
    {
        const string userName = "dev";
        const string email = "dev@email.com";
        var hasher = new PasswordHasher<User>();
        var admin = new User()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            ApplicationRoleId = Guid.Parse("00000000-0000-0000-0000-000000000001"), // SuperUser
            UserName = userName,
            NormalizedUserName = userName.ToUpper(),
            Email = email,
            NormalizedEmail = email.ToUpper(),
            PhoneNumber = "00 000 000 0000",
            SecurityStamp = Guid.NewGuid().ToString(),
            IsActive = true
        };
        admin.PasswordHash = hasher.HashPassword(admin, "Password123");
        // seed admin
        modelBuilder.Entity<User>().HasData(admin);
        // assign applicationRole role 
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>()
        {
            RoleId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            UserId = Guid.Parse("00000000-0000-0000-0000-000000000001")
        });
    }
}