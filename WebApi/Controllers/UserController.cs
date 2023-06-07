using Core.Interfaces.Services;
using Core.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task Register(RegisterUserViewModel model)
    {
        var result = await _userService.RegisterUserAsync(model);
    }
}