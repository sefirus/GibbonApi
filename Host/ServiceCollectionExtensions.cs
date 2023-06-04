using Application.Services;
using Core.Interfaces.Services;

namespace Host;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IWorkspaceService, WorkspaceService>();
    }
}