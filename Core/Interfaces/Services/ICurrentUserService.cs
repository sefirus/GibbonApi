using FluentResults;

namespace Core.Interfaces.Services;

public interface ICurrentUserService
{
    Result<Guid> GetCurrentUserId();
}