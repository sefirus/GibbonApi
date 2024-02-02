using System.Security.Claims;
using Core.Interfaces.Services;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Result<Guid> GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Result.Fail("User ID claim not found.");
        }

        return Guid.Parse(userIdClaim.Value);
    }
}