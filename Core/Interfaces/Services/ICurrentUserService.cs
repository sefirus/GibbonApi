namespace Core.Interfaces.Services;

public interface ICurrentUserService
{
    Guid GetCurrentUserId();
}