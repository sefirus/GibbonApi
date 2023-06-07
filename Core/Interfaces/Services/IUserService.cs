using Core.Entities;
using Core.ViewModels.User;

namespace Core.Interfaces.Services;

public interface IUserService
{
    Task<User> RegisterUserAsync(RegisterUserViewModel model);
}