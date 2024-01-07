using Core.Entities;
using Core.ViewModels.User;
using FluentResults;

namespace Core.Interfaces.Services;

public interface IUserService
{
    Task<Result<User>> RegisterUserAsync(RegisterUserViewModel model);
}