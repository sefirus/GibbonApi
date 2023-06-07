using Core.Entities;
using Core.Enums;
using Core.Interfaces.Services;
using Core.ViewModels.User;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly GibbonDbContext _context;

    public UserService(
        GibbonDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> RegisterUserAsync(RegisterUserViewModel model)
    {
        var role = await _context.Roles.SingleOrDefaultAsync(r => r.Name == RolesEnum.RegularUser);
        if (role == null)
        {
            throw new Exception("Role RegularUser not found");
        }

        var user = new User
        {
            Email = model.Email,
            UserName = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            ApplicationRoleId = role.Id
        };

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, model.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;    
    }
}