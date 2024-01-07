using Core.Entities;
using Core.Enums;
using Core.Interfaces.Services;
using Core.ViewModels.User;
using DataAccess;
using FluentResults;
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
    
    public async Task<Result<User>> RegisterUserAsync(RegisterUserViewModel model)
    {
        var isEmailTaken = await _context.Users.AnyAsync(u => u.Email == model.Email);
        if (isEmailTaken)
        {
            return Result.Fail($"Email {model.Email} is already taken");
        }
        
        var role = await _context.Roles.SingleAsync(r => r.Name == RolesEnum.RegularUser);
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